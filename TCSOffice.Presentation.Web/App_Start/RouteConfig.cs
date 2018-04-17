using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TCSOffice.Presentation.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //convention-based routing
            routes.MapRoute(
                name: "Activate",
                url: "Account/ActivateCompanyFromEmail/{userId}/{companyId}",
                defaults: new { controller = "Account", action = "ActivateCompanyFromEmail", userId = UrlParameter.Optional, companyId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
