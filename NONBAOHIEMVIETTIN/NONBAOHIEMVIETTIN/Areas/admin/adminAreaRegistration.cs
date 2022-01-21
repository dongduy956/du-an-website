using System.Web.Mvc;

namespace NONBAOHIEMVIETTIN.Areas.admin
{
    public class adminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            context.MapRoute(
              "admin_category_index",
              "admin/loai-non.html",
              new { controller = "Category_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
               namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
          );
            context.MapRoute(
               "admin_products_index",
               "admin/non.html",
               new { controller = "products_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },               
               namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
           );
            context.MapRoute(
               "admin_login",
               "admin/dang-nhap.html",
               new { controller = "Login", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
               namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
           );
            context.MapRoute(
                "admin_default",
                "admin/{controller}/{action}/{id}",
                new { controller="Dashboard", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
               namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
            );
        }
    }
}