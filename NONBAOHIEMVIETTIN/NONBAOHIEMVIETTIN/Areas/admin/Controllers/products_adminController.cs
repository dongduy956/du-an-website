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
    public class products_adminController : BaseController
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void ViewBagNoti(List<products> temp, int page)
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
            var temp = db.products.Where(x => x.isdelete == false).ToList();
            var products = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View(products);
        }
        [HttpPost]
        public JsonResult delete_product(int id)
        {
            try
            {
                var product = db.products.SingleOrDefault(x => x.id == id);
                product.isdelete = true;
                db.Entry(product).State = EntityState.Modified;
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
            ViewBag.idcategory = new SelectList(db.category, "id", "name");
            ViewBag.idgroupproduct = new SelectList(db.groupproduct, "id", "name");
            ViewBag.idproduction = new SelectList(db.production, "id", "name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,name,price,promationprice,quantity,description,image,idcategory,idproduction,idgroupproduct")] products products)
        {
            if (ModelState.IsValid)
            {
                products.createddate = DateTime.Now;
                products.fastsell = products.isdelete = false;
                products.newproduct =products.status =true;
                products.viewcount = 0;
                products.alias = HoTro.Instances.convertToUnSign3(products.name);
                db.products.Add(products);
                db.SaveChanges();
                return Redirect("/admin/non.html");
            }

            ViewBag.idcategory = new SelectList(db.category, "id", "name", products.idcategory);
            ViewBag.idgroupproduct = new SelectList(db.groupproduct, "id", "name", products.idgroupproduct);
            ViewBag.idproduction = new SelectList(db.production, "id", "name", products.idproduction);
            return View(products);
        }

        // GET: admin/products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            products products = db.products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.idcategory = new SelectList(db.category, "id", "name", products.idcategory);
            ViewBag.idgroupproduct = new SelectList(db.groupproduct, "id", "name", products.idgroupproduct);
            ViewBag.idproduction = new SelectList(db.production, "id", "name", products.idproduction);
            return View(products);
        }

        // POST: admin/products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,alias,status,price,promationprice,quantity,description,viewcount,createddate,image,fastsell,newproduct,idcategory,idproduction,idgroupproduct")] products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idcategory = new SelectList(db.category, "id", "name", products.idcategory);
            ViewBag.idgroupproduct = new SelectList(db.groupproduct, "id", "name", products.idgroupproduct);
            ViewBag.idproduction = new SelectList(db.production, "id", "name", products.idproduction);
            return View(products);
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
