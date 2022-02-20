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
using System.Web.Script.Serialization;
using System.Xml.Linq;

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
            var temp = db.products.ToList();
            var products = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            ViewBag.check = true;
            return View(products);
        }
        public ActionResult Search(int page = 1)
        {
            var keyword = Request["tukhoa"];
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            ViewBag.check = false;
            var temp = db.products.Where(x =>
            x.name.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.category.name.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.production.name.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.groupproduct.name.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.id.ToString().ToLower().Equals(keyword.ToLower().Trim()) ||
            x.price.ToString().ToLower().Equals(keyword.ToLower().Trim()) ||
            x.promationprice.ToString().ToLower().Equals(keyword.ToLower().Trim())
            ).ToList();
            var products = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index", products);
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
        public ActionResult Create([Bind(Include = "id,name,price,promationprice,description,image,idcategory,idproduction,idgroupproduct")] products products)
        {
            if (ModelState.IsValid)
            {

                if (db.products.SingleOrDefault(x => x.name.ToLower().Equals(products.name.ToLower())) == null)
                {
                    try
                    {
                        products.image = products.image.Substring(1, products.image.Length - 1);

                    }
                    catch (Exception ex)
                    {

                    }

                    products.createddate = DateTime.Now;
                    products.fastsell = products.isdelete = products.status = false;
                    products.newproduct = true;
                    products.viewcount = products.quantity = 0;
                    products.alias = HoTro.Instances.convertToUnSign3(products.name);
                    db.products.Add(products);
                    db.SaveChanges();
                    TempData["status"] = "Thêm mới nón thành công!!";

                    return Redirect("/admin/non");
                }
                else
                {
                    TempData["status"] = "Trùng tên nón!!";
                }
            }

            ViewBag.idcategory = new SelectList(db.category, "id", "name", products.idcategory);
            ViewBag.idgroupproduct = new SelectList(db.groupproduct, "id", "name", products.idgroupproduct);
            ViewBag.idproduction = new SelectList(db.production, "id", "name", products.idproduction);
            return View(products);
        }

        [HttpPost]
        public ActionResult LoadImages(int id)
        {
            var products = db.products.Find(id);
            var images = products.moreimage;
            try
            {
                XElement xElement = XElement.Parse(images);
                List<string> lstImages = new List<string>();
                foreach (XElement ele in xElement.Elements())
                {
                    lstImages.Add(ele.Value);
                }
                return Json(new
                {
                    data = lstImages
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    data = ""
                });
            }

        }
        [HttpPost]
        public ActionResult SaveImages(int id, string[] images)
        {
            XElement xElement = new XElement("Images");
            try
            {
                foreach (var item in images)
                {
                    if (item[0] == '/')
                    {
                        var temp = item.Substring(1, item.Length - 1);
                        xElement.Add(new XElement("Images", temp));
                    }
                    else
                        xElement.Add(new XElement("Images", item));
                }
            }
            catch (Exception ex)
            {

                
            }
            var products = db.products.Find(id);
            products.moreimage = xElement.ToString();
            db.Entry(products).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                status = 1
            });
        }

        // GET: admin/products/Edit/5
        public ActionResult Edit(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            products products = db.products.SingleOrDefault(x => x.alias.Equals(alias));
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.idcategory = new SelectList(db.category, "id", "name", products.idcategory);
            ViewBag.idgroupproduct = new SelectList(db.groupproduct, "id", "name", products.idgroupproduct);
            ViewBag.idproduction = new SelectList(db.production, "id", "name", products.idproduction);
            return View(products);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,name,alias,quantity,status,price,promationprice,description,viewcount,createddate,image,fastsell,newproduct,idcategory,idproduction,idgroupproduct,isdelete")] products products)
        {
            if (ModelState.IsValid)
            {
                var temp = db.products.SingleOrDefault(x => x.name.ToLower().Equals(products.name.ToLower()));
                if (temp == null)
                {
                    try
                    {
                        products.image = products.image.Substring(1, products.image.Length - 1);

                    }
                    catch (Exception ex)
                    {

                    }
                    products.alias = HoTro.Instances.convertToUnSign3(products.name);
                    db.Entry(products).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa nón thành công!!";
                    return Redirect("/admin/non");
                }
                else
                    if (temp != null && products.id == temp.id)
                {
                    var description = products.description;
                    var idgroupproduct = products.idgroupproduct;
                    var idproduction = products.idproduction;
                    var idcategory = products.idcategory;
                    products = temp = db.products.Find(products.id);
                    products.status = Boolean.Parse(Request["status"]);
                    products.newproduct = Boolean.Parse(Request["newproduct"]);
                    products.isdelete = Boolean.Parse(Request["isdelete"]);
                    products.name = Request["name"];
                    products.description = description;
                    products.image = Request["image"].Substring(1, Request["image"].Length - 1);
                    products.price = decimal.Parse(Request["price"]);
                    products.quantity = int.Parse(Request["quantity"]);
                    products.promationprice = decimal.Parse(Request["promationprice"]);
                    products.quantity = int.Parse(Request["quantity"]);
                    products.idcategory = idcategory;
                    products.idgroupproduct = idgroupproduct;
                    products.idproduction = idproduction;
                    products.alias = HoTro.Instances.convertToUnSign3(products.name.ToLower());
                    db.Entry(products).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Sửa nón thành công!!";
                    return Redirect("/admin/non");
                }
                else
                {
                    TempData["status"] = "Trùng tên nón!!";
                }
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
