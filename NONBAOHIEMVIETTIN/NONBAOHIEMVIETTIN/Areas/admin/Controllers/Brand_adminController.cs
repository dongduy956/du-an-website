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
    public class Brand_adminController : BaseController
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void ViewBagNoti(List<brand> temp, int page)
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
            var temp = db.brand.ToList();
            var brand = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            ViewBag.check = true;
            return View(brand);
        }
        public ActionResult Search(int page = 1)
        {
            var keyword = Request["tukhoa"];
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            ViewBag.check = false;
            var temp = db.brand.Where(x =>           
            x.id.ToString().ToLower().Equals(keyword.ToLower().Trim()) ||            
            x.name.ToLower().Contains(keyword.ToLower().Trim())
            ).ToList();
            var brand = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index", brand);
        }
        [HttpPost]
        public JsonResult delete_brand(int id)
        {
            try
            {
                var brand = db.brand.Find(id);
                db.brand.Remove(brand);
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
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,name,link,image")] brand brand)
        {
            if (ModelState.IsValid)
            {

                if (db.brand.SingleOrDefault(x => x.name.ToLower().Equals(brand.name.ToLower())) == null)
                {
                    try
                    {
                        brand.image = brand.image.Substring(1, brand.image.Length - 1);

                    }
                    catch (Exception ex)
                    {

                    }
                    brand.alias = HoTro.Instances.convertToUnSign3(brand.name);
                    db.brand.Add(brand);
                    db.SaveChanges();
                    TempData["status"] = "Thêm mới đối tác thành công!!";
                    return Redirect("/admin/doi-tac");
                }
                else
                {
                    TempData["status"] = "Trùng tên đối tác!!";
                }
            }
            return View(brand);
        }

        // GET: admin/products/Edit/5
        public ActionResult Edit(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            brand brand = db.brand.SingleOrDefault(x => x.alias.Equals(alias));
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,name,image,alias,link")] brand brand)
        {
            if (ModelState.IsValid)
            {
                var temp = db.brand.SingleOrDefault(x => x.name.ToLower().Equals(brand.name.ToLower()));
                if (temp == null)
                {
                    try
                    {
                        brand.image = brand.image.Substring(1, brand.image.Length - 1);

                    }
                    catch (Exception ex)
                    {

                    }

                    brand.alias = HoTro.Instances.convertToUnSign3(brand.name.ToLower());
                    db.Entry(brand).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa đối tác thành công!!";
                    return Redirect("/admin/doi-tac");
                }
                else
                    if (temp != null && brand.id == temp.id)
                {
                   
                    brand = temp = db.brand.Find(brand.id);
                    brand.name = Request["name"];
                    brand.link = Request["link"];
                    brand.image = Request["image"].Substring(1, Request["image"].Length - 1);
                    brand.alias = HoTro.Instances.convertToUnSign3(brand.name.ToLower());
                    db.Entry(brand).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa đối tác thành công!!";
                    return Redirect("/admin/doi-tac");
                }
                else
                {
                    TempData["status"] = "Trùng tên đối tác!!";
                }
            }
            return View(brand);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}