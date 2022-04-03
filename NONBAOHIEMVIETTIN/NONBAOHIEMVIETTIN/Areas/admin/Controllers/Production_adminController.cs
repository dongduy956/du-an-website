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
    public class Production_adminController : BaseController
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void ViewBagNoti(List<production> temp, int page)
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
            var temp = db.production.ToList();
            var production = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            ViewBag.check = true;
            return View(production);
        }
        public ActionResult Search(int page = 1)
        {
            var keyword = Request["tukhoa"];
            if(string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            ViewBag.check = false;

            var temp = db.production.Where(x=>
            x.name.ToLower().Contains(keyword.ToLower().Trim())||
            x.id.ToString().ToLower().Equals(keyword.ToLower().Trim()) 
            ).ToList();
            var production = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index",production);
        }
        [HttpPost]
        public JsonResult delete_production(int id)
        {
            try
            {
                var production = db.production.SingleOrDefault(x => x.id == id);
                production.isdelete = true;
                db.Entry(production).State = EntityState.Modified;
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
        public ActionResult Create([Bind(Include = "id,name")] production production)
        {

            if (ModelState.IsValid)
            {
                if (db.production.SingleOrDefault(x => x.name.ToLower().Equals(production.name.ToLower())) == null)
                {
                    production.isdelete = false;
                    production.status = true;
                    production.alias = Libary.Instances.convertToUnSign3(production.name);
                    db.production.Add(production);
                    db.SaveChanges();
                    TempData["status"] = "Thêm mới hãng sản xuất thành công!!";

                    return Redirect("/hang-san-xuat");
                }
                else
                {
                    TempData["status"] = "Trùng tên hãng sản xuất!!";
                }
            }
            return View(production);
        }
        public ActionResult Edit(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            production production = db.production.SingleOrDefault(x => x.alias.Equals(alias));
            if (production == null)
            {
                return HttpNotFound();
            }
            return View(production);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,alias,status,isdelete")] production production)
        {
            if (ModelState.IsValid)
            {
                var temp = db.production.SingleOrDefault(x => x.name.ToLower().Equals(production.name.ToLower()));
                if (temp == null)
                {
                    production.alias = Libary.Instances.convertToUnSign3(production.name.ToLower());
                    db.Entry(production).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa hãng sản xuất thành công!!";

                    return Redirect("/hang-san-xuat");
                }
                else
                    if (temp != null && production.id == temp.id)
                {
                    production = temp = db.production.Find(production.id);
                    production.status = Boolean.Parse(Request["status"]);
                    production.isdelete = Boolean.Parse(Request["isdelete"]);
                    production.name = Request["name"];
                    production.alias = Libary.Instances.convertToUnSign3(production.name.ToLower());
                    db.Entry(production).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa hãng sản xuất thành công!!";
                    return Redirect("/hang-san-xuat");
                }
                else
                {
                    TempData["status"] = "Trùng tên hãng sản xuất!!";
                }
            }
            return View(production);
        }

    }
}