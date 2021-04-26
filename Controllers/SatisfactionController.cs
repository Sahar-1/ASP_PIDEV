using PiDevEsprit.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.IO;
using System.Text;
using System.Drawing;

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
           // var _AccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJtZWQuYWhlZEBlc3ByaXQudG4kMmEkMTAkWHJlSzdaZUkwZ2xMNFFsTFpKNm1lTzhxNTBUQzdwdFZidWk4OWhmZjZnUERiNWl2aTZkaS5bUk9MRV9BRE1JTl0iLCJpYXQiOjE2MTg4OTA5MzQsImV4cCI6MTYxMjcxNDM4M30.w3dYJUNbbmgXEPunmxc478bP2HAibQZRnz2vUujSddU";
          //  httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", _AccessToken));
        }
        // GET: Satisfaction
        public ActionResult GetAllSatisfactions()
        {
            //var tokenResponse = httpClient.GetAsync(baseAddress +"retrieve-all-satisfactions").Result;
            // var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Oops!!!" };
            //throw new HttpResponseException(msg);
            var tokenResponse = httpClient.GetAsync(baseAddress+"retrieve-all-satisfactions").GetAwaiter().GetResult();
            if (tokenResponse.IsSuccessStatusCode)
            {
                var Satisfactions = tokenResponse.Content.ReadAsAsync<IEnumerable<Satisfaction>>().Result;

                return View(Satisfactions);
            }
            else
            {
                var Satisfactions = tokenResponse.Content.ReadAsAsync<IEnumerable<Satisfaction>>().GetAwaiter().GetResult();

                return View(Satisfactions);
            }
        }

        // GET: Satisfaction/Details/5
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = httpClient.GetAsync("findSatisfactionById/"+id).Result;
            Satisfaction satisfaction;

            if (response.IsSuccessStatusCode)
            {
                satisfaction = response.Content.ReadAsAsync<Satisfaction>().Result;
            }
            else
            {
                satisfaction = null;

            }
            return View(satisfaction);
        }

        // GET: Satisfaction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Satisfaction/Create


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Satisfaction satisfaction)
        {
            Debug.WriteLine(satisfaction.name);

            try
            {

                var APIResponse = httpClient.PostAsJsonAsync<Satisfaction>(baseAddress+"add-satisfaction",satisfaction).GetAwaiter().GetResult();

                if (APIResponse.IsSuccessStatusCode)
                {

                    return RedirectToAction("Create");
                }
                else
                {
                    return RedirectToAction("Create");
                }

            }
            catch
            {
                return View();
            }

        }


        // GET: Satisfaction
        public ActionResult AffecterQuetionsSatisfactionUser(Satisfaction satisfaction,int id)
        {
            try
            {
                // TODO: Add update logic here

                // TODO: Add update logic here
                satisfaction.Id = id;

                // var APIresponse = httpClient.PutAsJsonAsync<Question_Satisfaction>(baseAddress+"Updatequestion/"+id, question_Satisfaction).GetAwaiter().GetResult();
                var APIresponse = httpClient.PutAsJsonAsync<Satisfaction>(baseAddress+"AffecterQuetions_Satisfaction_Satisfaction_User/"+id,satisfaction).ContinueWith(putTask => putTask.Result.EnsureSuccessStatusCode());

                return View();
            }
            catch
            {
                return View();
            }
        }

        // POST: Satisfaction/Edit/5
        [HttpPost]
        public ActionResult Edit(Satisfaction satisfaction, int id)
        {
            try
            {
                // TODO: Add update logic here

                // TODO: Add update logic here
               // satisfaction.Id = id;

                // var APIresponse = httpClient.PutAsJsonAsync<Question_Satisfaction>(baseAddress+"Updatequestion/"+id, question_Satisfaction).GetAwaiter().GetResult();
                var APIresponse = httpClient.PutAsJsonAsync<Satisfaction>(baseAddress+"modify-satisfaction/", satisfaction).GetAwaiter().GetResult();


                return RedirectToAction("GetAllSatisfactions");
            }
            catch
            {
                return View();
            }
        }

        // GET: Satisfaction/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = httpClient.GetAsync("remove-satisfaction/"+id).Result;
            Satisfaction satisfaction;

            if (response.IsSuccessStatusCode)
            {
                satisfaction = response.Content.ReadAsAsync<Satisfaction>().Result;
            }
            else
            {
                satisfaction = null;

            }
            return View(satisfaction);
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


        // GET: Question_Satisfaction
        public ActionResult getAllQuestionsbySatisfaction(int id)
        {
            var tokenResponse = httpClient.GetAsync(baseAddress +"getAllQuestionsbySatisfaction/"+id).GetAwaiter().GetResult();
            if (tokenResponse.IsSuccessStatusCode)
            {
                var Questions = tokenResponse.Content.ReadAsAsync<IEnumerable<Question_Satisfaction>>().Result;

                return View(Questions);
            }
            else
            {
                var Questions = tokenResponse.Content.ReadAsAsync<IEnumerable<Question_Satisfaction>>().GetAwaiter().GetResult();

                return View(Questions);
            }

        }
        public ActionResult AffecterAnswerQuestionSatisfaction(List<Review> answer_Satisfactions, int id)
        {
            try
            {
                // TODO: Add update logic here

                // TODO: Add update logic here
        

                // var APIresponse = httpClient.PutAsJsonAsync<Question_Satisfaction>(baseAddress+"Updatequestion/"+id, question_Satisfaction).GetAwaiter().GetResult();
var APIresponse = httpClient.PutAsJsonAsync<List<Review>>(baseAddress+"Affecter_Answer_Question_Satisfaction/"+id,answer_Satisfactions).GetAwaiter().GetResult();

                return View();
            }
            catch
            {
                return View();
            }
        }
      
        public JsonResult Statistique(string namesat, int id)
        {
            List<string> data= new List<string>();

            try
            {
                // TODO: Add update logic here

                // TODO: Add update logic here


                // var APIresponse = httpClient.PutAsJsonAsync<Question_Satisfaction>(baseAddress+"Updatequestion/"+id, question_Satisfaction).GetAwaiter().GetResult();
                var APIresponse = httpClient.PutAsJsonAsync<string>(baseAddress+"StatistiqueAnswer_QuetionSatisfactionbUSer/"+id+"/"+namesat,null).GetAwaiter().GetResult();
               data = APIresponse.Content.ReadAsAsync<List<string>>().Result;

                var chartData = new object[data.Count + 1];
                chartData[0] = new object[]{
                "Year",
                "Electronics",
                "Books & Media",
                "Home & Kitchen"
            };

                int j = 0;
                foreach (var i in data)
                {
                    j++;
                    chartData[j] = new object[] { i.ToString() };
                }


                return Json(chartData,JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
