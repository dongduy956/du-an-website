using NONBAOHIEMVIETTIN.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NONBAOHIEMVIETTIN.Areas.admin.Controllers
{
    public class Feedback_adminController : BaseController
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void ViewBagNoti(List<feedback> temp, int page)
        {
            ViewBag.last = 1;
            if (temp.Count() > 0)
            {
                int last = int.Parse(Math.Ceiling((double)temp.Count() / pageSize).ToString());
                ViewBag.last = last;
                ViewBag.noti = "Showing " + page + "-" + last + " of " + temp.Count() + " results";
            }
        }
        public ActionResult Index(int page = 1)
        {
            var temp = db.feedback.ToList();
            var sendcontactinfo = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            ViewBag.check = true;
            return View(sendcontactinfo);
        }
        public ActionResult Search(int page = 1)
        {
            var keyword = Request["tukhoa"];
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            ViewBag.check = false;
            var temp = db.feedback.Where(x =>
            x.name.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.subject.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.id.ToString().ToLower().Equals(keyword.ToLower().Trim()) ||
            x.phone.ToString().ToLower().Equals(keyword.ToLower().Trim()) ||
            x.email.ToLower().Equals(keyword.ToLower().Trim())
            ).ToList();
            var news = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index", news);
        }
        [HttpPost]
        public JsonResult delete_feedback(int id)
        {
            try
            {
                var sendcontactinfo = db.feedback.Find(id);
                db.feedback.Remove(sendcontactinfo);
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
                message = "Xoá thành công."
            });
        }
    }
}