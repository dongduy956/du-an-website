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
using System.Data.Entity;
using log4net;

namespace NONBAOHIEMVIETTIN.Controllers
{
    public class CartController : Controller
    {
        private static readonly ILog log =
          LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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

        public JsonResult Pay(order order, string PaymentMethod)
        {
            order.createdate = DateTime.Now;
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
                db.SaveChanges();
                int idorder = db.order.OrderByDescending(x => x.id).FirstOrDefault().id;
                var urlImage = string.Empty;
                foreach (var item in cart)
                {
                    urlImage = "https://nonbaohiem.ml/" + item.Product.image.Replace(" ", "%20");
                    orderdetail orderdetail = new orderdetail();
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
                    Libary.Instances.sendMail("Thông tin đơn hàng " + idorder, order.email, body);
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
                //Get Config Info
                string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
                string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
                string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma website
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                var orderCurrent = db.order.OrderByDescending(x => x.id).FirstOrDefault();
                var idorder = orderCurrent == null ? 1 : (orderCurrent.id + 1);

                //Save order to db
                order.createdate = DateTime.Now;
                order.status = false;
                order.idaccount = (Session["account"] as accounts).id;
                order.total = cart.Sum(x => x.Quantity * (x.Product.promationprice > 0 ? x.Product.promationprice : x.Product.price));
                Session["order"] = order;

                //Build URL for VNPAY
                VnPayLibrary vnpay = new VnPayLibrary();

                vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
                vnpay.AddRequestData("vnp_Command", "pay");
                vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
                vnpay.AddRequestData("vnp_Amount", (order.total * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000

                // vnpay.AddRequestData("vnp_BankCode", cboBankCode.SelectedItem.Value);
                vnpay.AddRequestData("vnp_CreateDate", order.createdate?.ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_CurrCode", "VND");
                vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
                vnpay.AddRequestData("vnp_Locale", "vn");

                vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + idorder);
                vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
                vnpay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

                //Add Params of 2.1.0 Version
                vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));
                //Billing
                vnpay.AddRequestData("vnp_Bill_Mobile", order.phone.Trim());
                vnpay.AddRequestData("vnp_Bill_Email", order.email.Trim());
                var fullName = order.fullname.Trim();
                if (!String.IsNullOrEmpty(fullName))
                {
                    try
                    {
                        var indexof = fullName.IndexOf(' ');
                        vnpay.AddRequestData("vnp_Bill_FirstName", fullName.Substring(0, indexof));
                        vnpay.AddRequestData("vnp_Bill_LastName", fullName.Substring(indexof + 1, fullName.Length - indexof - 1));
                    }
                    catch (Exception ex)
                    {

                    }
                    
                }
                vnpay.AddRequestData("vnp_Bill_Address", order.address.Trim());
                vnpay.AddRequestData("vnp_Bill_Country", "Việt Nam");
                vnpay.AddRequestData("vnp_Bill_State", "");

                // Invoice

                vnpay.AddRequestData("vnp_Inv_Phone", order.phone.Trim());
                vnpay.AddRequestData("vnp_Inv_Email", order.email.Trim());
                vnpay.AddRequestData("vnp_Inv_Customer", order.fullname.Trim());
                vnpay.AddRequestData("vnp_Inv_Address", order.address.Trim());

                string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
                log.InfoFormat("VNPAY URL: {0}", paymentUrl);
                return Json(new
                {
                    status = true,
                    urlCheckout = paymentUrl
                });
            }

        }
        [HandleError]

        public ActionResult ConfirmOrder()
        {
            log.InfoFormat("Begin VNPAY Return, URL={0}", Request.RawUrl);
            if (Request.QueryString.Count > 0)
            {
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                var vnpayData = Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
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
                            db.SaveChanges();

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
                            Libary.Instances.sendMail("Thông tin đơn hàng " + idorder, order.email, body);
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
                        ViewBag.Result = "Thanh toán thành công. Vui lòng check email đơn hàng vừa đặt.<br>Chúng tôi sẽ liên hệ lại sớm hơn.<br>Xin cảm ơn quý khách và hẹn gặp lại.";
                    }
                    else
                    {
                        ViewBag.Result = "Thanh toán thất bại. Vui lòng thực hiện lại thao tác.";
                    }
                   
                }
                else
                {
                    ViewBag.Result = "Có lỗi xảy ra. Vui lòng liên hệ admin.";


                }
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