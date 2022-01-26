using NONBAOHIEMVIETTIN.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NONBAOHIEMVIETTIN.Areas.admin.Controllers
{
    public class Newstype_adminController : BaseController
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void ViewBagNoti(List<newstype> temp, int page)
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
            var temp = db.newstype.ToList();
            var newstype = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            ViewBag.check = true;
            return View(newstype);
        }
        public ActionResult Search(int page = 1)
        {
            var keyword = Request["tukhoa"];
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            ViewBag.check = false;

            var temp = db.newstype.Where(x =>
            x.name.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.id.ToString().ToLower().Equals(keyword.ToLower().Trim())).ToList();
            var newstype = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index", newstype);
        }
        [HttpPost]
        public JsonResult delete_newstype(int id)
        {
            try
            {
                var newstype = db.newstype.Find(id);
                db.newstype.Remove(newstype);
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

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] newstype newstype)
        {

            if (ModelState.IsValid)
            {
                if (db.newstype.SingleOrDefault(x => x.name.ToLower().Equals(newstype.name.ToLower())) == null)
                {
                    newstype.alias = HoTro.Instances.convertToUnSign3(newstype.name);
                    db.newstype.Add(newstype);
                    db.SaveChanges();
                    TempData["status"] = "Thêm mới loại tin thành công!!";

                    return Redirect("/admin/loai-tin.html");
                }
                else
                {
                    TempData["status"] = "Trùng tên loại tin!!";
                }
            }
            return View(newstype);
        }

        public ActionResult Edit(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            newstype newstype = db.newstype.SingleOrDefault(x => x.alias.Equals(alias));
            if (newstype == null)
            {
                return HttpNotFound();
            }
            return View(newstype);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,alias")] newstype newstype)
        {
            if (ModelState.IsValid)
            {
                var temp = db.newstype.SingleOrDefault(x => x.name.ToLower().Equals(newstype.name.ToLower()));
                if (temp == null)
                {
                    newstype.alias = HoTro.Instances.convertToUnSign3(newstype.name.ToLower());
                    db.Entry(newstype).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa loại tin thành công!!";
                    return Redirect("/admin/loai-tin.html");
                }
                else
                    if (temp != null && newstype.id == temp.id)
                {
                    newstype = temp = db.newstype.Find(newstype.id);
                    newstype.name = Request["name"];
                    newstype.alias = HoTro.Instances.convertToUnSign3(newstype.name.ToLower());
                    db.Entry(newstype).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa loại tin thành công!!";
                    return Redirect("/admin/loai-tin.html");
                }
                else
                {
                    TempData["status"] = "Trùng tên loại tin!!";
                }
            }
            return View(newstype);
        }
    }
}