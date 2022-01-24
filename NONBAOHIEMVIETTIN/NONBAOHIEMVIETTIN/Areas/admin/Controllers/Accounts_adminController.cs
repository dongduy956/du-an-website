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
    public class Accounts_adminController : BaseController
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void ViewBagNoti(List<accounts> temp, int page)
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
            var temp = db.accounts.ToList();
            var accounts = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            ViewBag.check = true;
            return View(accounts);
        }
        public ActionResult Search(int page = 1)
        {
            var keyword = Request["tukhoa"];
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            ViewBag.check = false;

            var temp = db.accounts.Where(x =>
            x.address.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.email.ToLower().Equals(keyword.ToLower().Trim()) ||
            x.phone.ToLower().Equals(keyword.ToLower().Trim()) ||
            x.role.name.ToLower().Equals(keyword.ToLower().Trim()) ||
            x.fullname.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.username.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.id.ToString().ToLower().Equals(keyword.ToLower().Trim())).ToList();
            var account = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index", account);
        }
        [HttpPost]
        public JsonResult delete_account(int id)
        {
            try
            {
                var accounts = db.accounts.SingleOrDefault(x => x.id == id);
                accounts.status = false;
                db.Entry(accounts).State = EntityState.Modified;
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

        // GET: admin/accounts/Create
        public ActionResult Create()
        {
            ViewBag.idrole = new SelectList(db.role, "id", "name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idrole,username,image,fullname,email,phone,address,status")] accounts accounts)
        {
            if (ModelState.IsValid)
            {
                if (db.accounts.SingleOrDefault(x => x.username.ToLower().Equals(accounts.username.ToLower())) == null)
                {
                    try
                    {
                        accounts.image = accounts.image.Substring(1, accounts.image.Length - 1);
                    }
                    catch (Exception ex)
                    {

                    }
                    accounts.password = HoTro.Instances.EncodeMD5("123");
                    accounts.issocial = 0;
                    accounts.alias = "tai-khoan-" + (db.accounts.OrderByDescending(x => x.id).FirstOrDefault().id + 1);
                    db.accounts.Add(accounts);
                    db.SaveChanges();
                    TempData["status"] = "Tạo tài khoản thành công. Mật khẩu mặc định là 123!!";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["status"] = "Trùng tên đăng nhập!!";
                }
            }

            ViewBag.idrole = new SelectList(db.role, "id", "name", accounts.idrole);
            return View(accounts);
        }

        // GET: admin/accounts/Edit/5
        public ActionResult Edit(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            accounts accounts = db.accounts.SingleOrDefault(x => x.alias.Equals(alias));
            if (accounts == null)
            {
                return HttpNotFound();
            }
            ViewBag.idrole = new SelectList(db.role, "id", "name", accounts.idrole);
            return View(accounts);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idrole,username,password,image,fullname,email,phone,address,status,issocial")] accounts accounts)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    accounts.image = accounts.image.Substring(1, accounts.image.Length - 1);

                }
                catch (Exception ex)
                {

                }
                db.Entry(accounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idrole = new SelectList(db.role, "id", "name", accounts.idrole);
            return View(accounts);
        }

        // GET: admin/accounts/Delete/5
        public ActionResult Delete(int? id)
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
            return View(accounts);
        }

        // POST: admin/accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            accounts accounts = db.accounts.Find(id);
            db.accounts.Remove(accounts);
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
