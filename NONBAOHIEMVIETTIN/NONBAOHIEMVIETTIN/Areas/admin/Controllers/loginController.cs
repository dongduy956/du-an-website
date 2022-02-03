using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NONBAOHIEMVIETTIN.Areas.admin.Models;
using NONBAOHIEMVIETTIN.Models;
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
                return Redirect("/admin");
            return View();
        }
        public ActionResult Logout()
        {
            Session["account_admin"] = null;            
            return Redirect("/admin/dang-nhap.html");
        }
        [HttpPost]
        public JsonResult Login(AccountLogin accLogin)
        {
            var password = HoTro.Instances.EncodeMD5(accLogin.password);
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