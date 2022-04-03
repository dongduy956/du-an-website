using NONBAOHIEMVIETTIN.Models;
using OfficeOpenXml;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NONBAOHIEMVIETTIN.Areas.admin.Controllers
{
    public class Order_adminController : BaseController
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void initializationSheet_order_detail(ExcelWorksheet Sheet)
        {
            Sheet.Cells["A1"].Value = "Mã đơn hàng";
            Sheet.Cells["B1"].Value = "Tên sản phẩm";
            Sheet.Cells["C1"].Value = "Giá";
            Sheet.Cells["D1"].Value = "Số lượng";
            Sheet.Cells["E1"].Value = "Thành tiền";
        }
        public void ExportExcel_EPPLUS()
        {
            if (db.order.Count() == 0)
                return;
            ExcelPackage ep = new ExcelPackage();
            ExcelWorksheet Sheet = ep.Workbook.Worksheets.Add("ReportOrder");
            Sheet.Cells["A1"].Value = "Mã đơn hàng";
            Sheet.Cells["B1"].Value = "Tên tài khoản";
            Sheet.Cells["C1"].Value = "Ngày đặt";
            Sheet.Cells["D1"].Value = "Họ tên";
            Sheet.Cells["E1"].Value = "Địa chỉ";
            Sheet.Cells["F1"].Value = "Số điện thoại";
            Sheet.Cells["G1"].Value ="Tổng tiền";
            Sheet.Cells["H1"].Value ="Phương thức thanh toán";
            Sheet.Cells["I1"].Value ="Tình trạng đơn hàng";
            Sheet.Cells["J1"].Value ="Đã chuyển tiền?";
            Sheet.Cells["K1"].Value ="Ghi chú";
            int row = 2;// dòng bắt đầu ghi dữ liệu
            foreach (var item in db.order.OrderBy(x=>x.id).ToList())
            {                
                Sheet.Cells[string.Format("A{0}", row)].Value = item.id;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.accounts.username;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.createdate?.ToString("dd/MM/yyyy");
                Sheet.Cells[string.Format("D{0}", row)].Value = item.fullname;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.address;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.phone;
                Sheet.Cells[string.Format("G{0}", row)].Value = Libary.Instances.convertVND(item.total.ToString());
                Sheet.Cells[string.Format("H{0}", row)].Value = item.paymentmethod==0?"Tiền mặt":"Online";
                Sheet.Cells[string.Format("I{0}", row)].Value = item.status==true?"Đã duyệt":"Chưa duyệt";
                Sheet.Cells[string.Format("J{0}", row)].Value = item.statuspay==true ? "Đã chuyển":"Chưa chuyển";
                Sheet.Cells[string.Format("K{0}", row)].Value = item.note;
                row++;
            }
            row = 2;
            //sheet chi tiết đơn hàng
            ExcelWorksheet Sheet1 = ep.Workbook.Worksheets.Add("OrderDetail");
            initializationSheet_order_detail(Sheet1);
            foreach (var item in db.orderdetail.OrderBy(x=>x.idorder))
            {    
                Sheet1.Cells[string.Format("A{0}", row)].Value = item.idorder;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + "ReportOrder.xlsx");
            Response.BinaryWrite(ep.GetAsByteArray());
            Response.End();
        }
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