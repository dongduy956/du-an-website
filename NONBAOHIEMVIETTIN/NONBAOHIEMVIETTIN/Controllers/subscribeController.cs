using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NONBAOHIEMVIETTIN.Models;
namespace NONBAOHIEMVIETTIN.Controllers
{
    public class subscribeController : Controller
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        // GET: subscribe
        [HttpPost]
        public JsonResult news(subscribe sub)
        {
            var subscribe = db.subscribe.SingleOrDefault(x => x.email.Equals(sub.email));
            if (subscribe != null)
                return Json(new {
                    status = 0,
                    message = "Email này đã đăng kí nhận tin rồi!!"
                });
            try
            {
                db.subscribe.Add(sub);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    status = -1,
                    message = "Có lỗi trong quá trình xử lý!!!"
                });
            }
            return Json(new
            {
                status = 1,
                message = "Chúc mừng bạn đã đăng kí thành công!!"
            });
        }
    }
}