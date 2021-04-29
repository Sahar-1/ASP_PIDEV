using Newtonsoft.Json;
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
    public class Question_SatisfactionController : Controller
    {


        HttpClient httpClient;
        string baseAddress;
        public Question_SatisfactionController()
        {
            baseAddress = "http://localhost:8900/question";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var _AccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJtZWQuYWhlZEBlc3ByaXQudG4kMmEkMTAkWHJlSzdaZUkwZ2xMNFFsTFpKNm1lTzhxNTBUQzdwdFZidWk4OWhmZjZnUERiNWl2aTZkaS5bUk9MRV9BRE1JTl0iLCJpYXQiOjE2MTg2MjAyOTgsImV4cCI6MTYxMjcxNDM4M30.geYwLgC7H47ALR2JqQrG8u5pYcK88QgxB3TYVgQhlcs";
            httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", _AccessToken));
        }
        // GET: Question_Satisfaction
        public async Task<ActionResult> Index()
        {
            List<Question_Satisfaction> questionInfo = new List<Question_Satisfaction>();
            string Baseurl = "http://localhost:8900/";


            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("question/getAllQuestion");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    questionInfo = JsonConvert.DeserializeObject<List<Question_Satisfaction>>(EmpResponse);
                }
                //returning the employee list to view  
                return View(questionInfo);
            }
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Question_Satisfaction question_Satisfaction)
        {
            /* try
             {

                 var APIResponse = httpClient.PostAsJsonAsync<Question_Satisfaction>(baseAddress+"/addQuestion",
                 question_Satisfaction).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                 return RedirectToAction("Index");
             }
             catch
             {
                 return View();
             }*/


            string Baseurl = "http://localhost:8900/question";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJtZWQuYWhlZEBlc3ByaXQudG4kMmEkMTAkWHJlSzdaZUkwZ2xMNFFsTFpKNm1lTzhxNTBUQzdwdFZidWk4OWhmZjZnUERiNWl2aTZkaS5bUk9MRV9BRE1JTl0iLCJpYXQiOjE2MTg2MjAyOTgsImV4cCI6MTYxMjcxNDM4M30.geYwLgC7H47ALR2JqQrG8u5pYcK88QgxB3TYVgQhlcs");
                var response = await client.PostAsJsonAsync("/addQuestion", question_Satisfaction);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(question_Satisfaction);


        }

        public ActionResult Edit(int id)
        {
            Question_Satisfaction epm = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://reqres.in/api/");
                //HTTP GET
                var responseTask = client.GetAsync("users/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Question_Satisfaction>();
                    readTask.Wait();

                    epm = readTask.Result;
                }
            }
            return View(epm);
        }


        //
        // POST: /Products/Edit/5

        [HttpPost]
        public ActionResult Edit(Question_Satisfaction epm)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://reqres.in/api/users");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Question_Satisfaction>("student", epm);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(epm);
        }


        /*
        // GET: Question_Satisfaction
        public ActionResult Index()
        {
            return View();
        }

        // GET: Question_Satisfaction/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Question_Satisfaction/Create
        public ActionResult Create()
        {
            return View();
        }

   
        // POST: Question_Satisfaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question_Satisfaction question)
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

        // GET: Question_Satisfaction/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Question_Satisfaction/Edit/5
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

        // GET: Question_Satisfaction/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Question_Satisfaction/Delete/5
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


*/
    }
}
