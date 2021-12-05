using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NONBAOHIEMVIETTIN.Models;
using DuongDongDuy_2001191210_ktralan3.Models;

namespace NONBAOHIEMVIETTIN.Controllers
{
    public class AccountsController : Controller
    {
        private EDO db = new EDO();

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginRegister lr)
        {
            if (ModelState.IsValid)
            {
                lr.login.password = HoTro.Instances.EncodeMD5(lr.login.password);
                accounts acc = db.accounts.SingleOrDefault(x => x.username.Equals(lr.login.username) && x.password == lr.login.password);
                if (acc == null)
                    ViewBag.login = "Thất bại";
                else
                    ViewBag.login = "Thành công";
            }
            else
            {
                ViewBag.login = "sai";
            }

            return View(lr);
        }
        // GET: Accounts/Create
        public ActionResult Create()
        {
            ViewBag.idpermission = new SelectList(db.permission, "id", "name");
            return View("Login");
        }
        // POST: Accounts/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idpermission,username,password,image,fullname,email,phone,address")] accounts accounts)
        {
            if (ModelState.IsValid)
            {
                db.accounts.Add(accounts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idpermission = new SelectList(db.permission, "id", "name", accounts.idpermission);
            return View("Login", accounts);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            accounts accounts = db.accounts.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            ViewBag.idpermission = new SelectList(db.permission, "id", "name", accounts.idpermission);
            return View(accounts);
        }

        // POST: Accounts/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idpermission,username,password,image,fullname,email,phone,address")] accounts accounts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idpermission = new SelectList(db.permission, "id", "name", accounts.idpermission);
            return View(accounts);
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
