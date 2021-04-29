using PiDevEsprit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
        [HttpGet]
        public ActionResult ListEvents()
        {
            IEnumerable<DBO_Events> events = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8900/");
                var result = client.GetAsync("Get-All-Event").Result;


                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    try
                    {
                        events = result.Content.ReadAsAsync<IEnumerable<DBO_Events>>().Result;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    //Error response received   
                    events = Enumerable.Empty<DBO_Events>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return PartialView(events);
        }
        [AuthenticateUser]
        [HttpGet]
        public async Task<ActionResult> GetAllEventsManagements()
        {
            IEnumerable<DBO_Events> events = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8900/");
                var result = await client.GetAsync("Get-All-Event");


                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    try
                    {
                        events = await result.Content.ReadAsAsync<IEnumerable<DBO_Events>>();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    //Error response received   
                    events = Enumerable.Empty<DBO_Events>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }


            return View(events);
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
        [AuthenticateUser]
        [HttpGet]
        public async Task<ActionResult> Get_EventDetails(long id)
        {
            DBO_Events _event = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8900/");
                var result = await  client.GetAsync("get_event_by_id/" + id);


                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    try
                    {
                        _event = await result.Content.ReadAsAsync<DBO_Events>();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }


                else
                {
                    //Error response received   
                    _event = null;
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
                return View(_event);
            }
        }
   
        
        [AuthenticateUser]
        [HttpGet]
        public async Task<ActionResult> DeleteEvent(long id)
        {
            using (var client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:8900/")
            })
            {
                HttpResponseMessage responseMessage = await client.DeleteAsync("delete_event/" + id);
                var responseJson = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("GetAllEventsManagements", "Home");
            }
        }

        [AuthenticateUser]
        [HttpGet]
        public ActionResult GetExpiredAfterLastLogin()
        {
            long id =  long.Parse(Session["id"].ToString());
            IEnumerable<DBO_Events> events = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:8900/");
                var result =  client.GetAsync("/events/getExpiredAfterLastLogin/"+id).Result;


                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    try
                    {
                        events =  result.Content.ReadAsAsync<IEnumerable<DBO_Events>>().Result;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    //Error response received   
                    events = Enumerable.Empty<DBO_Events>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }


            return PartialView(events);
        }
        [AuthenticateUser]
        [HttpGet]
        public async Task<ActionResult> AddEvent()
        {
            return View();
        }
        [AuthenticateUser]
        [HttpPost]
        public async Task<ActionResult> AddEvent(
            [System.Web.Http.FromBody] string Name,
            [System.Web.Http.FromBody] string Description,
            [System.Web.Http.FromBody] string Type,
            [System.Web.Http.FromBody] string StartDateOffset,
            [System.Web.Http.FromBody] string EndDateOffset)
        {

            return View();
        }
    }
}