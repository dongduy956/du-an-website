using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore; using NonBaoHiemVietTin.Data;
namespace NonBaoHiemVietTin.Controllers; public sealed class IntroduceController(AppDbContext db):Controller { [HttpGet] public async Task<IActionResult> Index()=>View(await db.Introduces.AsNoTracking().FirstOrDefaultAsync(x=>x.Status==true)); }
