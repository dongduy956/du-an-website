using System.Security.Cryptography; using System.Text; using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore; using NonBaoHiemVietTin.Data; using NonBaoHiemVietTin.Infrastructure; using NonBaoHiemVietTin.Models;
namespace NonBaoHiemVietTin.Controllers;
public sealed class AccountsController(AppDbContext db):Controller {
 const string AccountKey="account";
 [HttpGet] public IActionResult Login()=>View();
 [HttpPost] public async Task<IActionResult> Login(string usernamelogin,string passwordlogin){var hash=Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(passwordlogin))).ToLowerInvariant();var a=await db.Accounts.AsNoTracking().FirstOrDefaultAsync(x=>x.Username==usernamelogin&&x.Password==hash&&x.IsSocial==0);if(a==null)return Json(-1);if(a.Status!=true)return Json(0);HttpContext.Session.SetJson(AccountKey,a);return Json(1);}
 [HttpPost] public IActionResult Logout(){HttpContext.Session.Clear();return Json(1);}
 [HttpGet] public async Task<IActionResult> AccountInfo(int page=1){var a=HttpContext.Session.GetJson<Account>(AccountKey);if(a==null)return Redirect("/dang-nhap");return View(await db.Orders.AsNoTracking().Where(x=>x.IdAccount==a.Id).OrderByDescending(x=>x.Id).Skip((page-1)*4).Take(4).ToListAsync());}
 [HttpGet] public async Task<IActionResult> AccountWheel(int page=1){var a=HttpContext.Session.GetJson<Account>(AccountKey);if(a==null)return Redirect("/dang-nhap");return View(await db.Wheels.AsNoTracking().Where(x=>x.IdAccount==a.Id).OrderByDescending(x=>x.Id).Skip((page-1)*10).Take(10).ToListAsync());}
 [HttpGet] public async Task<IActionResult> AccountRecharge(int page=1){var a=HttpContext.Session.GetJson<Account>(AccountKey);if(a==null)return Redirect("/dang-nhap");return View(await db.HistoryRecharges.AsNoTracking().Where(x=>x.IdAccount==a.Id).OrderByDescending(x=>x.Id).Skip((page-1)*10).Take(10).ToListAsync());}
 [HttpGet] public async Task<IActionResult> AccountWithdraw(int page=1){var a=HttpContext.Session.GetJson<Account>(AccountKey);if(a==null)return Redirect("/dang-nhap");return View(await db.HistoryWithdraws.AsNoTracking().Where(x=>x.IdAccount==a.Id).OrderByDescending(x=>x.Id).Skip((page-1)*10).Take(10).ToListAsync());}
}
