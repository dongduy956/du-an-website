using NONBAOHIEMVIETTIN.Models;
using OfficeOpenXml;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NONBAOHIEMVIETTIN.Areas.admin.Controllers
{
    public class Receipt_adminController : BaseController
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void ViewBagNoti(List<receipt> temp, int page)
        {
            ViewBag.last = 1;
            if (temp.Count() > 0)
            {
                int last = int.Parse(Math.Ceiling((double)temp.Count() / pageSize).ToString());
                ViewBag.last = last;
                ViewBag.noti = "Showing " + page + "-" + last + " of " + temp.Count() + " results";
            }
        }
        void initializationSheet_order_detail(ExcelWorksheet Sheet)
        {
            Sheet.Cells["A1"].Value = "Mã phiếu nhập";
            Sheet.Cells["B1"].Value = "Tên sản phẩm";
            Sheet.Cells["C1"].Value = "Giá";
            Sheet.Cells["D1"].Value = "Số lượng";
            Sheet.Cells["E1"].Value = "Thành tiền";
        }
        public void ExportExcel_EPPLUS()
        {
            if (db.receipt.Count() == 0)
                return;
            ExcelPackage ep = new ExcelPackage();
            ExcelWorksheet Sheet = ep.Workbook.Worksheets.Add("ReportReceipt");
            Sheet.Cells["A1"].Value = "Mã phiếu nhập";
            Sheet.Cells["B1"].Value = "Tên tài khoản";
            Sheet.Cells["C1"].Value = "Ngày nhập";
            Sheet.Cells["D1"].Value = "Tổng giá";
            int row = 2;// dòng bắt đầu ghi dữ liệu
            foreach (var item in db.receipt.OrderBy(x=>x.id).ToList())
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.id;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.accounts.username;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.createdate?.ToString("dd/MM/yyyy");
                Sheet.Cells[string.Format("D{0}", row)].Value = Libary.Instances.convertVND(item.total.ToString());
                row++;
            }
            row = 2;
            //sheet chi tiết đơn hàng
            ExcelWorksheet Sheet1 = ep.Workbook.Worksheets.Add("ReceiptDetail");
            initializationSheet_order_detail(Sheet1);
            foreach (var item in db.receiptdetail.OrderBy(x => x.idreceipt))
            {
                Sheet1.Cells[string.Format("A{0}", row)].Value = item.idreceipt;
                Sheet1.Cells[string.Format("B{0}", row)].Value = item.products.name;
                Sheet1.Cells[string.Format("C{0}", row)].Value = Libary.Instances.convertVND(item.price.ToString()); ;
                Sheet1.Cells[string.Format("D{0}", row)].Value = item.quantity;
                Sheet1.Cells[string.Format("E{0}", row)].Value = Libary.Instances.convertVND(item.subtotal.ToString());
                row++;
            }
            Sheet.Cells["A:AZ"].AutoFitColumns();
            Sheet1.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=" + "ReportReceipt.xlsx");
            Response.BinaryWrite(ep.GetAsByteArray());
            Response.End();
        }
        public ActionResult Index(int page = 1)
        {
            var temp = db.receipt.ToList();
            var receipt = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            ViewBag.check = true;
            return View(receipt);
        }
        public ActionResult Search(int page = 1)
        {
            var keyword = Request["tukhoa"];
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }
            ViewBag.check = false;
            var temp = db.receipt.Where(x =>
            x.id.ToString().ToLower().Equals(keyword.ToLower().Trim()) ||
            x.accounts.username.ToLower().Contains(keyword.ToLower().Trim()) ||
            x.total.ToString().ToLower().Equals(keyword.ToLower().Trim()) ||
            x.createdate.ToString().ToLower().Contains(keyword.ToLower().Trim())
            ).ToList();
            var receipt = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View("Index", receipt);
        }
        [HttpPost]
        public JsonResult delete_receipt(int id)
        {
            try
            {
                var receipt = db.receipt.Find(id);
                db.receipt.Remove(receipt);
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
        public JsonResult Create(receiptdetail[] lstreceiptdetail)
        {
            try
            {
                var receipt = new receipt();
                receipt.idaccount = (Session["account_admin"] as accounts).id;
                receipt.total = 0;
                receipt.createdate = DateTime.Now;
                db.receipt.Add(receipt);
                db.SaveChanges();
                var idreceipt = db.receipt.OrderByDescending(x => x.id).FirstOrDefault().id;
                foreach (var item in lstreceiptdetail)
                {
                    item.idreceipt = idreceipt;
                    db.receiptdetail.Add(item);
                    db.SaveChanges();
                }
                TempData["status"] = "Thêm mới phiếu nhập thành công";
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status=0
                });
            }
            return Json(new
            {
                status = 1
            });
        }
    }
}