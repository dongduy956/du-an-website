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
         "admin_Contact_edit",
"admin/sua-lien-he/{alias}.html",
new { controller = "Contact_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
          "admin_Introduce_edit",
"admin/sua-gioi-thieu/{alias}.html",
new { controller = "Introduce_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
           "admin_News_edit",
"admin/sua-tin-tuc/{alias}.html",
new { controller = "News_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
             "admin_Newstype_edit",
"admin/sua-loai-tin/{alias}.html",
new { controller = "Newstype_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
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
"admin_Receipt_search",
"admin/tim-kiem-phieu-nhap.html",
new { controller = "Receipt_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Order_search",
"admin/tim-kiem-don-hang.html",
new { controller = "Order_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Rate_search",
"admin/tim-kiem-danh-gia.html",
new { controller = "Rate_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Subscribe_search",
"admin/tim-kiem-dang-ki.html",
new { controller = "Subscribe_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Feedback_search",
"admin/tim-kiem-phan-hoi.html",
new { controller = "Feedback_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Contact_search",
"admin/tim-kiem-lien-he.html",
new { controller = "Contact_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Introduce_search",
"admin/tim-kiem-gioi-thieu.html",
new { controller = "Introduce_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_News_search",
"admin/tim-kiem-tin-tuc.html",
new { controller = "News_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Newstype_search",
"admin/tim-kiem-loai-tin.html",
new { controller = "Newstype_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
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
"admin_Receipt_create",
"admin/them-moi-phieu-nhap.html",
new { controller = "Receipt_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Contact_create",
"admin/them-moi-lien-he.html",
new { controller = "Contact_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Introduce_create",
"admin/them-moi-gioi-thieu.html",
new { controller = "Introduce_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_News_create",
"admin/them-moi-tin-tuc.html",
new { controller = "News_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Newstype_create",
"admin/them-moi-loai-tin.html",
new { controller = "Newstype_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
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
"admin_Receipt_index",
"admin/nhap-kho.html",
new { controller = "Receipt_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Order_index",
"admin/don-hang.html",
new { controller = "Order_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Rate_index",
"admin/danh-gia.html",
new { controller = "Rate_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Subscribe_index",
"admin/dang-ki.html",
new { controller = "Subscribe_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Feedback_index",
"admin/phan-hoi.html",
new { controller = "Feedback_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
 namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
 "admin_Contact_index",
 "admin/lien-he.html",
 new { controller = "Contact_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
  namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
  "admin_Introduce_index",
  "admin/gioi-thieu.html",
  new { controller = "Introduce_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
   namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
   "admin_News_index",
   "admin/tin-tuc.html",
   new { controller = "News_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
    namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
    "admin_Newstype_index",
    "admin/loai-tin.html",
    new { controller = "Newstype_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
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