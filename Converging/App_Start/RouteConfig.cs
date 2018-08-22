using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Converging
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "ProductCategory",
                url: "Danh-muc-san-pham/{alias}/{id}",
                defaults: new { controller = "ProductCategory", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Converging.Controllers" }
            );

            routes.MapRoute(
                name: "Product",
                url: "San-pham/{alias}/{id}",
                defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Converging.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Converging.Controllers" }
            );


        }
    }
}
