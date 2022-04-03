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
    public class Contact_adminController : BaseController
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void ViewBagNoti(List<contact> temp, int page)
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
            var temp = db.contact.ToList();
            var contact = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            ViewBag.check = true;
            return View(contact);
        }
        public ActionResult Search(int page = 1)
        {
            var keyword = Request["tukhoa"];
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            ViewBag.check = false;

            var temp = db.contact.Where(x =>
            x.title.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.content.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.content.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.phone.ToLower().Equals(keyword.ToLower().Trim()) ||   
            x.worktime.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.workday.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.email.ToLower().Equals(keyword.ToLower().Trim()) ||
            x.id.ToString().ToLower().Equals(keyword.ToLower().Trim())).ToList();
            var contact = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index", contact);
        }
        [HttpPost]
        public JsonResult delete_contact(int id)
        {
            try
            {
                var contact = db.contact.Find(id);
                db.contact.Remove(contact);
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

        public ActionResult Create([Bind(Include = "id,title,content,address,email,phone,worktime,workday,map")] contact contact)
        {
            if (ModelState.IsValid)
            {
                if (db.contact.SingleOrDefault(x => x.title.ToLower().Equals(contact.title.ToLower())) == null)
                {
                    contact.display = true;
                    contact.alias = Libary.Instances.convertToUnSign3(contact.title);
                    db.contact.Add(contact);
                    db.SaveChanges();
                    TempData["status"] = "Thêm mới liên hệ thành công!!";

                    return Redirect("/lien-he");
                }
                else
                {
                    TempData["status"] = "Trùng tiêu đề liên hệ!!";
                }
            }
            return View(contact);
        }

        public ActionResult Edit(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contact contact = db.contact.SingleOrDefault(x => x.alias.Equals(alias));
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,title,content,address,email,phone,worktime,workday,map,display,alias")] contact contact)
        {
            if (ModelState.IsValid)
            {
                var temp = db.contact.SingleOrDefault(x => x.title.ToLower().Equals(contact.title.ToLower()));
                if (temp == null)
                {                   
                    contact.alias = Libary.Instances.convertToUnSign3(contact.title.ToLower());
                    db.Entry(contact).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa liên hệ thành công!!";
                    return Redirect("/lien-he");
                }
                else
                    if (temp != null && contact.id == temp.id)
                {
                    var content = contact.content;
                    var map = contact.map;
                    contact = temp = db.contact.Find(contact.id);
                  
                    contact.display = Boolean.Parse(Request["display"]);
                    contact.title = Request["title"];
                    contact.phone = Request["phone"];
                    contact.address = Request["address"];
                    contact.email = Request["email"];
                    contact.map = map;
                    contact.workday = Request["workday"];
                    contact.worktime = Request["worktime"];
                    contact.content = content;
                    contact.alias = Libary.Instances.convertToUnSign3(contact.title.ToLower());

                    db.Entry(contact).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa liên hệ thành công!!";
                    return Redirect("/lien-he");
                }
                else
                {
                    TempData["status"] = "Trùng tiêu đề liên hệ!!";
                }
            }
            return View(contact);
        }
    }
}