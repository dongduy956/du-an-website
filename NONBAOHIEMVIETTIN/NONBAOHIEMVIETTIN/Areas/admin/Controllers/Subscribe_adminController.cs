using NONBAOHIEMVIETTIN.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NONBAOHIEMVIETTIN.Areas.admin.Controllers
{
    public class Subscribe_adminController : Controller
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void ViewBagNoti(List<subscribe> temp, int page)
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
            var temp = db.subscribe.ToList();
            var subscribe = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            ViewBag.check = true;
            return View(subscribe);
        }
        public ActionResult Search(int page = 1)
        {
            var keyword = Request["tukhoa"];
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            ViewBag.check = false;
            var temp = db.subscribe.Where(x =>            
            x.id.ToString().ToLower().Equals(keyword.ToLower().Trim()) ||
            x.email.ToLower().Equals(keyword.ToLower().Trim())
            ).ToList();
            var subscribe = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index", subscribe);
        }
        [HttpPost]
        public JsonResult delete_subscribe(int id)
        {
            try
            {
                var subscribe = db.subscribe.Find(id);
                db.subscribe.Remove(subscribe);
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