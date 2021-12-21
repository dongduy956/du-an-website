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
                    return Json(new { status = 1, sumQuantity = list.Sum(x => x.Quantity), sumMoney = HoTro.Instances.convertVND(list.Sum(x => x.Quantity * x.Product.price).ToString()) }, JsonRequestBehavior.AllowGet);
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
                                    item.Quantity += Quantity;
                                    return Json(new
                                    {
                                        status = 1,
                                        id = item.Product.id,
                                        image = item.Product.image,
                                        name = item.Product.name,
                                        price = HoTro.Instances.convertVND(item.Product.price.ToString()),
                                        quantity = item.Quantity,
                                        alias = item.Product.alias,
                                        sumQuantity = list.Sum(x => x.Quantity),
                                        sumMoney = HoTro.Instances.convertVND(list.Sum(x => x.Quantity * x.Product.price).ToString())
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
                                price = HoTro.Instances.convertVND(item.Product.price.ToString()),
                                quantity = item.Quantity,
                                alias = item.Product.alias,
                                sumQuantity = list.Sum(x => x.Quantity),
                                sumMoney = HoTro.Instances.convertVND(list.Sum(x => x.Quantity * x.Product.price).ToString())
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
                            price = HoTro.Instances.convertVND(item.Product.price.ToString()),
                            quantity = item.Quantity,
                            alias = item.Product.alias,
                            sumQuantity = Quantity,
                            sumMoney = HoTro.Instances.convertVND(list.Sum(x => x.Quantity * x.Product.price).ToString())
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
                                item.Quantity = Quantity;
                                subtotal = Quantity * item.Product.price;
                            }
                        }
                }

                Session[cartSession] = list;
                return Json(new
                {
                    status = 1,
                    sumQuantity = list.Sum(x => x.Quantity),
                    sumMoney = HoTro.Instances.convertVND(list.Sum(x => x.Quantity * x.Product.price).ToString()),
                    total = HoTro.Instances.convertVND(subtotal.ToString())
                });
            }
            return Json(new { status = 0 });


        }
        string taoCode()
        {
            string val = string.Empty;
            Random r = new Random();
            for (int i = 0; i < 13; i++)
                val += r.Next(0, 9);
            return val;
        }
        bool sendMail(accounts acc)
        {
            // //đăng nhập mail để gửi
            string email = ConfigurationManager.AppSettings["mail"].ToString();
            string pass = ConfigurationManager.AppSettings["pass"].ToString();

            //gán thông tin
            var mess = new MailMessage(email, acc.email);
            mess.Subject = "Đây là mail tự động.";
            string text = string.Empty;
            int i = 1;
            var list = (Session[cartSession] as List<CartItem>);
            text = @"<table align='center' border='1' cellpadding='1' cellspacing='5' style='width:1000px'>
	                <thead>
		                <tr>
			                <th scope='row'>STT</th>
			                <th scope='col'>T&ecirc;n sản phẩm</th>
			                <th scope='col'>Gi&aacute;</th>
			                <th scope='col'>Số lượng</th>
			                <th scope='col'>Th&agrave;nh tiền</th>
		                </tr>
	                </thead>
	                <tbody>";
            ////tạo 1 hoá đơn
            //Invoices hd = new Invoices();
            //hd.Code = taoCode();
            //hd.AccountId = acc.Id;
            //hd.IssuedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            //hd.ShippingAddress = acc.Address;
            //hd.ShippingPhone = acc.Phone;
            //hd.Total = int.Parse(list.Sum(x => x.Quantity * x.Product.Price).ToString());
            //hd.Status = true;
            //db.Invoices.Add(hd);
            //db.SaveChanges();
            //int idHD = db.Invoices.OrderByDescending(x => x.Id).FirstOrDefault().Id;
            //foreach (var item in list)
            //{
            //    //thêm data vào table carts
            //    Carts cart = new Carts();
            //    cart.ProductId = item.Product.Id;
            //    cart.AccountId = acc.Id;
            //    cart.Quantity = item.Quantity;
            //    db.Carts.Add(cart);
            //    db.SaveChanges();
            //    //thêm data vào chi tiết hoá đơn
            //    InvoiceDetails cthd = new InvoiceDetails();
            //    cthd.InvoiceId = idHD;
            //    cthd.ProductId = item.Product.Id;
            //    cthd.Quantity = item.Quantity;
            //    cthd.UnitPrice = int.Parse(item.Product.Price.ToString());
            //    db.InvoiceDetails.Add(cthd);
            //    db.SaveChanges();
            //    text += String.Format(@"<tr><th scope='row'>{0}</th><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>",
            //    i, item.Product.Productname, HoTro.Instances.convertVND(item.Product.Price.ToString()), item.Quantity, HoTro.Instances.convertVND((item.Quantity * item.Product.Price).ToString()));
            //    i++;
            //}
            text += String.Format(@"</tbody></table><p align='right'>Tổng tiền:{0}</p>", HoTro.Instances.convertVND(list.Sum(x => x.Quantity * x.Product.price).ToString()));
            mess.Body = text;
            //cho gửi định dạng html
            mess.IsBodyHtml = true;
            //cấu hình mail
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            //gửi mail đi
            NetworkCredential net = new NetworkCredential(email, pass);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = net;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                smtp.Send(mess);
            }
            catch (SmtpException ex)
            {
                return false;
            }
            return true;

        }
        [HttpPost]
        public ActionResult Pay(order order, string PaymentMethod, string BankCode)
        {       
                order.createdate = DateTime.Parse(DateTime.Now.ToShortDateString());
                order.status = false;
                order.idaccount = (Session["account"] as accounts).id;
                db.order.Add(order);
                db.SaveChanges();
                int idorder = db.order.OrderByDescending(x => x.id).FirstOrDefault().id;
                var cart = Session[cartSession] as List<CartItem>;
                foreach (var item in cart)
                {
                orderdetail odetail = new orderdetail();
                    odetail.idorder = idorder;
                    odetail.idproduct = item.Product.id;
                    odetail.quantity = item.Quantity;
                    odetail.price = item.Product.price;
                    db.orderdetail.Add(odetail);
                    db.SaveChanges();
                }            
            if (PaymentMethod == "CASH")
            {
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                var currentLink = ConfigurationManager.AppSettings["CurrentLink"];
                RequestInfo info = new RequestInfo();
                info.Merchant_id = merchantId;
                info.Merchant_password = merchantPassword;
                info.Receiver_email = merchantEmail;
                info.cur_code = "vnd";
                info.bank_code = BankCode;
                info.Order_code = idorder.ToString();
                info.Total_amount = db.orderdetail.Where(x => x.idorder == idorder).Sum(x => x.subtotal).ToString();
                info.fee_shipping = "0";
                info.Discount_amount = "0";
                info.order_description = "Thanh toán đơn hàng tại nonbaohiemviettin";
                info.return_url = currentLink + "xac-nhan-don-hang.html";
                info.cancel_url = currentLink + "huy-don-hang.html";

                info.Buyer_fullname = order.fullname;
                info.Buyer_email = order.email;
                info.Buyer_mobile = order.phone;

                APICheckoutV3 objNLChecout = new APICheckoutV3();
                ResponseInfo result = objNLChecout.GetUrlCheckout(info,PaymentMethod);
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
                ////update status order
                //_orderService.UpdateStatus(int.Parse(result.order_code));
                //_orderService.Save();
                //ViewBag.IsSuccess = true;
                ViewBag.Result = "Thanh toán thành công. Chúng tôi sẽ liên hệ lại sớm nhất.";
            }
            else
            {
                ViewBag.IsSuccess = true;
                ViewBag.Result = "Có lỗi xảy ra. Vui lòng liên hệ admin.";
            }
            return View();
        }
        public ActionResult CancelOrder()
        {
            return View();
        }
    }
}