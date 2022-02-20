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

            if (!File.Exists(Server.MapPath("CountNumber.txt")))
                File.WriteAllText(Server.MapPath("~/CountNumber.txt"), "0");
            Application["Visited"] = int.Parse(File.ReadAllText(Server.MapPath("~/CountNumber.txt")));

        }
        void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            if (Application["Visiting"] == null)
                Application["Visiting"] = 1;
            else
                Application["Visiting"] = (int)Application["Visiting"] + 1;
            // Tăng số đã truy cập lên 1 nếu có khách truy cập
            Application["Visited"] = (int)Application["Visited"] + 1;
            File.WriteAllText(Server.MapPath("~/CountNumber.txt"), Application["Visited"].ToString());
            Application.UnLock();

        }
        void Session_End(object sender, EventArgs e)
        {
            Application["Visiting"] = (int)Application["Visiting"] - 1;
        }

    }
}
