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
    public class GroupProduct_adminController : BaseController
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void ViewBagNoti(List<groupproduct> temp, int page)
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
            var temp = db.groupproduct.Where(x => x.isdelete == false).ToList();
            var groupproduct = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View(groupproduct);
        }
        [HttpPost]
        public JsonResult delete_groupProduct(int id)
        {
            try
            {
                var groupproduct = db.groupproduct.SingleOrDefault(x => x.id == id);
                groupproduct.isdelete = true;
                db.Entry(groupproduct).State = EntityState.Modified;
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
        public ActionResult Create([Bind(Include = "id,name")] groupproduct groupproduct)
        {

            if (ModelState.IsValid)
            {
                groupproduct.isdelete = false;
                groupproduct.status = true;
                groupproduct.alias = HoTro.Instances.convertToUnSign3(groupproduct.name);
                db.groupproduct.Add(groupproduct);
                db.SaveChanges();
                return Redirect("/admin/nhom-non.html");
            }
            return View(groupproduct);
        }

        // GET: admin/products/Edit/5
        public ActionResult Edit(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            groupproduct groupproduct = db.groupproduct.SingleOrDefault(x => x.alias.Equals(alias));
            if (groupproduct == null)
            {
                return HttpNotFound();
            }
            return View(groupproduct);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,alias,status,isdelete")] groupproduct groupproduct)
        {
            if (ModelState.IsValid)
            {

                groupproduct.alias = HoTro.Instances.convertToUnSign3(groupproduct.name);
                db.Entry(groupproduct).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/admin/nhom-non.html");
            }
            return View(groupproduct);
        }

    }
}