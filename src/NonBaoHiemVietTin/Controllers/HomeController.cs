using Microsoft.AspNetCore.Mvc;

namespace NonBaoHiemVietTin.Controllers;

public sealed class HomeController : Controller
{
    [HttpGet("/")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("/health")]
    public IActionResult Health()
    {
        return Ok(new { status = "ok" });
    }
}
