﻿using NONBAOHIEMVIETTIN.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NONBAOHIEMVIETTIN.Controllers
{
    public class ProductsController : Controller
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void ViewBagNoti(List<products> temp,int check,int page)
        {
            if (temp.Count() > 0)
            {
                int last = int.Parse(Math.Ceiling((double)temp.Count() / pageSize).ToString());
                ViewBag.last = last;
                ViewBag.noti = "Showing " + page + "-" + last + " of " + temp.Count() + " results";
                ViewBag.check = check;
            }
        }
        // GET: Products
        public ActionResult Index(string alias, int page = 1)
        {
            var temp = db.products.Where(x => x.category.alias.Equals(alias) || x.production.alias.Equals(alias)).OrderByDescending(x => x.id).ToList();
            var products = temp.ToPagedList(page, pageSize);
            var category = db.category.SingleOrDefault(x => x.alias.Equals(alias));
            ViewBag.alias = category != null ? category.name : db.production.SingleOrDefault(x => x.alias.Equals(alias)).name;
            ViewBagNoti(temp,0, page);
            return View(products);
        }
        public ActionResult GroupProducts(string alias, string alia,int page=1)
        {
            var temp = db.products.Where(x => x.category.alias.Equals(alias) && x.groupproduct.alias.Equals(alia)).OrderByDescending(x => x.id).ToList();
            var products = temp.ToPagedList(page, pageSize);
            ViewBag.alias = db.groupproduct.SingleOrDefault(x => x.alias.Equals(alia)).name;
            ViewBagNoti(temp,1, page);
            return View("Index", products);
        }
        public ActionResult ProductDetail(string alias)
        {
            productdetail prd = new productdetail();
            prd.product= db.products.SingleOrDefault(x => x.alias.Equals(alias));
            prd.lstProductCategory = db.products.Where(x => x.id != prd.product.id && x.category.id == prd.product.category.id).ToList();
            prd.lstProductProduction= db.products.Where(x => x.id != prd.product.id && x.production.id == prd.product.production.id).ToList();
            prd.lstProductGroup= db.products.Where(x => x.id != prd.product.id && x.groupproduct.id == prd.product.groupproduct.id && x.category.id == prd.product.category.id).ToList();
            prd.lstRate = db.rate.Where(x => x.id_product == prd.product.id).ToList();
            return View(prd);
        }

        public ActionResult Search(int page=1)
        {
            try
            {
                string keyword = Request["tukhoa"].ToString().ToLower();
                var temp = db.products.Where(x => x.category.name.ToLower().Contains(keyword)
                || x.production.name.ToLower().Contains(keyword)
                || x.name.ToLower().Contains(keyword)
                || x.groupproduct.name.ToLower().Contains(keyword)
                || x.id.ToString().ToLower().Equals(keyword)).OrderByDescending(x => x.id).ToList();
                var products = temp.ToPagedList(page, pageSize);
                ViewBag.alias = "Tìm kiếm: " + keyword;
                ViewBagNoti(temp,2, page);
                return View("Index", products);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
        }

        [HttpPost]
        public JsonResult Ratting(rate ratting)
        {            
            var acc = Session["account"] as accounts;
            if ( acc== null)
                return Json(new
                {
                    status = 0,
                    message = "Bạn cần đăng nhập để đánh giá sản phẩm này."
                });
            ratting.id_account = acc.id;
            ratting.createdate = DateTime.Now;
            try
            {
                db.rate.Add(ratting);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = -1,
                    message = "Có lỗi trong quá trình xử lý!!"
                });
            }
            var countRate = db.rate.Count(x => x.id_product == ratting.id_product);
            var avgStar = db.rate.Where(x => x.id_product == ratting.id_product).Sum(x => x.star) / (double)countRate;
            
            return Json(new
            {
                status = 1,
                message = "Đánh giá thành công!!",
                countRate,
                avgStar = Math.Round((decimal)avgStar,0),
                issocial=acc.issocial,
                image=acc.image,
                fullname=acc.fullname,
                createDate=DateTime.Now.ToShortDateString()
            });
        }
    }
}
