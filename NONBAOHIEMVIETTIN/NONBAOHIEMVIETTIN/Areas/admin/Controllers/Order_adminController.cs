using NONBAOHIEMVIETTIN.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NONBAOHIEMVIETTIN.Areas.admin.Controllers
{
    public class Order_adminController : BaseController
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void ViewBagNoti(List<order> temp, int page)
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
            var temp = db.order.ToList();
            var order = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            ViewBag.check = true;
            return View(order);
        }
        public ActionResult Search(int page = 1)
        {
            var keyword = Request["tukhoa"];
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            ViewBag.check = false;
            var temp = db.order.Where(x =>
            x.id.ToString().ToLower().Equals(keyword.ToLower().Trim()) ||
            x.accounts.username.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.email.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.fullname.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.note.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.phone.ToLower().Equals(keyword.ToLower().Trim()) ||
            x.total.ToString().ToLower().Equals(keyword.ToLower().Trim()) ||
            x.address.ToLower().Contains(keyword.ToLower().Trim())
            ).ToList();
            var order = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index", order);
        }
        [HttpPost]
        public JsonResult delete_order(int id)
        {
            try
            {
                var order = db.order.Find(id);
                db.order.Remove(order);
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