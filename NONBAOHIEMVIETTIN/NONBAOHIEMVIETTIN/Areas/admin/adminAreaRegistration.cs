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
        "admin_Brand_edit",
"admin/sua-doi-tac/{alias}",
new { controller = "Brand_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
         "admin_Contact_edit",
"admin/sua-lien-he/{alias}",
new { controller = "Contact_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
          "admin_Introduce_edit",
"admin/sua-gioi-thieu/{alias}",
new { controller = "Introduce_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
           "admin_News_edit",
"admin/sua-tin-tuc/{alias}",
new { controller = "News_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
             "admin_Newstype_edit",
"admin/sua-loai-tin/{alias}",
new { controller = "Newstype_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
              "admin_Accounts_edit",
"admin/sua-tai-khoan/{alias}",
new { controller = "Accounts_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
 namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
               "admin_role_edit",
 "admin/sua-quyen/{alias}",
 new { controller = "Role_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
  namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
                 "admin_production_edit",
   "admin/sua-hang-san-xuat/{alias}",
   new { controller = "Production_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
    namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
            "admin_groupproduct_edit",
   "admin/sua-nhom-non/{alias}",
   new { controller = "GroupProduct_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
    namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
     "admin_category_edit",
     "admin/sua-loai-non/{alias}",
     new { controller = "Category_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
      namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
 );
            context.MapRoute(
"admin_Brand_search",
"admin/tim-kiem-doi-tac",
new { controller = "Brand_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Receipt_search",
"admin/tim-kiem-phieu-nhap",
new { controller = "Receipt_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Order_search",
"admin/tim-kiem-don-hang",
new { controller = "Order_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Rate_search",
"admin/tim-kiem-danh-gia",
new { controller = "Rate_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Subscribe_search",
"admin/tim-kiem-dang-ki",
new { controller = "Subscribe_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Feedback_search",
"admin/tim-kiem-phan-hoi",
new { controller = "Feedback_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Contact_search",
"admin/tim-kiem-lien-he",
new { controller = "Contact_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Introduce_search",
"admin/tim-kiem-gioi-thieu",
new { controller = "Introduce_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_News_search",
"admin/tim-kiem-tin-tuc",
new { controller = "News_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Newstype_search",
"admin/tim-kiem-loai-tin",
new { controller = "Newstype_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Accounts_search",
"admin/tim-kiem-tai-khoan",
new { controller = "Accounts_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_role_search",
"admin/tim-kiem-quyen",
new { controller = "Role_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_groupproduct_search",
"admin/tim-kiem-nhom-non",
new { controller = "GroupProduct_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_category_search",
"admin/tim-kiem-loai-non",
new { controller = "Category_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
  "admin_production_search",
  "admin/tim-kiem-hang-san-xuat",
  new { controller = "Production_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
   namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
      "admin_products_search",
      "admin/tim-kiem-non",
      new { controller = "products_admin", action = "Search", id = UrlParameter.Optional, Area = "Admin" },
       namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
  );
            context.MapRoute(
        "admin_products_edit",
        "admin/sua-non/{alias}",
        new { controller = "products_admin", action = "Edit", id = UrlParameter.Optional, Area = "Admin" },
         namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
    );
            context.MapRoute(
"admin_Brand_create",
"admin/them-moi-doi-tac",
new { controller = "Brand_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Receipt_create",
"admin/them-moi-phieu-nhap",
new { controller = "Receipt_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Contact_create",
"admin/them-moi-lien-he",
new { controller = "Contact_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Introduce_create",
"admin/them-moi-gioi-thieu",
new { controller = "Introduce_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_News_create",
"admin/them-moi-tin-tuc",
new { controller = "News_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Newstype_create",
"admin/them-moi-loai-tin",
new { controller = "Newstype_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Accounts_create",
"admin/them-moi-tai-khoan",
new { controller = "Accounts_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
   "admin_Role_create",
   "admin/them-moi-quyen",
   new { controller = "Role_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
    namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
    "admin_Production_create",
    "admin/them-moi-hang-san-xuat",
    new { controller = "Production_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
     namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
    "admin_groupproduct_create",
    "admin/them-moi-nhom-non",
    new { controller = "GroupProduct_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
     namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
       "admin_category_create",
       "admin/them-moi-loai-non",
       new { controller = "Category_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
        namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
   );
            context.MapRoute(
         "admin_products_create",
         "admin/them-moi-non",
         new { controller = "products_admin", action = "Create", id = UrlParameter.Optional, Area = "Admin" },
          namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
     );
            context.MapRoute(
"admin_Brand_index",
"admin/doi-tac",
new { controller = "Brand_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Receipt_index",
"admin/nhap-kho",
new { controller = "Receipt_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Order_index",
"admin/don-hang",
new { controller = "Order_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Rate_index",
"admin/danh-gia",
new { controller = "Rate_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Subscribe_index",
"admin/dang-ki",
new { controller = "Subscribe_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
"admin_Feedback_index",
"admin/phan-hoi",
new { controller = "Feedback_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
 namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
 "admin_Contact_index",
 "admin/lien-he",
 new { controller = "Contact_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
  namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
  "admin_Introduce_index",
  "admin/gioi-thieu",
  new { controller = "Introduce_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
   namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
   "admin_News_index",
   "admin/tin-tuc",
   new { controller = "News_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
    namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
    "admin_Newstype_index",
    "admin/loai-tin",
    new { controller = "Newstype_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
     namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
);
            context.MapRoute(
       "admin_account_index",
       "admin/tai-khoan",
       new { controller = "Accounts_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
        namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
   );
            context.MapRoute(
         "admin_role_index",
         "admin/quyen",
         new { controller = "Role_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
          namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
     );
            context.MapRoute(
            "admin_groupproduct_index",
            "admin/nhom-non",
            new { controller = "GroupProduct_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
             namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
        );
            context.MapRoute(
             "admin_production_index",
             "admin/hang-san-xuat",
             new { controller = "Production_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
              namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
         );
            context.MapRoute(
              "admin_category_index",
              "admin/loai-non",
              new { controller = "Category_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
               namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
          );
            context.MapRoute(
               "admin_products_index",
               "admin/non",
               new { controller = "products_admin", action = "Index", id = UrlParameter.Optional, Area = "Admin" },
               namespaces: new string[] { "NONBAOHIEMVIETTIN.Areas.admin.Controllers" }
           );
            context.MapRoute(
               "admin_login",
               "admin/dang-nhap",
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