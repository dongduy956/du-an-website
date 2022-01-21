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
    public class productsController : BaseController
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
        // GET: admin/products
        public ActionResult Index(int page=1)
        {
            var temp = db.products.Include(p => p.category).Include(p => p.groupproduct).Include(p => p.production).ToList();
            var products = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View(products);
        }

        // GET: admin/products/Details/5
        public ActionResult Details(int? id)
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
            return View(products);
        }

        // GET: admin/products/Create
        public ActionResult Create()
        {
            ViewBag.idcategory = new SelectList(db.category, "id", "name");
            ViewBag.idgroupproduct = new SelectList(db.groupproduct, "id", "name");
            ViewBag.idproduction = new SelectList(db.production, "id", "name");
            return View();
        }

        // POST: admin/products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,alias,status,price,promationprice,quantity,description,viewcount,createddate,image,fastsell,newproduct,idcategory,idproduction,idgroupproduct")] products products)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
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

        // GET: admin/products/Delete/5
        public ActionResult Delete(int? id)
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
            return View(products);
        }

        // POST: admin/products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            products products = db.products.Find(id);
            db.products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
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
