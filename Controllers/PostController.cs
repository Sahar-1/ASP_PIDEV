using System;
using PiDevEsprit.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;

namespace PiDevEsprit.Controllers
{
    public class PostController : Controller
    {
        HttpClient httpClient;
        string baseAddress;
        public PostController()
        {
            baseAddress = "http://localhost:8900/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var _AccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJzYWhhci5naGFyYmlAZXNwcml0LnRuJDJhJDEwJFhyZUs3WmVJMGdsTDRRbExaSjZtZU84cTUwVEM3cHRWYnVpODloZmY2Z1BEYjVpdmk2ZGkuW1JPTEVfUEFSRU5UXSIsImlhdCI6MTYxODk2MTA4NSwiZXhwIjoxNjEyNzE0MzgzfQ.6fW5TVcYhNVY5V - X_avSejU - eiNwpkJjFN1oG1JXzw0";
            // httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", _AccessToken));
        }
        // GET: Post
        public ActionResult Index()
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "retrieve-all-posts").Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var posts = tokenResponse.Content.ReadAsAsync<IEnumerable<Post>>().Result;
                return View(posts);
            }
            else
            {
                return View(new List<Post>());
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
        public ActionResult Create(Post post)
        {
            try
            {

                var APIResponse = httpClient.PostAsJsonAsync<Post>(baseAddress + "add-post", post).GetAwaiter().GetResult();

                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Edit/5
        public ActionResult Edit(long id)
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "retrieve-post/" + id).Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var subject = tokenResponse.Content.ReadAsAsync<Post>().Result;
                return View(subject);
            }
            else
            {
                return View(new Post());
            }
        }

        // POST: Post/Edit/5
        [HttpPost]
        public ActionResult Edit(long id,Post p)
        {
            try
            {
                var APIResponse = httpClient.PutAsJsonAsync<Post>(baseAddress + "modify-post/" + id, p).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
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
        public ActionResult Delete(long id)
        {
            var tokenResponse = httpClient.DeleteAsync(baseAddress + "remove-post/" + id).Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}


