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
            var temp = db.groupproduct.ToList();
            var groupproduct = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            ViewBag.check = true;
            return View(groupproduct);
        }
        public ActionResult Search(int page = 1)
        {
            var keyword = Request["tukhoa"];
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            ViewBag.check = true;

            var temp = db.groupproduct.Where(x => 
            x.name.ToLower().Contains(keyword.ToLower().Trim())||
            x.id.ToString().ToLower().Equals(keyword.ToLower().Trim())
            ).ToList();
            var groupproduct = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index", groupproduct);
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
                if (db.groupproduct.SingleOrDefault(x => x.name.ToLower().Equals(groupproduct.name.ToLower())) == null)
                {
                    groupproduct.isdelete = false;
                    groupproduct.status = true;
                    groupproduct.alias = HoTro.Instances.convertToUnSign3(groupproduct.name);
                    db.groupproduct.Add(groupproduct);
                    db.SaveChanges();
                    TempData["status"] = "Thêm mới nhóm nón thành công!!";

                    return Redirect("/admin/nhom-non");
                }
                else
                {
                    TempData["status"] = "Trùng tên nhóm nón!!";
                }
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
                var temp = db.groupproduct.SingleOrDefault(x => x.name.ToLower().Equals(groupproduct.name.ToLower()));
                if (temp == null)
                {
                    groupproduct.alias = HoTro.Instances.convertToUnSign3(groupproduct.name.ToLower());
                    db.Entry(groupproduct).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa nhóm nón thành công!!";
                    return Redirect("/admin/nhom-non");
                }
                else
                    if (temp != null && groupproduct.id == temp.id)
                {
                    groupproduct = temp = db.groupproduct.Find(groupproduct.id);
                    groupproduct.status = Boolean.Parse(Request["status"]);
                    groupproduct.isdelete = Boolean.Parse(Request["isdelete"]);
                    groupproduct.name = Request["name"];
                    groupproduct.alias = HoTro.Instances.convertToUnSign3(groupproduct.name.ToLower());
                    db.Entry(groupproduct).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa nhóm nón thành công!!";
                    return Redirect("/admin/nhom-non");
                }
                else
                {
                    TempData["status"] = "Trùng tên nhóm nón!!";
                }
            }
            return View(groupproduct);
        }

    }
}