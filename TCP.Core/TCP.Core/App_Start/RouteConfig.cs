using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TCP.Core
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "Giỏ hàng",
            url: "gio-hang",
            defaults: new { controller = "Home", action = "GioHang" }
        );
            routes.MapRoute(
        name: "Thank you",
        url: "dat-hang-thanh-cong",
        defaults: new { controller = "Home", action = "ThankYou" }
    );
            routes.MapRoute(
     name: "about",
     url: "gioi-thieu",
     defaults: new { controller = "Home", action = "About" }
 );
            routes.MapRoute(
          name: "Thanh toán",
          url: "thanh-toan",
          defaults: new { controller = "Home", action = "ThanhToan" }
      );
            routes.MapRoute(
               name: "Render Route",
               url: "{url}",
               defaults: new { controller = "Home", action = "Handle", url = "" }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
