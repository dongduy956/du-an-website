using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NONBAOHIEMVIETTIN.Models;
namespace NONBAOHIEMVIETTIN.Controllers
{
    public class IntroduceController : Controller
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        // GET: Introduce
        public ActionResult Index()
        {
            return View(db.introduce.FirstOrDefault(x=>x.status==true));
        }
    }
}