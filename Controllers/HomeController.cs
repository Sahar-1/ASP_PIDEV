using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PiDevEsprit.Controllers
{
    
    public class HomeController : Controller
    {

        /// Admin template 
        [AuthenticateUser]
        public ActionResult Index()
        {
            return View();
        }
        // Client template
        public ActionResult ClientIndex()
        {
            return View();
        }

        // Extra garbage views 
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        // Extra garbage views
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}