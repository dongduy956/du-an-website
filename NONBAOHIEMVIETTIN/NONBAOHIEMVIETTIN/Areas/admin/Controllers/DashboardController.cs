using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NONBAOHIEMVIETTIN.Models;
namespace NONBAOHIEMVIETTIN.Areas.admin.Controllers
{
    public class DashboardController : BaseController
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        public ActionResult Index()
        {
            return View();
        }       
        [HttpPost]
        public JsonResult LoadMoneysMonth(int month, int year)
        {
            var dayofmonth = HoTro.Instances.dayOfMonth(month, year);
            decimal[] moneys = new decimal[dayofmonth];
            string[] days = new string[dayofmonth];
            for (int i = 0; i < dayofmonth; i++)
            {
                days[i] = "N" + (i + 1).ToString();
                var totalOrder = db.order.Where(x => x.statuspay == true && x.createdate.Value.Day == (i + 1) && x.createdate.Value.Month == month && x.createdate.Value.Year == year).Sum(x => x.total);
                var totalReceipt = db.receipt.Where(x => x.createdate.Value.Day == (i + 1) && x.createdate.Value.Month == month && x.createdate.Value.Year == year).Sum(x => x.total);
                moneys[i] = (decimal)((totalOrder == null ? 0 : totalOrder)- (totalReceipt == null ? 0 : totalReceipt));
            }

            return Json(new
            {
                moneys,
                days
            });
        }

        [HttpPost]
        public JsonResult LoadMoneysDay(int day,int month, int year)
        {
                
            var moneysCollect = db.order.Where(x => x.statuspay == true && x.createdate.Value.Day == day && x.createdate.Value.Month == month && x.createdate.Value.Year == year).Sum(x => x.total);
            var moneysSpend = db.receipt.Where(x =>x.createdate.Value.Day == day && x.createdate.Value.Month == month && x.createdate.Value.Year == year).Sum(x => x.total);
            return Json(new
            {
                moneysCollect,
                moneysSpend
            });
        }

        [HttpPost]
        public JsonResult LoadMoneysYear(int year)
        {
            decimal[] moneys = new decimal[12];
            for (int i = 0; i < 12; i++)
            {
                var totalOrder = db.order.Where(x => x.statuspay == true && x.createdate.Value.Month ==(i+1) && x.createdate.Value.Year == year).Sum(x => x.total);
                var totalReceipt = db.receipt.Where(x =>x.createdate.Value.Month ==(i+1) && x.createdate.Value.Year == year).Sum(x => x.total);
                moneys[i] = (decimal)((totalOrder == null ? 0 : totalOrder)- (totalReceipt == null ? 0 : totalReceipt));
            }

            return Json(new
            {
                moneys           
            });
        }
    }
}