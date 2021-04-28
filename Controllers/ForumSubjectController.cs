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
using PiDevEsprit.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



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
        public async Task<ActionResult> listSubjects()
        {
            IEnumerable<ForumSubject> ForumSubjects = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8900/");
                try
                {
                    var result = await client.GetAsync("getAllForumsSubject");
                    if (result.IsSuccessStatusCode)
                    {
                        try
                        {
                            ForumSubjects = await result.Content.ReadAsAsync<IList<ForumSubject>>();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    else
                    {
                        //Error response received   
                        ForumSubjects = Enumerable.Empty<ForumSubject>();
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
                catch (NullReferenceException e)
                {
                    // Do something with e, please.
                }

            }

            return View(ForumSubjects);
        }


        /*
        // POST: ForumSubject/Create
        [HttpPost]
        public async Task<ActionResult> Create(ForumSubject subject, long id1 = 2)
        {
            Object ForumSubjectToAdd = new
            {
                titre = subject.title,
                dateStart = subject.postedDate,
                ques = subject.question,
                stat = subject.status,
                vote=subject.voteCount,
                

            };
            var objectContent = new StringContent(JsonConvert.SerializeObject(ForumSubjectToAdd), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("addForumSubject/" + id1 , objectContent);
            return RedirectToAction("Index", "ForumSubject");

        }

        */


        // GET: Categories/Create


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ForumSubject subject,int id=2)
        {
           


            string Baseurl = "http://localhost:8900/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //    client.DefaultRequestHeaders.Add("Authorization", "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJtZWQuYWhlZEBlc3ByaXQudG4kMmEkMTAkWHJlSzdaZUkwZ2xMNFFsTFpKNm1lTzhxNTBUQzdwdFZidWk4OWhmZjZnUERiNWl2aTZkaS5bUk9MRV9BRE1JTl0iLCJpYXQiOjE2MTg2MjAyOTgsImV4cCI6MTYxMjcxNDM4M30.geYwLgC7H47ALR2JqQrG8u5pYcK88QgxB3TYVgQhlcs");
               //  subject.postedDate = new DateTime();

               

                var response = await client.PostAsJsonAsync("addForumSubject/" + id, subject);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("listSubjects");
                }
            }
            return View(subject);


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



        //// GET: ForumSubject/Delete/4
        //public ActionResult Delete(long id)
        //{
        //    var tokenResponse = httpClient.DeleteAsync(baseAddress + "removeForumSubject/" + id).Result;
        //    if (tokenResponse.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("listSubjects");
        //    }
        //    return RedirectToAction("listSubjects");
        //}





        // GET: ForumSubject/Edit/5
/*        public ActionResult Edit(int id) 
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
        }*/

        // POST: ForumSubject/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ForumSubject subject)
        {
            try
            {
                var APIResponse = httpClient.PutAsJsonAsync<ForumSubject>(baseAddress + "update-forumSubject/" + id, subject).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("listSubjects");
            }
            catch
            {
                return View();
            }
        }


    }
}