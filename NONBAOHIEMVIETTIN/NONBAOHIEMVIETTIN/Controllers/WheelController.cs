using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NONBAOHIEMVIETTIN.Models;
using System.Data.Entity;
using System.Configuration;
using log4net;

namespace NONBAOHIEMVIETTIN.Controllers
{
    public class WheelController : Controller
    {
        private static readonly ILog log =
         LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const int COIN = 5;
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        // GET: Wheel
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult getNewWheel()
        {
            try
            {
                var id = int.Parse(Session["wheel"].ToString());
                var wheels = db.wheel.Where(x => x.id > id && (x.idpromotion != null || x.gift_name.Contains("xu")));
                if (wheels.Count() > 0)
                {
                    var list_gift = from w in wheels
                                    select new
                                    {
                                        w.accounts.fullname,
                                        w.gift_name
                                    };
                    Session["wheel"] = db.wheel.OrderByDescending(x => x.id).FirstOrDefault().id;
                    return Json(new
                    {
                        status = true,
                        list_gift
                    });
                }
            }
            catch (Exception exx)
            {


            }
            return Json(new
            {
                status = false
            });
        }
        [HttpPost]
        public JsonResult checkLogin()
        {
            if (Session["account"] != null)
                return Json(new
                {
                    status = true
                });
            return Json(new
            {
                status = false,
                message = "Bạn chưa đăng nhập."
            });
        }
        [HttpPost]
        public JsonResult Attendance()
        {
            var acc_temp = Session["account"] as accounts;
            if (acc_temp.date_attendance != null)
            {
                var date_temp = (DateTime)acc_temp.date_attendance;
                if (date_temp.Day == DateTime.Now.Day
                && date_temp.Month == DateTime.Now.Month
                && date_temp.Year == DateTime.Now.Year)
                    return Json(new
                    {
                        status = false,
                        message = "Bạn đã điểm danh rồi."
                    });
            }
            var acc = db.accounts.Find(acc_temp.id);
            acc.spin += 1;
            acc.date_attendance = DateTime.Now;
            db.Entry(acc).State = EntityState.Modified;
            db.SaveChanges();
            Session["account"] = acc;
            return Json(new
            {
                status = true,
                message = "Điểm danh thành công.",
                spin = acc.spin
            });
        }
        [HttpPost]
        public JsonResult CheckSpin()
        {
            var acc_temp = Session["account"] as accounts;
            bool checkSpin = true;//true: quay bằng lượt; false: quay bằng xu
            if (acc_temp.spin == 0)
            {
                if (acc_temp.coin < COIN)
                    return Json(new
                    {
                        status = false,
                        message = "Bạn đã hết lượt quay."
                    });
                checkSpin = false;
            }
            var acc = db.accounts.Find(acc_temp.id);
            if (checkSpin)
                acc.spin -= 1;
            else
                acc.coin -= 5;
            db.Entry(acc).State = EntityState.Modified;
            db.SaveChanges();
            Session["account"] = acc;
            if (checkSpin)
                return Json(new
                {
                    status = true,
                    spin = acc.spin,
                    checkSpin
                });
            return Json(new
            {
                status = true,
                coin = acc.coin,
                checkSpin
            });
        }

        [HttpPost]
        public JsonResult AddGift(int key, string name)
        {
            var acc_temp = Session["account"] as accounts;
            if (key == 0)
            {
                var acc = db.accounts.Find(acc_temp.id);
                acc.spin += 1;
                Session["account"] = acc;
                db.Entry(acc).State = EntityState.Modified;
            }
            else
            if (key == 100)
            {
                var acc = db.accounts.Find(acc_temp.id);
                acc.coin += 100;
                db.Entry(acc).State = EntityState.Modified;
                Session["account"] = acc;
            }
            promotion promotion = null;
            int? idpromotion = null;
            if (key > 0 && key != 100)
            {
                var _name = "Giảm giá vòng quay " + DateTime.Now.Ticks;
                db.promotion.Add(new promotion()
                {
                    alias = Libary.Instances.convertToUnSign3(_name),
                    code = Libary.Instances.randCode(10),
                    create_by = acc_temp.id,
                    create_date = DateTime.Now,
                    discount = new Random().Next(1, 50),
                    end_date = DateTime.Now.AddDays(key),
                    name = _name,
                    quantity_use = 1,
                    start_date = DateTime.Now
                });
                db.SaveChanges();
                promotion = db.promotion.OrderByDescending(x => x.id).FirstOrDefault();
                idpromotion = promotion.id;
            }
            db.wheel.Add(new wheel()
            {
                idaccount = acc_temp.id,
                gift_name = name,
                create_date = DateTime.Now,
                idpromotion = idpromotion
            });
            db.SaveChanges();
            if (promotion != null)
                return Json(new
                {
                    status = true,
                    code = promotion.code,
                    discount = promotion.discount
                });
            if (key == 100)
                return Json(new
                {
                    status = true,
                    coin = (Session["account"] as accounts).coin
                });
            if (key == 0)
                return Json(new
                {
                    status = true,
                    spin = (Session["account"] as accounts).spin
                });
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]

        public JsonResult Recharge(int amount_money)
        {
            var acc = Session["account"] as accounts;
            var history_recharge = new history_recharge()
            {
                idaccount = acc.id,
                amount_money = amount_money,
            };
            //Get Config Info
            string vnp_Returnurl_recharge = ConfigurationManager.AppSettings["vnp_Returnurl-recharge"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma website
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
            var orderCurrent = "Nạp tiền " + amount_money + "-" + DateTime.Now.Ticks.ToString();
            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (amount_money * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000

            // vnpay.AddRequestData("vnp_BankCode", cboBankCode.SelectedItem.Value);
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");

            vnpay.AddRequestData("vnp_OrderInfo", "Nạp tiền " + amount_money + "-" + DateTime.Now.Ticks.ToString());
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl_recharge);
            vnpay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));
            //Billing
            vnpay.AddRequestData("vnp_Bill_Mobile", acc.phone ?? "");
            vnpay.AddRequestData("vnp_Bill_Email", acc.email ?? "");
            var fullName = acc.fullname;
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
            vnpay.AddRequestData("vnp_Bill_Address", acc.address ?? "");
            vnpay.AddRequestData("vnp_Bill_Country", "Việt Nam");
            vnpay.AddRequestData("vnp_Bill_State", "");
            // Invoice
            vnpay.AddRequestData("vnp_Inv_Phone", acc.phone ?? "");

            vnpay.AddRequestData("vnp_Inv_Email", acc.email ?? "");
            vnpay.AddRequestData("vnp_Inv_Customer", acc.fullname ?? "");
            vnpay.AddRequestData("vnp_Inv_Address", acc.address ?? "");
            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            Session["history_recharge"] = history_recharge;
            return Json(new
            {
                status = true,
                urlCheckout = paymentUrl
            });
        }
        [HandleError]

        public ActionResult ConfirmRecharge()
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
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        var history_recharge = Session["history_recharge"] as history_recharge;
                        history_recharge.create_date = DateTime.Now;
                        db.history_recharge.Add(history_recharge);
                        db.SaveChanges();
                        Session["account"] = db.accounts.Find(history_recharge.idaccount);
                        Session["history_recharge"] = null;
                        ViewBag.idrecharge = db.history_recharge.OrderByDescending(x => x.id).FirstOrDefault().id;
                        ViewBag.Result = "Nạp tiền thành công. Quý khách vui lòng kiểm tra xu trong tài khoản.<br>Xin cảm ơn quý khách và hẹn gặp lại.";
                    }
                    else
                    {
                        ViewBag.Result = "Nạp tiền thất bại. Vui lòng thực hiện lại thao tác.";
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
        public JsonResult Withdraw(string bank_number, string bank_name, string fullname, int amount_money)
        {
            var acc = Session["account"] as accounts;
            if (acc.coin < amount_money)
                return Json(new
                {
                    status = false,
                    message = "Xu không đủ để rút."
                });
            db.history_withdraw.Add(new history_withdraw()
            {
                create_date = DateTime.Now,
                idaccount = acc.id,
                status = 0,
                amount_money = amount_money * 1000,
                bank_name = bank_name,
                bank_number = bank_number,
                fullname = fullname,
            });
            db.SaveChanges();
            Session["account"] = db.accounts.Find(acc.id);
            return Json(new
            {
                status = true,
                message = "Đặt lệnh rút tiền thành công.",
                coin = (acc.coin - amount_money)
            });
        }
    }
}