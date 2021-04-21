using PiDevEsprit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

using System.Web.Mvc;

namespace PiDevEsprit.Controllers
{
    public class SatisfactionController : Controller
    {
        HttpClient httpClient;
        string baseAddress;
        public SatisfactionController()
        {
            baseAddress = "http://localhost:8900/Satisfaction/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var _AccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJtZWQuYWhlZEBlc3ByaXQudG4kMmEkMTAkWHJlSzdaZUkwZ2xMNFFsTFpKNm1lTzhxNTBUQzdwdFZidWk4OWhmZjZnUERiNWl2aTZkaS5bUk9MRV9BRE1JTl0iLCJpYXQiOjE2MTg4OTA5MzQsImV4cCI6MTYxMjcxNDM4M30.w3dYJUNbbmgXEPunmxc478bP2HAibQZRnz2vUujSddU";
            httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", _AccessToken));
        }
        // GET: Satisfaction
        public ActionResult GetAllSatisfactions()
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "retrieve-all-satisfactions").Result;
           var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Oops!!!" };
           //throw new HttpResponseException(msg);
            if (tokenResponse.IsSuccessStatusCode)
            {
                var satisfactions = tokenResponse.Content.ReadAsAsync<IEnumerable<Satisfaction>>().Result;

                return View(satisfactions);
            }
            else
            {
                var satisfactions = tokenResponse.Content.ReadAsAsync<IEnumerable<Satisfaction>>().Result;

                return View(satisfactions);
                //  return View(new List<Satisfaction>());
            }
        }

        // GET: Satisfaction/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Satisfaction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Satisfaction/Create
        [System.Web.Http.HttpPost]
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

        // GET: Satisfaction/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Satisfaction/Edit/5
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

        // GET: Satisfaction/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Satisfaction/Delete/5
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
