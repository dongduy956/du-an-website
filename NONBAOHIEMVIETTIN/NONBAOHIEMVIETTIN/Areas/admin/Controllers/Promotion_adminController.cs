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
    public class Promotion_adminController : BaseController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        int pageSize = 10;
        void ViewBagNoti(List<promotion> temp, int page)
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
            var temp = db.promotion.ToList();
            var promotion = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            ViewBag.check = true;
            return View(promotion);
        }
        public ActionResult Search(int page = 1)
        {
            var keyword = Request["tukhoa"];
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            ViewBag.check = false;
            keyword = keyword.ToLower().Trim();
            var temp = db.promotion.Where(x =>
            x.id.ToString().ToLower().Equals(keyword) ||
            x.name.ToLower().Contains(keyword) ||
            x.accounts.fullname.Contains(keyword) ||
            x.create_date.ToString().Contains(keyword) ||
            x.end_date.ToString().Contains(keyword) ||
            x.start_date.ToString().Contains(keyword)
            ).ToList();
            var brand = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index", brand);
        }
        [HttpPost]
        public JsonResult delete_promotion(int id)
        {
            try
            {
                var promotion = db.promotion.Find(id);
                db.promotion.Remove(promotion);
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
        // GET: admin/Promotion_admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Promotion_admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,start_date,end_date,discount,quantity_use")] promotion promotion)
        {
            if (ModelState.IsValid)
            {

                if (promotion.quantity_use == 0)
                    TempData["status"] = "Số lượng người sử phải lớn hơn 0!!";
                else
                if (db.promotion.SingleOrDefault(x => x.name.ToLower().Equals(promotion.name.ToLower())) == null)
                {
                    if (((DateTime)promotion.start_date).CompareTo(((DateTime)promotion.end_date)) >= 0)
                        TempData["status"] = "Ngày kết thúc phải lớn hơn ngày bắt đầu!!";
                    else
                    {
                        promotion.code = Libary.Instances.randCode(10);
                        promotion.create_by = (Session["account_admin"] as accounts).id;
                        promotion.create_date = DateTime.Now;
                        promotion.alias = Libary.Instances.convertToUnSign3(promotion.name);
                        db.promotion.Add(promotion);
                        db.SaveChanges();
                        TempData["status"] = "Thêm mới phiếu giảm giá thành công!!";

                        return Redirect("/giam-gia");
                    }
                }
                else
                {
                    TempData["status"] = "Trùng tên giảm giá!!";
                }
            }
            return View(promotion);
        }

        // GET: admin/Promotion_admin/Edit/5
        public ActionResult Edit(string alias)
        {

            if (string.IsNullOrEmpty(alias))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            promotion promotion = db.promotion.SingleOrDefault(x => x.alias.Equals(alias));
            if (promotion == null)
            {
                return HttpNotFound();
            }
            return View(promotion);
        }

        // POST: admin/Promotion_admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,create_date,start_date,end_date,create_by,discount,quantity_use,code")] promotion promotion)
        {
            if (ModelState.IsValid)
            {
                if (((DateTime)promotion.start_date).CompareTo(((DateTime)promotion.end_date)) >= 0)
                    TempData["status"] = "Ngày kết thúc phải lớn hơn ngày bắt đầu!!";
                else
                {
                    var temp = db.promotion.SingleOrDefault(x => x.name.ToLower().Equals(promotion.name.ToLower()));
                    if (temp == null)
                    {
                        promotion.alias = Libary.Instances.convertToUnSign3(promotion.name.ToLower());
                        db.Entry(promotion).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["status"] = "Sửa phiếu giảm giá thành công!!";
                        return Redirect("/giam-gia");
                    }
                    else
                        if (temp != null && promotion.id == temp.id)
                    {
                        promotion = temp = db.promotion.Find(promotion.id);
                        promotion.create_by = int.Parse(Request["create_by"].ToString());
                        promotion.create_date = DateTime.Parse(Request["create_date"].ToString());
                        promotion.end_date = DateTime.Parse(Request["end_date"].ToString());
                        promotion.start_date = DateTime.Parse(Request["start_date"].ToString());
                        promotion.name = Request["name"];
                        promotion.discount = int.Parse(Request["discount"].ToString());
                        promotion.quantity_use = int.Parse(Request["quantity_use"].ToString());
                        promotion.alias = Libary.Instances.convertToUnSign3(promotion.name.ToLower());
                        db.Entry(promotion).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["status"] = "Sửa giảm giá thành công!!";
                        return Redirect("/giam-gia");
                    }
                    else
                        TempData["status"] = "Trùng tên giảm giá!!";
                }
            }
            return View(promotion);

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
