using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore; using NonBaoHiemVietTin.Data;
namespace NonBaoHiemVietTin.Controllers;
public sealed class HomeController(AppDbContext db):Controller {
 [HttpGet("/")] public async Task<IActionResult> Index()=>View(await db.Products.AsNoTracking().Where(x=>x.IsDelete!=true&&x.Status==true).OrderByDescending(x=>x.Id).Take(24).ToListAsync());
 [HttpGet("/health")] public IActionResult Health()=>Ok(new{status="ok"});
}
