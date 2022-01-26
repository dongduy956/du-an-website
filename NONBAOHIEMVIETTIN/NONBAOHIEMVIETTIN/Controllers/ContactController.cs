using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NONBAOHIEMVIETTIN.Models;
namespace NONBAOHIEMVIETTIN.Controllers
{
    public class ContactController : Controller
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        // GET: Contact
        public ActionResult Index()
        {
            var contact = db.contact.FirstOrDefault(x => x.display == true);
            return View(contact);
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult feedback(feedback send)
        {
            try
            {
                db.feedback.Add(send);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = 0,
                    message="Có lỗi trong quá trình xử lý!!"
                });
            }
            return Json(new
            {
                status=1,
                message = "Gửi thông tin liên hệ thành công."            
            });
        }
    }
}