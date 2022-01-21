using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NONBAOHIEMVIETTIN.Models;
using PagedList;

namespace NONBAOHIEMVIETTIN.Areas.admin.Controllers
{
    public class Category_adminController : BaseController
    {

        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void ViewBagNoti(List<category> temp, int page)
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
            var temp = db.category.Where(x => x.isdelete == false).ToList();
            var category = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View(category);
        }
        [HttpPost]
        public JsonResult delete_category(int id)
        {
            try
            {
                var category = db.category.SingleOrDefault(x => x.id == id);
                category.isdelete = true;
                db.Entry(category).State = EntityState.Modified;
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