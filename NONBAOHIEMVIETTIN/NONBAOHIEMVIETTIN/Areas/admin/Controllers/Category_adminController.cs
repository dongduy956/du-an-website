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
            var temp = db.category.ToList();
            var category = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            ViewBag.check = true;
            return View(category);
        }
        public ActionResult Search(int page = 1)
        {
            var keyword = Request["tukhoa"];
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            ViewBag.check = false;

            var temp = db.category.Where(x =>
            x.name.ToLower().Contains(keyword.ToLower().Trim())||
            x.id.ToString().ToLower().Equals(keyword.ToLower().Trim())).ToList();
            var category = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index", category);
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

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] category category)
        {

            if (ModelState.IsValid)
            {
                if (db.category.SingleOrDefault(x => x.name.ToLower().Equals(category.name.ToLower())) == null)
                {
                    category.isdelete = false;
                    category.status = true;
                    category.alias = HoTro.Instances.convertToUnSign3(category.name);
                    db.category.Add(category);
                    db.SaveChanges();
                    TempData["status"] = "Thêm mới loại nón thành công!!";

                    return Redirect("/admin/loai-non.html");
                }
                else
                {
                    TempData["status"] = "Trùng tên loại nón!!";
                }
            }
            return View(category);
        }

        // GET: admin/products/Edit/5
        public ActionResult Edit(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.category.SingleOrDefault(x => x.alias.Equals(alias));
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,alias,status,isdelete")] category category)
        {
            if (ModelState.IsValid)
            {
                var temp = db.category.SingleOrDefault(x => x.name.ToLower().Equals(category.name.ToLower()));
                if (temp == null)
                {
                    category.alias = HoTro.Instances.convertToUnSign3(category.name.ToLower());
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa loại nón thành công!!";
                    return Redirect("/admin/loai-non.html");
                }
                else
                    if (temp != null && category.id == temp.id)
                {
                    category = temp = db.category.Find(category.id);
                    category.status = Boolean.Parse(Request["status"]);
                    category.isdelete = Boolean.Parse(Request["isdelete"]);
                    category.name = Request["name"];
                    category.alias = HoTro.Instances.convertToUnSign3(category.name.ToLower());
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa loại nón thành công!!";
                    return Redirect("/admin/loai-non.html");
                }
                else
                {
                    TempData["status"] = "Trùng tên loại nón!!";
                }
            }
            return View(category);
        }

    }
}