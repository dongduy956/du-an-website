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
    public class Introduce_adminController : BaseController
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void ViewBagNoti(List<introduce> temp, int page)
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
            var temp = db.introduce.ToList();
            var introduce = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            ViewBag.check = true;
            return View(introduce);
        }
        public ActionResult Search(int page = 1)
        {
            var keyword = Request["tukhoa"];
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            ViewBag.check = false;

            var temp = db.introduce.Where(x =>
            x.title.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.id.ToString().ToLower().Equals(keyword.ToLower().Trim())).ToList();
            var introduce = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index", introduce);
        }
        [HttpPost]
        public JsonResult delete_introduce(int id)
        {
            try
            {
                var introduce = db.introduce.SingleOrDefault(x => x.id == id);
                introduce.status = false;
                db.Entry(introduce).State = EntityState.Modified;
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

        public ActionResult Create([Bind(Include = "id,title,content,image")] introduce introduce)
        {
            if (ModelState.IsValid)
            {
                if (db.introduce.SingleOrDefault(x => x.title.ToLower().Equals(introduce.title.ToLower())) == null)
                {
                    try
                    {
                        introduce.image = introduce.image.Substring(1, introduce.image.Length - 1);

                    }
                    catch (Exception ex)
                    {

                    }
                    introduce.status = true;
                    introduce.alias = HoTro.Instances.convertToUnSign3(introduce.title);
                    db.introduce.Add(introduce);
                    db.SaveChanges();
                    TempData["status"] = "Thêm mới giới thiệu thành công!!";

                    return Redirect("/admin/gioi-thieu.html");
                }
                else
                {
                    TempData["status"] = "Trùng tiêu đề giới thiệu!!";
                }
            }
            return View(introduce);
        }

        // GET: admin/products/Edit/5
        public ActionResult Edit(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            introduce introduce = db.introduce.SingleOrDefault(x => x.alias.Equals(alias));
            if (introduce == null)
            {
                return HttpNotFound();
            }
            return View(introduce);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,title,alias,status,content,image")] introduce introduce)
        {
            if (ModelState.IsValid)
            {
                var temp = db.introduce.SingleOrDefault(x => x.title.ToLower().Equals(introduce.title.ToLower()));
                if (temp == null)
                {
                    try
                    {
                        introduce.image = introduce.image.Substring(1, introduce.image.Length - 1);

                    }
                    catch (Exception ex)
                    {

                    }
                    introduce.alias = HoTro.Instances.convertToUnSign3(introduce.title.ToLower());
                    db.Entry(introduce).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa giới thiệu thành công!!";
                    return Redirect("/admin/gioi-thieu.html");
                }
                else
                    if (temp != null && introduce.id == temp.id)
                {
                    var content = introduce.content;
                    introduce = temp = db.introduce.Find(introduce.id);
                    try
                    {
                        introduce.image = Request["image"].Substring(1, Request["image"].Length - 1);

                    }
                    catch (Exception)
                    {

                    }                  
                    introduce.status = Boolean.Parse(Request["status"]);
                    introduce.title = Request["title"];
                    introduce.content = content;

                    introduce.alias = HoTro.Instances.convertToUnSign3(introduce.title.ToLower());
                    db.Entry(introduce).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa giới thiệu thành công!!";
                    return Redirect("/admin/gioi-thieu.html");
                }
                else
                {
                    TempData["status"] = "Trùng tiêu đề!!";
                }
            }
            return View(introduce);
        }

    }
}