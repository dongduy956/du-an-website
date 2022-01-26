using NONBAOHIEMVIETTIN.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NONBAOHIEMVIETTIN.Areas.admin.Controllers
{
    public class Rate_adminController : BaseController
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void ViewBagNoti(List<rate> temp, int page)
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
            var temp = db.rate.ToList();
            var rate = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            ViewBag.check = true;
            return View(rate);
        }
        public ActionResult Search(int page = 1)
        {
            var keyword = Request["tukhoa"];
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            ViewBag.check = false;
            var temp = db.rate.Where(x =>
            x.id.ToString().ToLower().Equals(keyword.ToLower().Trim()) ||
            x.accounts.username.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.comment.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.products.name.ToLower().Contains(keyword.ToLower().Trim()) ||
           DateTime.Parse(x.createdate.ToString()).ToShortDateString().ToLower().Equals(keyword.ToLower().Trim())
            ).ToList();
            var rate = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index", rate);
        }
        [HttpPost]
        public JsonResult delete_rate(int id)
        {
            try
            {
                var rate = db.rate.Find(id);
                db.rate.Remove(rate);
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