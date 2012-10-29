using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SportsStore.WebUI.Infrastructure;
using SportsStore.WebUI.Binders;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,   // Matches /
                            "",
                            new { controller = "Product", action = "List",
                                  category = (String) null, page = 1
                            });

            routes.MapRoute(null,
                "Page{page}", // URL with parameters, matches /Page2, Page123, but not /PageABC
                new { controller = "Product", action = "List", category = (String) null },
                new { page = @"\d+" } // constraints: page must be numerical
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

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}