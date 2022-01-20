using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NONBAOHIEMVIETTIN.Areas.admin.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: admin/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}