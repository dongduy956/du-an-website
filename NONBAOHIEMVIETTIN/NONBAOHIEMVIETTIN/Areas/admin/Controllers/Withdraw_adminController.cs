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
    public class Withdraw_adminController : BaseController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        private const int pageSize = 10;
        private void ViewBagNoti(List<history_withdraw> temp, int page)
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
            var temp = db.history_withdraw.ToList();
            var withdraw = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            ViewBag.check = true;
            return View(withdraw);
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
            var temp = db.history_withdraw.Where(x =>
            x.id.ToString().ToLower().Equals(keyword) ||
            x.amount_money.ToString().ToLower().Equals(keyword) ||
            x.accounts.username.Equals(keyword) ||
            x.bank_name.ToLower().Contains(keyword) ||
            x.bank_number.Equals(keyword) ||
            x.create_date.ToString().Equals(keyword) ||
            x.fullname.ToLower().Contains(keyword) ||
            (x.note == null ? "" : x.note).ToLower().Contains(keyword)
            ).ToList();
            var history_withdraw = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index", history_withdraw);
        }

        [HttpPost]
        public JsonResult ConfirmWithdraw(int id,string note)
        {
            var withdraw = db.history_withdraw.Find(id);
            if (withdraw == null)
                return Json(new
                {
                    status=false,
                    message="Có lỗi xảy ra."
                });
            withdraw.status = 1;
            withdraw.confirm_date = DateTime.Now;
            withdraw.note = note;
            db.Entry(withdraw).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                status=true,
                message="Duyệt lệnh thành công."
            });
        }

        [HttpPost]
        public JsonResult RefuseWithdraw(int id,string note)
        {
            var withdraw = db.history_withdraw.Find(id);
            if (withdraw == null)
                return Json(new
                {
                    status = false,
                    message = "Có lỗi xảy ra."
                });
            withdraw.status = -1;
            withdraw.confirm_date = DateTime.Now;
            withdraw.note = note;
            db.Entry(withdraw).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                status = true,
                message = "Đã từ chối lệnh."
            });
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
