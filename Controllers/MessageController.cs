using PiDevEsprit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PiDevEsprit.Controllers
{
    public class MessageController : Controller
    {

        // GET: Message
        //  [AllowAnonymous]
        public async Task<ActionResult> GetAllMessages()
        {
            IEnumerable<Message> messages = null;
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //  var _AccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJtb3VoYW1lZC5haGVkQGVzcHJpdC50biQyYSQxMCRYcmVLN1plSTBnbEw0UWxMWko2bWVPOHE1MFRDN3B0VmJ1aTg5aGZmNmdQRGI1aXZpNmRpLltST0xFX1BBUkVOVF0iLCJpYXQiOjE2MTg4MDA0NjcsImV4cCI6MTYxMjcxNDM4M30.1_N7WwSGD0PkcoccOwBQKZedXQLt9KlFhkrEWgPQ0j4";
                // client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}",_AccessToken));
                using (var response = await client.GetAsync("http://localhost:8900/messages/retrieve-all-Messages"))
                {
                    // try
                    //   {
                    //      string apiResponse = await response.Content.ReadAsStringAsync();
                    //     messages = JsonConvert.DeserializeObject<IEnumerable<Message>>(apiResponse);
                    //  }
                    //  catch (Exception e)
                    //  {
                    //     Console.WriteLine(e.Message);
                    //}

                    // string baseAddress ="";
                    var tokenResponse = await client.GetAsync("http://localhost:8900/messages/retrieve-all-Messages");
                    if (tokenResponse.IsSuccessStatusCode)
                    {
                        try
                        {
                            messages = await tokenResponse.Content.ReadAsAsync<IList<Message>>();

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }


                    else
                    {


                        // messages = Enumerable.Empty<Message>();
                        //Storing the response details recieved from web api   
                        // var EmpResponse = tokenResponse.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        //   messages = (IEnumerable<Message>)JsonConvert.DeserializeObject<List<Question_Satisfaction>>(EmpResponse);
                        /*    try
                            {
                                messages = await tokenResponse.Content.ReadAsAsync<List<Message>>();

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            }*/

                        messages = await tokenResponse.Content.ReadAsAsync<IList<Message>>();
                        messages = new List<Message>();
                    }
                }
                return View(messages);
            }

        }

        // GET: Message/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Message/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Message/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Message/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Message/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Message/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Message/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
