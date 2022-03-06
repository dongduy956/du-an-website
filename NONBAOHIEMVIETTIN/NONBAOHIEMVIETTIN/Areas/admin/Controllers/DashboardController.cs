using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NONBAOHIEMVIETTIN.Models;
using NONBAOHIEMVIETTIN.Areas.admin.Models;
using System.Data.Entity;

namespace NONBAOHIEMVIETTIN.Areas.admin.Controllers
{
    public class DashboardController : BaseController
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Backup()
        {
            try
            {
                string dbname = db.Database.Connection.Database;
                string url = "https://" + Request.Url.Authority; 
                string sqlCommand = @"BACKUP DATABASE [{0}] TO  DISK = N'{1}' WITH NOFORMAT, NOINIT,  NAME = N'MyAir-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, string.Format(sqlCommand, dbname, Server.MapPath("~/" + dbname + ".bak")));
                return Json(new
                {
                    status = 1,
                    message = "Sau lưu thành công."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = 0,
                    message = "Có lỗi xảy ra!"
                });
            }
            
        }
        decimal billMoneys(List<order> orders, List<receipt> receipts)
        {
            decimal total = 0;
            var lstProductsReceipt = new List<Spend>();
            var lstProductsOrder = new List<Spend>();
            //tính tổng tiền trong danh sách nhập kho
            foreach (var receipt in receipts)
            {
                var receiptdetails = db.receiptdetail.Where(x => x.idreceipt == receipt.id).ToList();
                foreach (var receiptdetail in receiptdetails)
                {
                    var index = lstProductsReceipt.FindIndex(x => x.idproduct == receiptdetail.idproduct);
                    var subtotal = receiptdetail.subtotal;
                    if (index == -1)
                        lstProductsReceipt.Add(new Spend()
                        {
                            idproduct = receiptdetail.idproduct,
                            total = subtotal
                        });
                    else
                    {
                        var item = lstProductsReceipt[index];
                        item.total += subtotal;
                        lstProductsReceipt.Insert(index, item);
                    }
                }
            }

            //tính tổng tiền trong danh sách đặt hàng
            foreach (var order in orders)
            {
                var orderdetails = db.orderdetail.Where(x => x.idorder == order.id).ToList();
                foreach (var orderdetail in orderdetails)
                {
                    var index = lstProductsOrder.FindIndex(x => x.idproduct == orderdetail.idproduct);
                    var subtotal = (decimal)orderdetail.subtotal;
                    if (index == -1)
                        lstProductsOrder.Add(new Spend()
                        {
                            idproduct = orderdetail.idproduct,
                            total = subtotal
                        });
                    else
                    {
                        var item = lstProductsOrder[index];
                        item.total += subtotal;
                        lstProductsOrder.Insert(index, item);
                    }
                }
            }
            // tính tiền lời
            bool check = false;
            foreach(var itemReceipt in lstProductsReceipt)
            {
                check = false;
                foreach(var itemOrder in lstProductsOrder)
                {                
                    if(itemReceipt.idproduct==itemOrder.idproduct)
                    {
                        total += itemOrder.total - itemReceipt.total;
                        check = true;
                        break;
                    }
                }
                if (!check)
                    total -= itemReceipt.total;
            }
            return total;
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
                var orders = db.order.Where(x => x.statuspay == true && x.createdate.Value.Day == (i + 1) && x.createdate.Value.Month == month && x.createdate.Value.Year == year).ToList();
                var receipts = db.receipt.Where(x => x.createdate.Value.Day == (i + 1) && x.createdate.Value.Month == month && x.createdate.Value.Year == year).ToList();
                moneys[i] = billMoneys(orders, receipts);
            }
            return Json(new
            {
                moneys,
                days
            });
        }

        [HttpPost]
        public JsonResult LoadMoneysDay(int day, int month, int year)
        {

            var countOrders = db.order.Where(x => x.statuspay == true && x.createdate.Value.Day == day && x.createdate.Value.Month == month && x.createdate.Value.Year == year).Count();
            var countReceipts = db.receipt.Where(x => x.createdate.Value.Day == day && x.createdate.Value.Month == month && x.createdate.Value.Year == year).Count();
            return Json(new
            {
                countOrders,
                countReceipts
            });
        }

        [HttpPost]
        public JsonResult LoadMoneysYear(int year)
        {
            decimal[] moneys = new decimal[12];
            for (int i = 0; i < 12; i++)
            {
                var orders=db.order.Where(x => x.statuspay == true && x.createdate.Value.Month == (i + 1) && x.createdate.Value.Year == year).ToList();
                var receipts = db.receipt.Where(x => x.createdate.Value.Month == (i + 1) && x.createdate.Value.Year == year).ToList();
                moneys[i] = billMoneys(orders,receipts);
            }

            return Json(new
            {
                moneys
            });
        }
    }
}