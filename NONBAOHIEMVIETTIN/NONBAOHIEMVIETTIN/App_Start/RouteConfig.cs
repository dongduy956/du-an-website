using NONBAOHIEMVIETTIN.Models;
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
            routes.MapMvcAttributeRoutes();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");       
            routes.MapRoute(
               name: "Login",
               url: "dang-nhap",
               defaults: new { controller = "Accounts", action = "Login", id = UrlParameter.Optional },
               namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
           );
            routes.MapRoute(
        name: "Accounts_LoginFacebook",
        url: "dang-nhap-facebook",
        defaults: new { controller = "Accounts", action = "LoginFacebook", id = UrlParameter.Optional },
        namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
    );
            routes.MapRoute(
        name: "Cart_UpdateItem",
        url: "cap-nhat-so-luong-gio-hang",
        defaults: new { controller = "Cart", action = "UpdateItem", id = UrlParameter.Optional },
        namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
    );
            routes.MapRoute(
         name: "Products_ListName",
         url: "danh-sach-goi-y",
         defaults: new { controller = "Products", action = "ListName", id = UrlParameter.Optional },
         namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
     );
            routes.MapRoute(
         name: "Pay",
         url: "thanh-toan",
         defaults: new { controller = "Cart", action = "Pay", id = UrlParameter.Optional },
         namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
     );
            routes.MapRoute(
          name: "LoginGoogle",
          url: "dang-nhap-google",
          defaults: new { controller = "Accounts", action = "LoginGoogle", id = UrlParameter.Optional },
          namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
      );
            routes.MapRoute(
           name: "Register",
           url: "dang-ki",
           defaults: new { controller = "Accounts", action = "Register", id = UrlParameter.Optional },
           namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
       );
            routes.MapRoute(
         name: "Update",
         url: "cap-nhat",
         defaults: new { controller = "Accounts", action = "Update", id = UrlParameter.Optional },
         namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
     );
            routes.MapRoute(
        name: "subscribe_news",
        url: "dang-ki-nhan-tin",
        defaults: new { controller = "subscribe", action = "news", id = UrlParameter.Optional },
        namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
    );


            routes.MapRoute(
             name: "ChangePassword",
             url: "doi-mat-khau",
             defaults: new { controller = "Accounts", action = "ChangePassword", id = UrlParameter.Optional },
             namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
         );
            routes.MapRoute(
             name: "ChangePasswordEmail",
             url: "doi-mat-khau-email",
             defaults: new { controller = "Accounts", action = "ChangePasswordEmail", id = UrlParameter.Optional },
             namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
         );
            routes.MapRoute(
            name: "SendAgain",
            url: "gui-lai-ma",
            defaults: new { controller = "Accounts", action = "SendAgain", id = UrlParameter.Optional },
            namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
        );
            routes.MapRoute(
            name: "ForgetPass",
            url: "quen-mat-khau",
            defaults: new { controller = "Accounts", action = "ForgetPass", id = UrlParameter.Optional },
            namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
        );
            routes.MapRoute(
             name: "CodeForget",
             url: "ma-quen-mat-khau",
             defaults: new { controller = "Accounts", action = "CodeForget", id = UrlParameter.Optional },
             namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
         );
            routes.MapRoute(
     name: "Wish_AddItem",
     url: "them-vao-yeu-thich",
     defaults: new { controller = "Wish", action = "AddItem", id = UrlParameter.Optional },
     namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
 );

            routes.MapRoute(
      name: "Cart_DeleteItem",
      url: "xoa-gio-hang",
      defaults: new { controller = "Cart", action = "DeleteItem", id = UrlParameter.Optional },
      namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
  );
            routes.MapRoute(
        name: "Wish_DeleteItem",
        url: "xoa-yeu-thich",
        defaults: new { controller = "Wish", action = "DeleteItem", id = UrlParameter.Optional },
        namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
    );
            routes.MapRoute(
        name: "Cart_AddItem",
        url: "them-vao-gio-hang",
        defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
        namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
    );
            routes.MapRoute(
           name: "Products_Ratting",
           url: "danh-gia-san-pham",
           defaults: new { controller = "Products", action = "Ratting", id = UrlParameter.Optional },
           namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
       );
            routes.MapRoute(
           name: "Contact_feedback",
           url: "phan-hoi",
           defaults: new { controller = "Contact", action = "feedback", id = UrlParameter.Optional },
           namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
       );
            routes.MapRoute(
              name: "CodeRegister",
              url: "ma-dang-ki",
              defaults: new { controller = "Accounts", action = "CodeRegister", id = UrlParameter.Optional },
              namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
          );
            routes.MapRoute(
               name: "UploadImage",
               url: "tai-anh",
               defaults: new { controller = "Accounts", action = "UploadImg", id = UrlParameter.Optional },
               namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
           );
            routes.MapRoute(
               name: "Logout",
               url: "dang-xuat",
               defaults: new { controller = "Accounts", action = "Logout", id = UrlParameter.Optional },
               namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
           );
          
            routes.MapRoute(
             name: "NotFound",
             url: "loi-404",
             defaults: new { controller = "Error", action = "NotFound", id = UrlParameter.Optional },
             namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
         );            
            routes.MapRoute(
              name: "ConfirmOrder",
              url: "xac-nhan-don-hang",
              defaults: new { controller = "Cart", action = "ConfirmOrder", id = UrlParameter.Optional },
              namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
          );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "AccountInfo",
               url: "thong-tin-tai-khoan",
               defaults: new { controller = "Accounts", action = "AccountInfo", id = UrlParameter.Optional },
               namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
           );
            routes.MapRoute(
         name: "Introduce",
         url: "gioi-thieu",
         defaults: new { controller = "Introduce", action = "Index", id = UrlParameter.Optional },
         namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
     );
            routes.MapRoute(
           name: "Contact",
           url: "lien-he",
           defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
           namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
       );
            routes.MapRoute(
            name: "Wish",
            url: "yeu-thich",
            defaults: new { controller = "Wish", action = "Index", id = UrlParameter.Optional },
            namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
        );

            routes.MapRoute(
              name: "Cart",
              url: "gio-hang",
              defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
              namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
          );
            routes.MapRoute(
name: "Search",
url: "tim-kiem",
defaults: new { controller = "Products", action = "Search", id = UrlParameter.Optional },
namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
);
            routes.MapRoute(
           name: "NewsDetail",
           url: "tin-tuc/chi-tiet/{alias}",
           defaults: new { controller = "News", action = "NewsDetail", id = UrlParameter.Optional },
           namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
       );
            routes.MapRoute(
            name: "ProductsDetail",
            url: "chi-tiet/{alias}",
            defaults: new { controller = "Products", action = "ProductDetail", id = UrlParameter.Optional },
            namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
        );
            routes.MapRoute(
         name: "NewsIndex",
         url: "tin-tuc/{alias}",
         defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional },
         namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
     );
            routes.MapRoute(
           name: "ProductsGroup",
           url: "{alias}/{alia}",
           defaults: new { controller = "Products", action = "GroupProducts", id = UrlParameter.Optional },
           namespaces: new string[] { "NONBAOHIEMVIETTIN.Controller" }
       );

            routes.MapRoute(
               name: "ProductsIndex",
               url: "{alias}",
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
