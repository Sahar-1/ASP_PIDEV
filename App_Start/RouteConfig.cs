using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PiDevEsprit
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "ClientIndex", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "edit",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Sign_In", action = "EditUser", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "delete",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Sign_In", action = "DeleteUser", Id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "deleteEv",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "DeleteEvent", Id = UrlParameter.Optional }
          );
            routes.MapRoute(
            name: "GetEv",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Get_EventDetails", Id = UrlParameter.Optional }
        );

        }
    }
}
