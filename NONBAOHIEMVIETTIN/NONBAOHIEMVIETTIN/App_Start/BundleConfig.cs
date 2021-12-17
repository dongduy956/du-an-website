using System.Configuration;
using System.Web;
using System.Web.Optimization;

namespace NONBAOHIEMVIETTIN
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {           
            BundleTable.EnableOptimizations = bool.Parse(ConfigurationManager.AppSettings["EnableBundles"]);
        }
    }
}
