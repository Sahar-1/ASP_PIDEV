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
    public class ForumCommentController : Controller
    {
        HttpClient httpClient;
        string baseAddress;
        public ForumCommentController()
        {
            baseAddress = "http://localhost:8900/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //  var _AccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJvbnMuYWRtaW5AZXNwcml0LnRuJDJhJDEwJFhyZUs3WmVJMGdsTDRRbExaSjZtZU84cTUwVEM3cHRWYnVpODloZmY2Z1BEYjVpdmk2ZGkuW1JPTEVfQURNSU5dIiwiaWF0IjoxNjE4OTUxMDIwLCJleHAiOjE2MTI3MTQzODN9.Nbl6ROPVxFUfPOb1_stCto6I6Y3uZMW2L4xhHiWz0Zo";
            //   httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Basic {0}", _AccessToken));
        }



        // GET: ForumComment by subject
        public async Task<ActionResult> listComments(string id)
        {
            if(id == null)
            {
                return RedirectToAction("listSubjects", "ForumSubject");
            }

            ICollection<ForumComment> forumComments = null;
            ForumSubject forumSubject = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8900/");
                var commentsResult = await client.GetAsync("findCommentBySubject/" + id);
                var forumSubjectResult = await client.GetAsync("retrieve-Subject/" + id);

                //If success received   
                if (commentsResult.IsSuccessStatusCode)
                {
                    try
                    {
                        forumComments = await commentsResult.Content.ReadAsAsync<IList<ForumComment>>();
                        forumSubject = await forumSubjectResult.Content.ReadAsAsync<ForumSubject>();
                        forumSubject.forumComments = forumComments;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return View(forumSubject);


        }




        //public ActionResult Create()
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<ActionResult> Create(string commentText, long subjectId)
        {
            string Baseurl = "http://localhost:8900/";
            ForumComment comment = new ForumComment();
            comment.answer = commentText;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //    client.DefaultRequestHeaders.Add("Authorization", "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJtZWQuYWhlZEBlc3ByaXQudG4kMmEkMTAkWHJlSzdaZUkwZ2xMNFFsTFpKNm1lTzhxNTBUQzdwdFZidWk4OWhmZjZnUERiNWl2aTZkaS5bUk9MRV9BRE1JTl0iLCJpYXQiOjE2MTg2MjAyOTgsImV4cCI6MTYxMjcxNDM4M30.geYwLgC7H47ALR2JqQrG8u5pYcK88QgxB3TYVgQhlcs");
                var response = await client.PostAsJsonAsync("addForumCommentToSubject/" + subjectId, comment);
                return RedirectToAction("listComments", "ForumComment", new { id = subjectId });

            }
        }




    
        [HttpPost]
        public ActionResult Edit(int subjectId, int commentId, string commentAnswer)
        {
            try
            {
                ForumComment comment = new ForumComment();
                comment.id = commentId;
                comment.answer = commentAnswer;
                var APIResponse = httpClient.PutAsJsonAsync<ForumComment>(baseAddress + "update-forumComment", comment)
                    .ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return Json(new { Message = "SUCCESS", JsonRequestBehavior.AllowGet });
            }
            catch
            {
                return Json(new { Message = "ERROR", JsonRequestBehavior.AllowGet });
            }
        }

        // GET: ForumComment/Delete/4
        public ActionResult Delete(int idComment, int idForumSubject)
        {
            var tokenResponse = httpClient.DeleteAsync(baseAddress + "removeForumComment/" + idComment).Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("listComments", new { id = idForumSubject });
            }
            return RedirectToAction("listComments");
        }








    }
}