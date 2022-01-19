using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace NONBAOHIEMVIETTIN
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            if (!File.Exists(Server.MapPath("~/countOnline.txt")))
                File.WriteAllText(Server.MapPath("~/countOnline.txt"), "0");            
            Application["visited"] = int.Parse(File.ReadAllText(Server.MapPath("~/countOnline.txt")));
        }              
        void Session_Start()
        {
            Application["visited"] = Application["visited"];

            if (Application["visiting"] == null)
                Application["visiting"] = 1;
            else
                Application["visiting"] = (int)Application["visiting"] + 1;
            // Tăng số đã truy cập lên 1 nếu có khách truy cập
            Application["visited"] = (int)Application["visited"] + 1;
            File.WriteAllText(Server.MapPath("~/countOnline.txt"), Application["visited"].ToString());
        }
        void Session_End()
        {
            Application["visiting"] = (int)Application["visiting"] - 1;
        }
    }
}
