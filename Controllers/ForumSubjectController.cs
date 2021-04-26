using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PiDevEsprit.Models;

namespace PiDevEsprit.Controllers
{
    public class ForumSubjectController : Controller
    {
        HttpClient httpClient;
        string baseAddress;
        public ForumSubjectController()
        {
            baseAddress = "http://localhost:8900/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //  var _AccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJvbnMuYWRtaW5AZXNwcml0LnRuJDJhJDEwJFhyZUs3WmVJMGdsTDRRbExaSjZtZU84cTUwVEM3cHRWYnVpODloZmY2Z1BEYjVpdmk2ZGkuW1JPTEVfQURNSU5dIiwiaWF0IjoxNjE4OTUxMDIwLCJleHAiOjE2MTI3MTQzODN9.Nbl6ROPVxFUfPOb1_stCto6I6Y3uZMW2L4xhHiWz0Zo";
            //   httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Basic {0}", _AccessToken));
        }





        // GET: ForumSubject
        public async Task<ActionResult> Index()
        {
            IEnumerable<ForumSubject> forumS = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8900/");
                var result = await client.GetAsync("getAllForumsSubject");


                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    try
                    {
                        forumS = await result.Content.ReadAsAsync<IList<ForumSubject>>();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    //Error response received   
                    forumS = Enumerable.Empty<ForumSubject>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return View(forumS);
        }


        // GET: ForumSubject/Details/4
        public ActionResult Details(long id)
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "retrieve-Subject/" + id).Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var subject = tokenResponse.Content.ReadAsAsync<ForumSubject>().Result;
                return View(subject);
            }
            else
            {
                return View(new ForumSubject());
            }

        }



        // GET: ForumSubject/Delete/4
        public ActionResult Delete(long id)
        {
            var tokenResponse = httpClient.DeleteAsync(baseAddress + "removeForumSubject/" + id).Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }





        // GET: ForumSubject/Edit/5
        public ActionResult Edit(int id) 
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "retrieve-Subject/" + id).Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var subject = tokenResponse.Content.ReadAsAsync<ForumSubject>().Result;
                return View(subject);
            }
            else
            {
                return View(new ForumSubject());
            }
        }

        // POST: ForumSubject/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ForumSubject subject)
        {
            try
            {
                var APIResponse = httpClient.PutAsJsonAsync<ForumSubject>(baseAddress + "update-forumSubject/" + id, subject).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}