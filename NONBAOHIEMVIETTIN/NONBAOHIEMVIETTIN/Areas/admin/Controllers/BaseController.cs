using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NONBAOHIEMVIETTIN.Models;
using System.Web.Routing;

namespace NONBAOHIEMVIETTIN.Areas.admin.Controllers
{
    public class BaseController : Controller
    {
       protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var acc = Session["account"] as accounts;
            if(acc== null||!acc.role.name.Equals("admin"))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
            }           
            base.OnActionExecuting(filterContext);
        }
    }
}