using Newtonsoft.Json;
using PiDevEsprit.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            baseAddress = "http://localhost:8900/question/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           // var _AccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJtZWQuYWhlZEBlc3ByaXQudG4kMmEkMTAkWHJlSzdaZUkwZ2xMNFFsTFpKNm1lTzhxNTBUQzdwdFZidWk4OWhmZjZnUERiNWl2aTZkaS5bUk9MRV9BRE1JTl0iLCJpYXQiOjE2MTg2MjAyOTgsImV4cCI6MTYxMjcxNDM4M30.geYwLgC7H47ALR2JqQrG8u5pYcK88QgxB3TYVgQhlcs";
           // httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", _AccessToken));
        }
        // GET: Question_Satisfaction
        public ActionResult GetAllQuestions()
        {
            var tokenResponse = httpClient.GetAsync(baseAddress+"getAllQuestion").GetAwaiter().GetResult();
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
        public ActionResult getAllAnswerByQuestion(int id)
        {
            var tokenResponse = httpClient.GetAsync(baseAddress+"getAllAnswerByQuestion/"+id).GetAwaiter().GetResult();
            if (tokenResponse.IsSuccessStatusCode)
            {
                var Question_Satisfactions = tokenResponse.Content.ReadAsAsync<IEnumerable<Answer_Satisfaction>>().Result;

                return View(Question_Satisfactions);
            }
            else
            {
                var Question_Satisfactions = tokenResponse.Content.ReadAsAsync<IEnumerable<Answer_Satisfaction>>().GetAwaiter().GetResult();

                return View(Question_Satisfactions);
            }

        }
        public ActionResult Create()
        {
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question_Satisfaction question_Satisfaction)
        {
            Debug.WriteLine(question_Satisfaction.question_Sat);

            try
            {

                var APIResponse = httpClient.PostAsJsonAsync<Question_Satisfaction>(baseAddress + "addQuestion/", question_Satisfaction).GetAwaiter().GetResult();
                TempData["seccussmessage"] = "save seccuss";
                var message = APIResponse.ToString();
                if (APIResponse.IsSuccessStatusCode)
                {

                    return RedirectToAction("GetAllQuestions");
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


      

        [HttpPost]
        public ActionResult Edit(Question_Satisfaction question_Satisfaction, int id)
        {
            try
            {
                // TODO: Add update logic here

                // TODO: Add update logic here
                question_Satisfaction.id = id;

               // var APIresponse = httpClient.PutAsJsonAsync<Question_Satisfaction>(baseAddress+"Updatequestion/"+id, question_Satisfaction).GetAwaiter().GetResult();
                var APIresponse = httpClient.PutAsJsonAsync<Question_Satisfaction>(baseAddress + "Updatequestion/", question_Satisfaction).ContinueWith(putTask => putTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("GetAllQuestions");
            }
            catch
            {
                return View();
            }
        }

        // GET: Message/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = httpClient.GetAsync("deleteQuestionById/" + id).Result;
            Question_Satisfaction question_Satisfaction;

            if (response.IsSuccessStatusCode)
            {
                question_Satisfaction = response.Content.ReadAsAsync<Question_Satisfaction>().Result;
            }
            else
            {
                question_Satisfaction = null;

            }
            return View(question_Satisfaction);
        }

    }
}
