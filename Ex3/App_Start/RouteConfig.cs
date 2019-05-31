using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ex3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("displayOnce", "display/{ip}/{port}",
            new { controller = "First", action = "DisplayOnce" },
            new { ip = @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$" });

            routes.MapRoute("loadDisplay", "display/{fname}/{rate}",
                new { controller = "First", action = "LoadDisplay" });

            routes.MapRoute("autoDisplay", "display/{ip}/{port}/{rate}",
            new { controller = "First", action = "AutoDisplay" });

            routes.MapRoute("saveDisplay", "save/{ip}/{port}/{rate}/{duration}/{fname}",
            new { controller = "First", Action = "SaveDisplay" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "First", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
