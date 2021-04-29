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
        HttpClient httpClient;
        string baseAddress;
        public MessageController()
        {
            baseAddress = "http://localhost:8900/messages/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           // var _AccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJtZWQuYWhlZEBlc3ByaXQudG4kMmEkMTAkWHJlSzdaZUkwZ2xMNFFsTFpKNm1lTzhxNTBUQzdwdFZidWk4OWhmZjZnUERiNWl2aTZkaS5bUk9MRV9BRE1JTl0iLCJpYXQiOjE2MTg4OTA5MzQsImV4cCI6MTYxMjcxNDM4M30.w3dYJUNbbmgXEPunmxc478bP2HAibQZRnz2vUujSddU";
           // httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", _AccessToken));
        }

        // GET: Message
        //  [AllowAnonymous]
        public ActionResult GetAllMessages()
        {
         
                var tokenResponse = httpClient.GetAsync(baseAddress + "retrieve-all-Messages").Result;
                if (tokenResponse.IsSuccessStatusCode)
                {
                    var satisfactions = tokenResponse.Content.ReadAsAsync<IEnumerable<Message>>().Result;

                    return View(satisfactions);
                }
                else
                {
                    var satisfactions = tokenResponse.Content.ReadAsAsync<IEnumerable<Message>>().Result;

                    return View(satisfactions);
                    //  return View(new List<Satisfaction>());
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
