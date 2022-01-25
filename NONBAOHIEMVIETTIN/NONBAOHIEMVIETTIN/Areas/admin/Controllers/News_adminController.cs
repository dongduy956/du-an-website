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
    public class News_adminController : BaseController
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 1;
        void ViewBagNoti(List<news> temp, int page)
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
            var temp = db.news.ToList();
            var news = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            ViewBag.check = true;
            return View(news);
        }
        public ActionResult Search(int page = 1)
        {
            var keyword = Request["tukhoa"];
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            ViewBag.check = false;
            var temp = db.news.Where(x =>
            x.createdate.ToString().ToLower().Contains(keyword.ToLower().Trim()) ||
            x.newstype.name.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.id.ToString().ToLower().Equals(keyword.ToLower().Trim()) ||
            x.title.ToLower().Contains(keyword.ToLower().Trim())
            ).ToList();
            var news = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index", news);
        }
        [HttpPost]
        public JsonResult delete_news(int id)
        {
            try
            {
                var news = db.news.SingleOrDefault(x => x.id == id);
                news.isdelete = true;
                db.Entry(news).State = EntityState.Modified;
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
            ViewBag.id_newstype = new SelectList(db.newstype, "id", "name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,id_newstype,title,content,image")] news news)
        {
            if (ModelState.IsValid)
            {

                if (db.news.SingleOrDefault(x => x.title.ToLower().Equals(news.title.ToLower())) == null)
                {
                    try
                    {
                        news.image = news.image.Substring(1, news.image.Length - 1);

                    }
                    catch (Exception ex)
                    {

                    }
                    news.createdate = DateTime.Now;
                    news.isdelete = false;
                    news.alias = HoTro.Instances.convertToUnSign3(news.title);
                    db.news.Add(news);
                    db.SaveChanges();
                    TempData["status"] = "Thêm mới tin tức thành công!!";
                    return Redirect("/admin/tin-tuc.html");
                }
                else
                {
                    TempData["status"] = "Trùng tên tiêu đề!!";
                }
            }

            ViewBag.id_newstype = new SelectList(db.newstype, "id", "name", news.id_newstype);

            return View(news);
        }

        // GET: admin/products/Edit/5
        public ActionResult Edit(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            news news = db.news.SingleOrDefault(x => x.alias.Equals(alias));
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_newstype = new SelectList(db.newstype, "id", "name", news.id_newstype);

            return View(news);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,id_newstype,title,content,image,alias,createdate,isdelete")] news news)
        {
            if (ModelState.IsValid)
            {
                var temp = db.news.SingleOrDefault(x => x.title.ToLower().Equals(news.title.ToLower()));
                if (temp == null)
                {
                    news.alias = HoTro.Instances.convertToUnSign3(news.title.ToLower());
                    db.Entry(news).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa tin tức thành công!!";
                    return Redirect("/admin/tin-tuc.html");
                }
                else
                    if (temp != null && news.id == temp.id)
                {
                    var content = news.content;
                    var id_newstype = news.id_newstype;
                    news = temp = db.news.Find(news.id);
                    news.isdelete = Boolean.Parse(Request["isdelete"]);
                    news.title = Request["title"];
                    news.id_newstype = id_newstype;
                    news.content = content;
                    news.image = Request["image"].Substring(1,Request["image"].Length-1);
                    news.alias = HoTro.Instances.convertToUnSign3(news.title.ToLower());
                    db.Entry(news).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa tin tức thành công!!";
                    return Redirect("/admin/tin-tuc.html");
                }
                else
                {
                    TempData["status"] = "Trùng tên tin tức!!";
                }
            }
            ViewBag.id_newstype = new SelectList(db.newstype, "id", "name", news.id_newstype);
            return View(news);
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