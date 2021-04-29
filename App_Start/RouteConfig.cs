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
                defaults: new { controller = "Home", action = "SignIn", id = UrlParameter.Optional }
            );


            routes.MapRoute(
               name: "Default1",
               url: "{controller}/{action}/{id}/{id1}",
               defaults: new { controller = "Bill", action = "Create", id = UrlParameter.Optional, id1 = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "GetCommentBySubject",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "ForumComment", action = "listComments", id=UrlParameter.Optional }
           );
           


        }
    }
}
