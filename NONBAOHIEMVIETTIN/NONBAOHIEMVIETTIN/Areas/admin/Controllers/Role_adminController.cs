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
    public class Role_adminController : BaseController
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void ViewBagNoti(List<role> temp, int page)
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
            var temp = db.role.ToList();
            var role = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View(role);
        }
        public ActionResult Search(int page = 1)
        {
            var keyword = Request["tukhoa"];
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            ViewBag.check = false;

            var temp = db.role.Where(x =>
            x.name.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.id.ToString().ToLower().Equals(keyword.ToLower().Trim())).ToList();
            var role = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index", role);
        }

        [HttpPost]
        public JsonResult delete_role(int id)
        {
            try
            {
                var role = db.role.Find(id);
                db.role.Remove(role);
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
        public ActionResult Create([Bind(Include = "id,name")] role role)
        {

            if (ModelState.IsValid)
            {
                if (db.role.SingleOrDefault(x => x.name.ToLower().Equals(role.name.ToLower())) == null)
                {                   
                    role.alias = HoTro.Instances.convertToUnSign3(role.name);
                    db.role.Add(role);
                    db.SaveChanges();
                    TempData["status"] = "Thêm mới quyền thành công!!";

                    return Redirect("/admin/quyen");
                }
                else
                {
                    TempData["status"] = "Trùng tên quyền!!";
                }
            }
            return View(role);
        }

        public ActionResult Edit(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            role role = db.role.SingleOrDefault(x => x.alias.Equals(alias));
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,alias")] role role)
        {
            if (ModelState.IsValid)
            {
                var temp = db.role.SingleOrDefault(x => x.name.ToLower().Equals(role.name.ToLower()));
                if (temp == null)
                {
                    role.alias = HoTro.Instances.convertToUnSign3(role.name.ToLower());
                    db.Entry(role).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa quyền thành công!!";
                    return Redirect("/admin/quyen");
                }
                else
                    if (temp != null && role.id == temp.id)
                {
                    role = temp = db.role.Find(role.id);
                    role.name = Request["name"];
                    role.alias = HoTro.Instances.convertToUnSign3(role.name.ToLower());
                    db.Entry(role).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa quyền thành công!!";
                    return Redirect("/admin/quyen");
                }
                else
                {
                    TempData["status"] = "Trùng tên quyền!!";
                }
            }
            return View(role);
        }

    }
}