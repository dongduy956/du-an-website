using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NONBAOHIEMVIETTIN
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "Login",
               url: "dang-nhap.html",
               defaults: new { controller = "Accounts", action = "Login", id = UrlParameter.Optional },
               namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
           );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "AccountInfo",
               url: "thong-tin-tai-khoan.html",
               defaults: new { controller = "Accounts", action = "AccountInfo", id = UrlParameter.Optional },
               namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
           );
            routes.MapRoute(
         name: "Introduce",
         url: "gioi-thieu.html",
         defaults: new { controller = "Introduce", action = "Index", id = UrlParameter.Optional },
         namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
     );
            routes.MapRoute(
           name: "Contact",
           url: "lien-he.html",
           defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
           namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
       );
            routes.MapRoute(
            name: "Wish",
            url: "yeu-thich.html",
            defaults: new { controller = "Wish", action = "Index", id = UrlParameter.Optional },
            namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
        );

            routes.MapRoute(
              name: "Cart",
              url: "gio-hang.html",
              defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
              namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
          );
            routes.MapRoute(
name: "Search",
url: "tim-kiem.html",
defaults: new { controller = "Products", action = "Search", id = UrlParameter.Optional },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
);
            routes.MapRoute(
           name: "NewsDetail",
           url: "chi-tiet/{alias}.html",
           defaults: new { controller = "News", action = "NewsDetail", id = UrlParameter.Optional },
           namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
       );
            routes.MapRoute(
            name: "ProductsDetail",
            url: "chi-tiet/{alias}.html",
            defaults: new { controller = "Products", action = "ProductDetail", id = UrlParameter.Optional },
            namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
        );

            routes.MapRoute(
           name: "ProductsGroup",
           url: "{alias}/{alia}.html",
           defaults: new { controller = "Products", action = "GroupProducts", id = UrlParameter.Optional },
           namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
       );
            routes.MapRoute(
               name: "NewsIndex",
               url: "{alias}.html",
               defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
           );
            routes.MapRoute(
               name: "ProductsIndex",
               url: "{alias}.html",
               defaults: new { controller = "Products", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }

            );


        }
    }
}
