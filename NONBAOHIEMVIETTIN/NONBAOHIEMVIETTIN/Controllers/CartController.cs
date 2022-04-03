using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NONBAOHIEMVIETTIN.Models;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Web.Script.Serialization;
using NONBAOHIEMVIETTIN.assets.NganLuongAPI;
using System.Data.Entity;

namespace NONBAOHIEMVIETTIN.Controllers
{
    public class CartController : Controller
    {
        private string merchantId = ConfigurationManager.AppSettings["MerchantId"];
        private string merchantPassword = ConfigurationManager.AppSettings["MerchantPassword"];
        private string merchantEmail = ConfigurationManager.AppSettings["MerchantEmail"];
        // GET: Cart
        public const string cartSession = "cartSession";
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: Cart
        [HandleError]

        public ActionResult Index()
        {
            var cart = Session[cartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        [HttpPost]
        public JsonResult DeleteItem(int ProductId)
        {
            var cart = Session[cartSession];
            var p = db.products.Find(ProductId);
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.id == ProductId))
                {
                    list.RemoveAll(r => r.Product.id == ProductId);
                    Session[cartSession] = list;
                    return Json(new { status = 1, sumQuantity = list.Sum(x => x.Quantity), sumMoney = Libary.Instances.convertVND(list.Sum(x => x.Quantity * (x.Product.promationprice > 0 ? x.Product.promationprice : x.Product.price)).ToString()) }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult AddItem(int ProductId, int Quantity)
        {
            if (Session["account"] == null)
                return Json(new { status = -1 }, JsonRequestBehavior.AllowGet);
            else
                try
                {
                    var wish = Session[cartSession];//bien wish co ten la wishSession
                    var p = db.products.Find(ProductId);
                    if (Quantity > p.quantity)
                        return Json(new
                        {
                            status = -3,
                            message = "Không đủ hàng"
                        });
                    //tim kiem san pham trong db, Id=1
                    if (wish != null)
                    {//gio da co sp
                        var list = (List<CartItem>)wish;
                        if (list.Exists(x => x.Product.id == ProductId))
                        {
                            foreach (var item in list)
                            {
                                if (item.Product.id == ProductId)
                                {
                                    if ((item.Quantity + Quantity) > p.quantity)
                                        return Json(new
                                        {
                                            status = -3,
                                            message = "Không đủ hàng"
                                        });
                                    item.Quantity += Quantity;
                                    return Json(new
                                    {
                                        status = 1,
                                        id = item.Product.id,
                                        image = item.Product.image,
                                        name = item.Product.name,
                                        price = Libary.Instances.convertVND(item.Product.promationprice > 0 ? item.Product.promationprice.ToString() : item.Product.price.ToString()),
                                        quantity = item.Quantity,
                                        alias = item.Product.alias,
                                        sumQuantity = list.Sum(x => x.Quantity),
                                        sumMoney = Libary.Instances.convertVND(list.Sum(x => x.Quantity * (x.Product.promationprice > 0 ? x.Product.promationprice : x.Product.price)).ToString())
                                    });
                                }

                            }
                        }
                        else
                        {
                            var item = new CartItem();
                            item.Product = p;
                            item.Quantity = Quantity;
                            list.Add(item);
                            Session[cartSession] = list; //save
                            return Json(new
                            {
                                status = 0,
                                id = item.Product.id,
                                image = item.Product.image,
                                name = item.Product.name,
                                price = Libary.Instances.convertVND((item.Product.promationprice > 0 ? item.Product.promationprice.ToString() : item.Product.price.ToString())),
                                quantity = item.Quantity,
                                alias = item.Product.alias,
                                sumQuantity = list.Sum(x => x.Quantity),
                                sumMoney = Libary.Instances.convertVND(list.Sum(x => x.Quantity * (x.Product.promationprice > 0 ? x.Product.promationprice : x.Product.price)).ToString())
                            });
                        }

                    }
                    else
                    { //gio hang moi
                        var item = new CartItem();
                        item.Product = p;
                        item.Quantity = Quantity;
                        var list = new List<CartItem>();
                        list.Add(item);
                        Session[cartSession] = list;//save 
                        return Json(new
                        {
                            status = 0,
                            id = item.Product.id,
                            image = item.Product.image,
                            name = item.Product.name,
                            price = Libary.Instances.convertVND((item.Product.promationprice > 0 ? item.Product.promationprice.ToString() : item.Product.price.ToString())),
                            quantity = item.Quantity,
                            alias = item.Product.alias,
                            sumQuantity = Quantity,
                            sumMoney = Libary.Instances.convertVND(list.Sum(x => x.Quantity * (x.Product.promationprice > 0 ? x.Product.promationprice : x.Product.price)).ToString())
                        });
                    }
                }
                catch (Exception ex)
                {
                }
            return Json(new { status = -2 }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult UpdateItem(int ProductId, int Quantity)
        {
            var cart = Session[cartSession];
            var p = db.products.Find(ProductId);
            if (Quantity > p.quantity)
                return Json(new
                {
                    status = -3,
                    message = "Không đủ hàng"
                });
            decimal? subtotal = 0;
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.id == ProductId))
                {
                    if (Quantity <= 0)
                        list.RemoveAll(r => r.Product.id == ProductId);
                    else
                        foreach (var item in list)
                        {
                            if (item.Product.id == ProductId)
                            {
                                if (Quantity > p.quantity)
                                    return Json(new
                                    {
                                        status = -3,
                                        message = "Không đủ hàng"
                                    });
                                item.Quantity = Quantity;
                                subtotal = Quantity * (item.Product.promationprice > 0 ? item.Product.promationprice : item.Product.price);
                            }
                        }
                }

                Session[cartSession] = list;
                return Json(new
                {
                    status = 1,
                    sumQuantity = list.Sum(x => x.Quantity),
                    sumMoney = Libary.Instances.convertVND(list.Sum(x => x.Quantity * (x.Product.promationprice > 0 ? x.Product.promationprice : x.Product.price)).ToString()),
                    total = Libary.Instances.convertVND(subtotal.ToString())
                });
            }
            return Json(new { status = 0 });


        }       
        [HttpPost]

        public JsonResult Pay(order order, string PaymentMethod, string BankCode)
        {
            order.createdate = DateTime.Parse(DateTime.Now.ToShortDateString());
            order.status = false;            
            order.idaccount = (Session["account"] as accounts).id;
            var cart = Session[cartSession] as List<CartItem>;
            if (PaymentMethod == "CASH")
            {
                order.statuspay = false;
                order.paymentmethod = 0;
            }
            else
            {
                order.statuspay = true;
                order.paymentmethod = 1;
            }
            if (PaymentMethod == "CASH")
            {
                #region body mail
                string body = @"<!DOCTYPE html>
<html lang='en'>
<head>
  <meta charset='UTF-8'>
  <meta http-equiv='X-UA-Compatible' content='IE=edge'>
  <meta name='viewport' content='width=device-width, initial-scale=1.0'>
  <title>Document</title>
  <style>
    body {
      padding: 4px;
      margin: 0;
      box-sizing: border-box;
      font-size: 1.2rem;

    }

    .container {
      border: 1px solid #ccc;

    }

    .table {
      border-top:1px solid #ccc;
      
    }

    .row {
      display: flex;
      justify-content: space-between;
    }

    .row>span {
      width: 20%;
      text-align: center;
    }

    .row span:nth-child(4) {
      width: 10%;

    }

    .row span:nth-child(2) {
      width: 30%;

    }

    .thead span {
      border-bottom: 3px solid #00bba6;
      line-height: 3rem;
    }

    .tbody .row>span {
      display: flex;
      line-height: 1.5rem;
      justify-content: center;
      align-items: center;
    }

    .border_right-none {
      border-right: none !important;
    }

    .row>span {
      border-right: 1px solid #ccc;
    }

    img {
      width: 100%;
    }

    .tbody .row>span>span {
      margin: auto;
    }

    .tbody .row {
      border-bottom: 1px solid #ccc;
    }
    

    .footer {
      padding: 8px;
    display: flex;
    }
    .footer_text{
      font-weight: 600;
      margin-right:auto ;
    }
    .header{
      padding: 4px;
      background: #00bba6;
    }
    .header h1,.header h3,.header h5,.header p{
      text-transform: uppercase;
      padding: 0;
      margin: 0;
      text-align: center;
      color: white;
      margin-bottom: 4px;
    }
    .header_detail{
          color: white;
          display: flex;
          flex-wrap: wrap;
        }
        .header_detail > div{
          width: 50%;
          text-align: center;
        }
  </style>
</head>

<body>
  <div class='container'>
    <div class='header'>
      <h1 >CÔNG TY NÓN BẢO HIỂM VIỆT TIN</h1>
      <h3>Thông tin đơn hàng</h3>
      <h5 style='margin:0'>Ngày đặt: " + DateTime.Now.ToString()+@"</h5>
      <div class='header_detail'>
        <div>
          <span>Họ tên:</span>
          <span>"+order.fullname+@"</span>
        </div>
        <div>
          <span>Email:</span>
          <span>"+order.email+@"</span>
        </div>       
      </div>
         <div class='header_detail'>        
        <div>
          <span>Số điện thoại:</span>
          <span>" + order.phone + @"</span>
        </div>
        <div>
          <span>Địa chỉ nhận hàng:</span>
          <span>" + order.address + @"</span>
        </div>
      </div>
    </div>
    <div class='table'>
      <div class='thead'>
        <div class='row'>
          <span>Ảnh</span>
          <span>Tên sản phẩm</span>
          <span>Giá</span>
          <span>Số lượng</span>
          <span class='border_right-none'>Thành tiền</span>
        </div>
      </div>
      <div class='tbody'>";
                #endregion
                db.order.Add(order);
                db.SaveChanges();
                int idorder = db.order.OrderByDescending(x => x.id).FirstOrDefault().id;
                var urlImage = string.Empty;
                foreach (var item in cart)
                {
                    urlImage = "https://nonbaohiem.ml/" + item.Product.image.Replace(" ","%20");
                    orderdetail orderdetail = new orderdetail();
                    orderdetail.idorder = idorder;
                    orderdetail.idproduct = item.Product.id;
                    orderdetail.quantity = item.Quantity;
                    orderdetail.price = (item.Product.promationprice > 0 ? item.Product.promationprice : item.Product.price);
                    #region body mail
                    body += @"
                        <div class='row'>
                  <span><img
                      src='"+ urlImage + @"'
                      alt='Error'></span>
                  <span><span>"+ item.Product.name + @"</span></span>
                  <span><span>"+ Libary.Instances.convertVND(orderdetail.price.ToString()) + @"</span></span>
                  <span><span>" + item.Quantity + @"</span></span>
                  <span class='border_right-none'><span>"+ Libary.Instances.convertVND((orderdetail.price * orderdetail.quantity).ToString()) + @"</span></span>
                </div>";
                    #endregion
                    db.orderdetail.Add(orderdetail);
                    db.SaveChanges();
                }
                #region body mail
                body += String.Format(@"</div>
    </div>
    <div class='footer'>
      <span class='footer_text'>
        Tổng sản phẩm
      </span>
      <span>
        {0}
      </span>
    </div>
    <div class='footer'>
      <span class='footer_text'>
        Tổng tiền
      </span>
      <span>
       {1}
      </span>
    </div>
    <div class='header'>
      <p>Xin cảm ơn quý khách đã ủng hộ shop.</p>
      <p style='margin:0;'>Hẹn gặp lại quý khách.</p>
      
    </div>
  </div>

</body>

</html>", cart.Sum(x => x.Quantity), Libary.Instances.convertVND(cart.Sum(x=>x.Quantity*(x.Product.promationprice>0? x.Product.promationprice:x.Product.price)).ToString()));
                #endregion
                Session[cartSession] = null;
                try
                {
                    Libary.Instances.sendMail("Thông tin đơn hàng " + idorder,order.email, body);
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        status = true,
                        message = "Lỗi gửi mail đơn hàng"
                    });
                }
                return Json(new
                {
                    status = true,
                    message = "Thanh toán thành công"
                });
            }
            else
            {
                var orderCurrent = db.order.OrderByDescending(x => x.id).FirstOrDefault();
                Session["order"] = order;
                var idorder = orderCurrent == null ? 1 : (orderCurrent.id + 1);
                var currentLink = ConfigurationManager.AppSettings["CurrentLink"];
                RequestInfo info = new RequestInfo();
                info.Merchant_id = merchantId;
                info.Merchant_password = merchantPassword;
                info.Receiver_email = merchantEmail;
                info.cur_code = "vnd";
                info.bank_code = BankCode;
                info.Order_code = idorder.ToString();
                info.Total_amount = cart.Sum(x => x.Quantity * (x.Product.promationprice > 0 ? x.Product.promationprice : x.Product.price)).ToString();
                info.fee_shipping = "0";
                info.Discount_amount = "0";
                info.order_description = "Thanh toán đơn hàng tại nonbaohiemviettin";
                info.return_url = currentLink + "xac-nhan-don-hang";
                info.cancel_url = currentLink + "gio-hang";
                info.Buyer_fullname = order.fullname;
                info.Buyer_email = order.email;
                info.Buyer_mobile = order.phone;

                APICheckoutV3 objNLChecout = new APICheckoutV3();
                ResponseInfo result = objNLChecout.GetUrlCheckout(info, PaymentMethod);
                if (result.Error_code == "00")
                {
                    return Json(new
                    {
                        status = true,
                        urlCheckout = result.Checkout_url,
                        message = result.Description
                    });
                }
                else
                    return Json(new
                    {
                        status = false,
                        message = result.Description
                    });
            }

        }
        [HandleError]

        public ActionResult ConfirmOrder()
        {
            string token = Request["token"];
            RequestCheckOrder info = new RequestCheckOrder();
            info.Merchant_id = merchantId;
            info.Merchant_password = merchantPassword;
            info.Token = token;
            APICheckoutV3 objNLChecout = new APICheckoutV3();
            ResponseCheckOrder result = objNLChecout.GetTransactionDetail(info);
            if (result.errorCode == "00")
            {
                var order = Session["order"] as order;
                var cart = Session[cartSession] as List<CartItem>;
                Session["order"] = null;
                #region body mail
                string body = @"<!DOCTYPE html>
<html lang='en'>
<head>
  <meta charset='UTF-8'>
  <meta http-equiv='X-UA-Compatible' content='IE=edge'>
  <meta name='viewport' content='width=device-width, initial-scale=1.0'>
  <title>Document</title>
  <style>
    body {
      padding: 4px;
      margin: 0;
      box-sizing: border-box;
      font-size: 1.2rem;

    }

    .container {
      border: 1px solid #ccc;

    }

    .table {
      border-top:1px solid #ccc;
      
    }

    .row {
      display: flex;
      justify-content: space-between;
    }

    .row>span {
      width: 20%;
      text-align: center;
    }

    .row span:nth-child(4) {
      width: 10%;

    }

    .row span:nth-child(2) {
      width: 30%;

    }

    .thead span {
      border-bottom: 3px solid #00bba6;
      line-height: 3rem;
    }

    .tbody .row>span {
      display: flex;
      line-height: 1.5rem;
      justify-content: center;
      align-items: center;
    }

    .border_right-none {
      border-right: none !important;
    }

    .row>span {
      border-right: 1px solid #ccc;
    }

    img {
      width: 100%;
    }

    .tbody .row>span>span {
      margin: auto;
    }

    .tbody .row {
      border-bottom: 1px solid #ccc;
    }
    

    .footer {
      padding: 8px;
    display: flex;
    }
    .footer_text{
      font-weight: 600;
      margin-right:auto ;
    }
    .header{
      padding: 4px;
      background: #00bba6;
    }
    .header h1,.header h3,.header h5,.header p{
      text-transform: uppercase;
      padding: 0;
      margin: 0;
      text-align: center;
      color: white;
      margin-bottom: 4px;
    }
    .header_detail{
          color: white;
          display: flex;
          flex-wrap: wrap;
        }
        .header_detail > div{
          width: 50%;
          text-align: center;
        }
  </style>
</head>

<body>
  <div class='container'>
    <div class='header'>
      <h1 >CÔNG TY NÓN BẢO HIỂM VIỆT TIN</h1>
      <h3>Thông tin đơn hàng</h3>
      <h5 style='margin:0'>Ngày đặt: " + DateTime.Now.ToString() + @"</h5>
      <div class='header_detail'>
        <div>
          <span>Họ tên:</span>
          <span>" + order.fullname + @"</span>
        </div>
        <div>
          <span>Email:</span>
          <span>" + order.email + @"</span>
        </div>       
      </div>
         <div class='header_detail'>        
        <div>
          <span>Số điện thoại:</span>
          <span>" + order.phone + @"</span>
        </div>
        <div>
          <span>Địa chỉ nhận hàng:</span>
          <span>" + order.address + @"</span>
        </div>
      </div>
    </div>
    <div class='table'>
      <div class='thead'>
        <div class='row'>
          <span>Ảnh</span>
          <span>Tên sản phẩm</span>
          <span>Giá</span>
          <span>Số lượng</span>
          <span class='border_right-none'>Thành tiền</span>
        </div>
      </div>
      <div class='tbody'>";
                #endregion
                db.order.Add(order);
                int idorder = db.order.OrderByDescending(x => x.id).FirstOrDefault().id;
                var urlImage = string.Empty;
                foreach (var item in cart)
                {
                    orderdetail orderdetail = new orderdetail();
                    urlImage = "https://nonbaohiem.ml/" + item.Product.image.Replace(" ", "%20");
                    orderdetail.idorder = idorder;
                    orderdetail.idproduct = item.Product.id;
                    orderdetail.quantity = item.Quantity;
                    orderdetail.price = (item.Product.promationprice > 0 ? item.Product.promationprice : item.Product.price);
                    #region body mail
                    body += @"
                        <div class='row'>
                  <span><img
                      src='" + urlImage + @"'
                      alt='Error'></span>
                  <span><span>" + item.Product.name + @"</span></span>
                  <span><span>" + Libary.Instances.convertVND(orderdetail.price.ToString()) + @"</span></span>
                  <span><span>" + item.Quantity + @"</span></span>
                  <span class='border_right-none'><span>" + Libary.Instances.convertVND((orderdetail.price * orderdetail.quantity).ToString()) + @"</span></span>
                </div>";
                    #endregion
                    db.orderdetail.Add(orderdetail);
                    db.SaveChanges();
                }
                #region body mail
                body += String.Format(@"</div>
    </div>
    <div class='footer'>
      <span class='footer_text'>
        Tổng sản phẩm
      </span>
      <span>
        {0}
      </span>
    </div>
    <div class='footer'>
      <span class='footer_text'>
        Tổng tiền
      </span>
      <span>
       {1}
      </span>
    </div>
    <div class='header'>
      <p>Xin cảm ơn quý khách đã ủng hộ shop.</p>
      <p style='margin:0;'>Hẹn gặp lại quý khách.</p>
      
    </div>
  </div>

</body>

</html>", cart.Sum(x => x.Quantity), Libary.Instances.convertVND(cart.Sum(x => x.Quantity * (x.Product.promationprice > 0 ? x.Product.promationprice : x.Product.price)).ToString()));
                #endregion
                Session[cartSession] = null;
                try
                {
                    Libary.Instances.sendMail("Thông tin đơn hàng " + idorder,order.email, body);
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        status = true,
                        message = "Lỗi gửi mail đơn đặt hàng."
                    });
                }
                ViewBag.idorder = idorder;
                ViewBag.Result = "Thanh toán thành công. Chúng tôi sẽ liên hệ lại sớm nhất.";
            }
            else
            {
                ViewBag.Result = "Có lỗi xảy ra. Vui lòng liên hệ admin.";
            }
            return View();
        }

        [HttpPost]
        public JsonResult delete_order(int id)
        {
            try
            {
                var order = db.order.Find(id);
                if (order.paymentmethod == 0 && order.statuspay == false)
                    foreach (var item in db.orderdetail.Where(x => x.idorder == order.id).ToList())
                    {
                        var products = db.products.Find(item.idproduct);
                        products.quantity += item.quantity;
                        db.Entry(products).State = EntityState.Modified;
                    }
                db.order.Remove(order);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = 0,
                    message = "Có lỗi trong quá trình xoá.Vui lòng thử lại."
                });
            }

            return Json(new
            {
                status = 1,
                message = "Huỷ đơn hàng thành công."
            });
        }
    }
}