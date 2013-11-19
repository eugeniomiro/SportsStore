using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,   // Matches /
                            "",
                            new {
                                controller = "Product",
                                action = "List",
                                category = (String) null,
                                page = 1
                            });

            routes.MapRoute(null,
                "Page{page}", // URL with parameters, matches /Page2, Page123, but not /PageABC
                new { controller = "Product", action = "List", category = (String) null },
                new { page = @"\d+" } // constraints: page must be numerical
            );

            routes.MapRoute(null,
                    "Admin",
                    new { controller = "Admin", action = "Index" }
                );

            routes.MapRoute(null,
                "Admin/Page{page}",
                new { controller = "Admin", action = "Index" }
            );

            routes.MapRoute(null,
                "{category}", // matches Football or /AnythingWithNoSlash
                new { controller = "Product", action = "List", page = 1 }
            );

            routes.MapRoute(null,
                "{category}/Page{page}", // matches Football/Page876
                new { controller = "Product", action = "List" },
                new { page = @"\d+" } // constraints: page must be numerical
            );

            routes.MapRoute(null, "{controller}/{action}");
        }

    }
}