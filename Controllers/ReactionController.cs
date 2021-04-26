﻿using PiDevEsprit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace PiDevEsprit.Controllers
{
    public class ReactionController : Controller
    {
        HttpClient httpClient;
        string baseAddress;
        public ReactionController()
        {
            baseAddress = "http://localhost:8900/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var _AccessToken = "$2a$10$XreK7ZeI0glL4QlLZJ6meO8q50TC7ptVbui89hff6gPDb5ivi6di.";
            // httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", _AccessToken));
        }
        // GET: Post
        public ActionResult Index()
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "retrieve-all-reactions").Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var com = tokenResponse.Content.ReadAsAsync<IEnumerable<Reaction>>().Result;
                return View(com);
            }
            else
            {
                return View(new List<Reaction>());
            }
        }

        // GET: Post/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        public ActionResult Create(long id, Reaction comm)
        {
            try
            {

                var APIResponse = httpClient.PostAsJsonAsync<Reaction>(baseAddress + "add-reaction/" + id, comm).GetAwaiter().GetResult();

                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Post/Edit/5
        [HttpPost]
        public ActionResult Edit(Reaction p, long id)
        {
            try
            {
                // TODO: Add update logic here

                // TODO: Add update logic here
                p.reactionId = id;

                var APIresponse = httpClient.PutAsJsonAsync<Reaction>(baseAddress + "modify-reaction/" + id, p).GetAwaiter().GetResult();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        // GET: Post/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: Post/Delete/5
        [HttpPost]
        public ActionResult Delete(long Reactionid)
        {
            var tokenResponse = httpClient.DeleteAsync(baseAddress + "remove-reaction/" + Reactionid).Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
