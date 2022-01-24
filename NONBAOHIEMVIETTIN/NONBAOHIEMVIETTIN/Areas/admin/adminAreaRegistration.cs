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
              "admin_Accounts_edit",
"admin/sua-tai-khoan/{alias}.html",
new { controller = "Accounts_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
 namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
               "admin_role_edit",
 "admin/sua-quyen/{alias}.html",
 new { controller = "Role_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
  namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
                 "admin_production_edit",
   "admin/sua-hang-san-xuat/{alias}.html",
   new { controller = "Production_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
    namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
            "admin_groupproduct_edit",
   "admin/sua-nhom-non/{alias}.html",
   new { controller = "GroupProduct_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
    namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
     "admin_category_edit",
     "admin/sua-loai-non/{alias}.html",
     new { controller = "Category_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
      namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
 );
            context.MapRoute(
"admin_Accounts_search",
"admin/tim-kiem-tai-khoan.html",
new { controller = "Accounts_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_role_search",
"admin/tim-kiem-quyen.html",
new { controller = "Role_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_groupproduct_search",
"admin/tim-kiem-nhom-non.html",
new { controller = "GroupProduct_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_category_search",
"admin/tim-kiem-loai-non.html",
new { controller = "Category_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
  "admin_production_search",
  "admin/tim-kiem-hang-san-xuat.html",
  new { controller = "Production_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
   namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
      "admin_products_search",
      "admin/tim-kiem-non.html",
      new { controller = "products_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
       namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
  );
            context.MapRoute(
        "admin_products_edit",
        "admin/sua-non/{alias}.html",
        new { controller = "products_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
         namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
    );
            context.MapRoute(
"admin_Accounts_create",
"admin/them-moi-tai-khoan.html",
new { controller = "Accounts_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
   "admin_Role_create",
   "admin/them-moi-quyen.html",
   new { controller = "Role_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
    namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
    "admin_Production_create",
    "admin/them-moi-hang-san-xuat.html",
    new { controller = "Production_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
     namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
    "admin_groupproduct_create",
    "admin/them-moi-nhom-non.html",
    new { controller = "GroupProduct_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
     namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
       "admin_category_create",
       "admin/them-moi-loai-non.html",
       new { controller = "Category_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
        namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
   );
            context.MapRoute(
         "admin_products_create",
         "admin/them-moi-non.html",
         new { controller = "products_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
          namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
     );
            context.MapRoute(
       "admin_account_index",
       "admin/tai-khoan.html",
       new { controller = "Accounts_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
        namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
   );
            context.MapRoute(
         "admin_role_index",
         "admin/quyen.html",
         new { controller = "Role_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
          namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
     );
            context.MapRoute(
            "admin_groupproduct_index",
            "admin/nhom-non.html",
            new { controller = "GroupProduct_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
             namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
        );
            context.MapRoute(
             "admin_production_index",
             "admin/hang-san-xuat.html",
             new { controller = "Production_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
              namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
         );
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
                new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
               namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
            );
        }
    }
}