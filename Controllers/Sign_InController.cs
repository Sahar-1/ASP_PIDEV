using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PiDevEsprit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PiDevEsprit.Controllers
{
    public class Sign_InController : Controller
    {


        protected HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:8900/")
        };
        ~Sign_InController()
        {
            client.Dispose();
        }
        private async Task<JsonResult> ForwardToApi(string endpoint, object payload, Func<IEnumerable<KeyValuePair<string, JToken>>, JsonResult> success, Func<string, JsonResult> failure)
        {
            var str = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(endpoint, str);
            var responseJson = await responseMessage.Content.ReadAsStringAsync();
            var json_response_fields = JObject.Parse(responseJson) as IEnumerable<KeyValuePair<string, JToken>>;
            if (responseMessage.IsSuccessStatusCode)
            {
                return success(json_response_fields);
            }
            else
            {
                var mes = json_response_fields.Where(x => x.Key == "message").Select(x => x.Value).First().ToString();
                return failure(mes);
            }
        }
        [HttpGet]
        public ActionResult SignIn()
        {
            if (Session["username"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        // POST: Sign_In
        [HttpPost]
        public async Task<ActionResult> SignIn([System.Web.Http.FromBody] string Email, [System.Web.Http.FromBody] string Password)
        {
            return await ForwardToApi(
                endpoint: "Sign-In",
                payload: new
                {
                    email = Email,
                    password = Password
                },
                success: (json_response_fields) =>
                {
                    json_response_fields.ForEach(x =>
                        Session[x.Key] = x.Value
                    );

                    var roles = json_response_fields
                                    .Where(x => x.Key == "roles")
                                    .Select(x => x.Value.ToObject<string[]>())
                                    .First()
                                    .Contains("ROLE_ADMIN");

                    if(!roles)
                    {
                        return Json(new { result = "Redirect", url = Url.Action("ClientIndex", "Home") });
                    }
                    return Json(new { result = "Redirect", url = Url.Action("Index", "Home") });
                },
                failure: (error_msg) =>
                {
                    return Json(new { result = "Error", message = error_msg });
                }
            );
        }

        // GET: Sign_Up
        [HttpGet]
        public ActionResult Resgiter()
        {
            return View();
        }
        // POST: Sign_Up
        [HttpPost]
        public async Task<ActionResult> Resgiter(
            [System.Web.Http.FromBody] string FirstName,
            [System.Web.Http.FromBody] string LastName,
            [System.Web.Http.FromBody] string Email,
            [System.Web.Http.FromBody] string Password,
            [System.Web.Http.FromBody] string Date)
        {

            return await ForwardToApi(
              endpoint: "Sign-Up",
              payload: new
              {
                  email = Email,
                  password = Password,
                  firstName = FirstName,
                  lastName = LastName,
                  date = Date
              },
              success: (json_response_fields) =>
              {
                  json_response_fields.ForEach(x =>
                      Session[x.Key] = x.Value
                  );
                  ViewBag.message = "Check your mail please to verify you account";
                  return Json(new { result = "Redirect", url = Url.Action("SignIn", "Sign_In") });

              },
              failure: (error_msg) =>
              {
                  return Json(new { result = "Error", message = error_msg });
              }
          );
        }

        public async Task<RedirectToRouteResult> Logout()
        {
            using (var client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:8900/")
            })
            {
                object o = new
                {

                };
                // just to parsing object in PostAsync
                var str = new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync("log-out", str);
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                Session.Abandon();
                Session.Clear();
                return RedirectToAction("SignIn", "Sign_In");
            }


        }
        [HttpGet]
        [AuthenticateUser]
        public async Task<ActionResult> Get_All_Users()
        {
            IEnumerable<DBO_User> users = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8900/");
                var result = await client.GetAsync("retrieve-all-users");


                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    try
                    {
                        users = await result.Content.ReadAsAsync<IList<DBO_User>>();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    //Error response received   
                    users = Enumerable.Empty<DBO_User>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return View(users);
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(DBO_User dBO_User)
        {

            using (var client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:8900/")
            })
            {

                object o = new
                {
                    email = dBO_User.Email,
                };
                // just to parsing object in PostAsync
                var str = new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync("forgot-password", str);
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                if (!ModelState.IsValid)
                {
                    return View(dBO_User);
                }
                return View(responseJson);
            }
        }
        [HttpGet]
        public async Task<ActionResult> ResetPassword(string token)
        {
            using (var client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:8900/")
            })
            {

                HttpResponseMessage responseMessage = await client.GetAsync("confirm-reset?token=" + token);
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                var jObject = JObject.Parse(responseJson);
                HttpStatusCode httpStatusCode = responseMessage.StatusCode;
                if (!responseMessage.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = responseJson;
                    return RedirectToAction("ErrorPage", "Sign_In");
                }

                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(DBO_User dBO_User)
        {

            using (var client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:8900/")
            })
            {

                object o = new
                {
                    email = dBO_User.Email,
                    password = dBO_User.Password,
                };

                var str = new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync("reset-password", str);
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                if (!ModelState.IsValid)
                {
                    return View(dBO_User);
                }
                return View(responseJson);
            }
        }
        [AuthenticateUser]
        public DBO_User GetUserDetails(long id)
        {
            DBO_User user = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8900/");
                var result = client.GetAsync("/retrieve-user/" + id).Result;


                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    try
                    {
                        user = result.Content.ReadAsAsync<DBO_User>().Result;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    //Error response received   
                    user = null;
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
                return user;
            }

        }
        [HttpGet]
        public ViewResult ErrorPage()
        {
            return View();
        }
        
        [AuthenticateUser]
        [HttpGet]
        public ActionResult EditUser(long id)
        {
            var user = GetUserDetails(id);
            return View(user);
        }
        
        [AuthenticateUser]
        [HttpPost]
        public async Task<ActionResult> EditUser(long id,
            [System.Web.Http.FromBody] string FirstName,
            [System.Web.Http.FromBody] string LastName,
            [System.Web.Http.FromBody] string Email,
            [System.Web.Http.FromBody] string Actif,
            [System.Web.Http.FromBody] string Date,
            [System.Web.Http.FromBody] string AccountNonLocked)
        {
            using (var client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:8900/")
            })
            {

                object o = new
                {
                    firstName = FirstName,
                    lastName = LastName,
                    email = Email,
                    accountNonLocked = AccountNonLocked,
                    date = Date,
                    actif = Actif,
                };

                var editingUser = new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PutAsync("updateUser/" + id, editingUser);
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                if (!ModelState.IsValid)
                {
                    ViewBag.ErrorMessage = "Error updating user , NB: All fields are required .";
                    return View(o);
                }
                return RedirectToAction("Get_All_Users", "Sign_In");
            }

        }
        [AuthenticateUser]
        [HttpGet]
        public async Task<ActionResult> DeleteUser(long id)
        {
            using (var client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:8900/")
            })
            {
                HttpResponseMessage responseMessage = await client.DeleteAsync("remove-user/" + id);
                var responseJson = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Get_All_Users", "Sign_In");
            }
        }
        [AuthenticateUser]
        [HttpGet]
        public async Task<ActionResult> MyProfile()
        {

            var userConnected = long.Parse(Session["id"].ToString()); //Session["id"] as long?;
            var user = GetUserDetails(userConnected);
            return View(user);
        }

        [AuthenticateUser]
        [HttpPost]
        public ActionResult MyProfile(long? id)
        {
            return View();
        }
    }
}
