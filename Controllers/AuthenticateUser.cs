using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace PiDevEsprit.Controllers
{
    public class AuthenticateUser : System.Web.Mvc.ActionFilterAttribute

    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // check  if sessions is present
            if (HttpContext.Current.Session["id"] == null)
            {
                filterContext.Result = new RedirectResult("~/Sign_In/SignIn");
                return;
            }
            base.OnActionExecuting(filterContext);

        }
    }
}
