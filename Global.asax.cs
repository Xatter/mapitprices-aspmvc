using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MapItPrices
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Map",
                "Map/{itemid}",
                new { controller = "Map", action = "Index" });

            routes.MapRoute(
                "UpdatePrice",
                "API/UpdatePrice/{itemid}/{storeid}/{price}",
                new { controller = "API", action = "UpdatePrice" });

            routes.MapRoute(
                "GetStores",
                "API/GetStores/{lng}/{lat}",
                new { controller = "API", action = "GetStores" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}