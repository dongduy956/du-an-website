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
            var temp = db.production.Where(x => x.isdelete == false).ToList();
            var production = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View(production);
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
                production.isdelete = false;
                production.status = true;
                production.alias = HoTro.Instances.convertToUnSign3(production.name);
                db.production.Add(production);
                db.SaveChanges();
                return Redirect("/admin/hang-san-xuat.html");
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

                production.alias = HoTro.Instances.convertToUnSign3(production.name);
                db.Entry(production).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/admin/hang-san-xuat.html");
            }
            return View(production);
        }

    }
}