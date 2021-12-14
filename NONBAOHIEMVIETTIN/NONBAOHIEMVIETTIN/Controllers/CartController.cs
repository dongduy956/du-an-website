using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NONBAOHIEMVIETTIN.Models;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace NONBAOHIEMVIETTIN.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public const string cartSession = "cartSession";
        private nonbaohiemviettinEntities  db = new nonbaohiemviettinEntities();
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
                return Json(new { status = 1, count = list.Sum(x => x.Quantity), summoney = HoTro.Instances.convertVND(list.Sum(x => x.Quantity * x.Product.price).ToString()), subtotal = HoTro.Instances.convertVND(subtotal.ToString()) }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);


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
        public JsonResult Pay()
        {
            if (sendMail(Session["account"] as accounts))
            {
                Session[cartSession] = null;
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);

        }
    }
}