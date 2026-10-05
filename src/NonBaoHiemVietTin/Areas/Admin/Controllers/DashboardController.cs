using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore; using NonBaoHiemVietTin.Data;
namespace NonBaoHiemVietTin.Areas.Admin.Controllers;
[Area("Admin"),Route("admin")]
public sealed class DashboardController(AppDbContext db):Controller {
 [HttpGet("")] public async Task<IActionResult> Index(){ViewBag.Accounts=await db.Accounts.CountAsync();ViewBag.Products=await db.Products.CountAsync();ViewBag.Orders=await db.Orders.CountAsync();ViewBag.Revenue=await db.Orders.Where(x=>x.StatusPay==true).SumAsync(x=>x.Total??0);return View();}
}
