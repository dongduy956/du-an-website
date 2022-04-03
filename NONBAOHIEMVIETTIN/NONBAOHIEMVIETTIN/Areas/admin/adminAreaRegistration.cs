using NONBAOHIEMVIETTIN.Models;
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
        "admin_Error_NotFound",
"loi-404",
new { controller = "Error", action = "NotFound", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
        "admin_Brand_edit",
"sua-doi-tac/{alias}",
new { controller = "Brand_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);       
            context.MapRoute(
         "admin_Contact_edit",
"sua-lien-he/{alias}",
new { controller = "Contact_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
          "admin_Introduce_edit",
"sua-gioi-thieu/{alias}",
new { controller = "Introduce_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
           "admin_News_edit",
"sua-tin-tuc/{alias}",
new { controller = "News_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
             "admin_Newstype_edit",
"sua-loai-tin/{alias}",
new { controller = "Newstype_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
              "admin_Accounts_edit",
"sua-tai-khoan/{alias}",
new { controller = "Accounts_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
 new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
               "admin_role_edit",
 "sua-quyen/{alias}",
 new { controller = "Role_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
  new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
                 "admin_production_edit",
   "sua-hang-san-xuat/{alias}",
   new { controller = "Production_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
    new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
            "admin_groupproduct_edit",
   "sua-nhom-non/{alias}",
   new { controller = "GroupProduct_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
    new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
     "admin_category_edit",
     "sua-loai-non/{alias}",
     new { controller = "Category_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
      new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
 );
            context.MapRoute(
"admin_Brand_search",
"tim-kiem-doi-tac",
new { controller = "Brand_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Receipt_search",
"tim-kiem-phieu-nhap",
new { controller = "Receipt_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);            
                context.MapRoute(
"admin_Order_ExportExcel_EPPLUS",
"xuat-excel-don-hang",
new { controller = "Order_admin", action = "ExportExcel_EPPLUS", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") }, namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Order_search",
"tim-kiem-don-hang",
new { controller = "Order_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Rate_search",
"tim-kiem-danh-gia",
new { controller = "Rate_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Subscribe_search",
"tim-kiem-dang-ki",
new { controller = "Subscribe_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Feedback_search",
"tim-kiem-phan-hoi",
new { controller = "Feedback_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Contact_search",
"tim-kiem-lien-he",
new { controller = "Contact_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Introduce_search",
"tim-kiem-gioi-thieu",
new { controller = "Introduce_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_News_search",
"tim-kiem-tin-tuc",
new { controller = "News_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Newstype_search",
"tim-kiem-loai-tin",
new { controller = "Newstype_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Accounts_search",
"tim-kiem-tai-khoan",
new { controller = "Accounts_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_role_search",
"tim-kiem-quyen",
new { controller = "Role_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_groupproduct_search",
"tim-kiem-nhom-non",
new { controller = "GroupProduct_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_category_search",
"tim-kiem-loai-non",
new { controller = "Category_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
  "admin_production_search",
  "tim-kiem-hang-san-xuat",
  new { controller = "Production_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
   new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
      "admin_products_search",
      "tim-kiem-non",
      new { controller = "products_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
       new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
  );
            context.MapRoute(
        "admin_products_edit",
        "sua-non/{alias}",
        new { controller = "products_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
         new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
    );
            context.MapRoute(
"admin_Brand_create",
"them-moi-doi-tac",
new { controller = "Brand_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Receipt_ExportExcel_EPPLUS",
"xuat-excel-phieu-nhap",
new { controller = "Receipt_admin", action = "ExportExcel_EPPLUS", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") }, namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Receipt_create",
"them-moi-phieu-nhap",
new { controller = "Receipt_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Contact_create",
"them-moi-lien-he",
new { controller = "Contact_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Introduce_create",
"them-moi-gioi-thieu",
new { controller = "Introduce_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_News_create",
"them-moi-tin-tuc",
new { controller = "News_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Newstype_create",
"them-moi-loai-tin",
new { controller = "Newstype_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Accounts_create",
"them-moi-tai-khoan",
new { controller = "Accounts_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
   "admin_Role_create",
   "them-moi-quyen",
   new { controller = "Role_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
    new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
    "admin_Production_create",
    "them-moi-hang-san-xuat",
    new { controller = "Production_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
     new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
    "admin_groupproduct_create",
    "them-moi-nhom-non",
    new { controller = "GroupProduct_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
     new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
       "admin_category_create",
       "them-moi-loai-non",
       new { controller = "Category_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
        new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
   );
            context.MapRoute(
         "admin_products_create",
         "them-moi-non",
         new { controller = "products_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
          new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
     );
            context.MapRoute(
"admin_Brand_index",
"doi-tac",
new { controller = "Brand_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Receipt_index",
"nhap-kho",
new { controller = "Receipt_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Order_index",
"don-hang",
new { controller = "Order_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Rate_index",
"danh-gia",
new { controller = "Rate_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Subscribe_index",
"dang-ki",
new { controller = "Subscribe_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Feedback_index",
"phan-hoi",
new { controller = "Feedback_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
 new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
 "admin_Contact_index",
 "lien-he",
 new { controller = "Contact_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
  new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
  "admin_Introduce_index",
  "gioi-thieu",
  new { controller = "Introduce_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
   new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
   "admin_News_index",
   "tin-tuc",
   new { controller = "News_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
    new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
    "admin_Newstype_index",
    "loai-tin",
    new { controller = "Newstype_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
     new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
       "admin_account_index",
       "tai-khoan",
       new { controller = "Accounts_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
        new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
   );
            context.MapRoute(
         "admin_role_index",
         "quyen",
         new { controller = "Role_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
          new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
     );
            context.MapRoute(
            "admin_groupproduct_index",
            "nhom-non",
            new { controller = "GroupProduct_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
             new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
        );
            context.MapRoute(
             "admin_production_index",
             "hang-san-xuat",
             new { controller = "Production_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
              new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
         );
            context.MapRoute(
              "admin_category_index",
              "loai-non",
              new { controller = "Category_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
               new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
          );
            context.MapRoute(
               "admin_products_index",
               "non",
               new { controller = "products_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
               new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
           );
            context.MapRoute(
               "admin_login",
               "dang-nhap",
               new { controller = "Login", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
               new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
           );
            context.MapRoute(
                "admin_default",
                "{controller}/{action}/{id}",
                new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
               new { controller = new SubdomainRouteConstraint("admin.") },namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
            );
        }
    }
}