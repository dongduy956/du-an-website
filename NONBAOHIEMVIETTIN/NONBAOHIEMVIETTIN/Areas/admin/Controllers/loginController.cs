using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NONBAOHIEMVIETTIN.Areas.admin.Models;
using NONBAOHIEMVIETTIN.Models;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace NONBAOHIEMVIETTIN.Areas.admin.Controllers
{
    public class LoginController : Controller
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        // GET: admin/Login
        public ActionResult Index()
        {
            var acc = Session["account_admin"] as accounts;
            if (acc != null && acc.role.name.Equals("admin"))
                return Redirect("/");
            return View();
        }
        public ActionResult Logout()
        {
            Session["account_admin"] = null;            
            return Redirect("/dang-nhap");
        }
        private bool IsValidRecaptcha(string recaptcha)
        {
            if (string.IsNullOrEmpty(recaptcha))
            {
                return false;
            }
            var secretKey = "6LfSna0eAAAAAFKwKXzLSajQz835jJn2xZBzqtyY";//Mã bí mật
            string remoteIp = Request.ServerVariables["REMOTE_ADDR"];
            string myParameters = String.Format("secret={0}&response={1}&remoteip={2}", secretKey, recaptcha, remoteIp);
            RecaptchaResult captchaResult;
            using (var wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var json = wc.UploadString("https://www.google.com/recaptcha/api/siteverify", myParameters);
                var js = new DataContractJsonSerializer(typeof(RecaptchaResult));
                var ms = new MemoryStream(Encoding.ASCII.GetBytes(json));
                captchaResult = js.ReadObject(ms) as RecaptchaResult;
                if (captchaResult != null && captchaResult.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        [HttpPost]
        public ActionResult Login(AccountLogin accLogin,string recaptcha)
        {
            var password = Libary.Instances.EncodeMD5(accLogin.password);
            if (!IsValidRecaptcha(recaptcha))
            {

                return Json(new
                {
                    status = -2,
                    message = "Bạn chưa xác nhận không phải là người máy!"
                });
            }
            var acc = db.accounts.SingleOrDefault(x => x.username.Equals(accLogin.username) && x.password.Equals(password));
            if (acc == null)
                return Json(new
                {
                    status = -1,
                    message = "Tài khoản hoặc mật khẩu không chính xác!"
                });
            if (!acc.role.name.Equals("admin"))
                return Json(new
                {
                    status = 0,
                    message = "Tài khoản này không phải admin!"
                });
            if (!bool.Parse(acc.status.ToString()))
                return Json(new
                {
                    status = 0,
                    message = "Tài khoản này bị khoá. Vui lòng liên hệ admin để biết thêm chi tiết!"
                });
            Session["account_admin"] = acc;
            return Json(new
            {
                status = 1,
                message = "Đăng nhập thành công!!"
            });
        }       
    }
}