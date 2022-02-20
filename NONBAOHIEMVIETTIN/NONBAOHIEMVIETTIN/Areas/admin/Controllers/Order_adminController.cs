using NONBAOHIEMVIETTIN.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NONBAOHIEMVIETTIN.Areas.admin.Controllers
{
    public class Order_adminController : BaseController
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void ViewBagNoti(List<order> temp, int page)
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
            var temp = db.order.ToList();
            var order = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            ViewBag.check = true;
            return View(order);
        }
        public ActionResult Search(int page = 1)
        {
            var keyword = Request["tukhoa"];
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            ViewBag.check = false;
            var temp = db.order.Where(x =>
            x.id.ToString().ToLower().Equals(keyword.ToLower().Trim()) ||
            x.accounts.username.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.email.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.fullname.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.note.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.phone.ToLower().Equals(keyword.ToLower().Trim()) ||
            x.total.ToString().ToLower().Equals(keyword.ToLower().Trim()) ||
            x.address.ToLower().Contains(keyword.ToLower().Trim())
            ).ToList();
            var order = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index", order);
        }
        [HttpPost]
        public JsonResult delete_order(int id)
        {
            try
            {
                var order = db.order.Find(id);
                if (order.paymentmethod == 0 && order.statuspay == false)
                    foreach (var item in db.orderdetail.Where(x => x.idorder == order.id).ToList())
                    {
                        var products = db.products.Find(item.idproduct);
                        products.quantity += item.quantity;
                        db.Entry(products).State = EntityState.Modified;
                    }
                db.order.Remove(order);
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

        [HttpPost]
        public JsonResult confirm_order(int id)
        {
            try
            {
                var order = db.order.Find(id);
                order.status = true;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = 0,
                    message = "Có lỗi trong quá trình xử lý.Vui lòng thử lại."
                });
            }

            return Json(new
            {
                status = 1,
                message = "Duyệt thành công."
            });
        }

        [HttpPost]
        public JsonResult transfer_order(int id)
        {
            try
            {
                var order = db.order.Find(id);
                if(order.status==false)
                    return Json(new
                    {
                        status = -1,
                        message = "Vui lòng duyệt đơn hàng trước khi thanh toán"
                    });
                order.statuspay = true;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = 0,
                    message = "Có lỗi trong quá trình xử lý.Vui lòng thử lại."
                });
            }

            return Json(new
            {
                status = 1,
                message = "Thanh toán thành công."
            });
        }

    }
}