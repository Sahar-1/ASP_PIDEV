using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PiDevEsprit.Models;

namespace PiDevEsprit.Controllers
{
    public class BillController : Controller
    {

        HttpClient httpClient;
        string baseAddress;
        public BillController()
        {
            
            baseAddress = "http://localhost:8900/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //  var _AccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJvbnMuYWRtaW5AZXNwcml0LnRuJDJhJDEwJFhyZUs3WmVJMGdsTDRRbExaSjZtZU84cTUwVEM3cHRWYnVpODloZmY2Z1BEYjVpdmk2ZGkuW1JPTEVfQURNSU5dIiwiaWF0IjoxNjE4OTUxMDIwLCJleHAiOjE2MTI3MTQzODN9.Nbl6ROPVxFUfPOb1_stCto6I6Y3uZMW2L4xhHiWz0Zo";
            //   httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Basic {0}", _AccessToken));
        }



        //public ActionResult Create() 
        //{
        //    return View();
        //}

        //// POST: Bill/Create
        //[HttpPost]
        //public async Task<ActionResult> Create(Bill bill, String id , String id1) 
        //{
        //    Object billToAdd = new
        //    {
        //        title = bill.title,
        //        dateStart = bill.dateStart,
        //        dateDeadline = bill.dateDeadline,
        //        discription = bill.discription,
        //        total = bill.total,
        //        amount = bill.amount,
        //        discount = bill.discount,
        //    };
        //    var objectContent = new StringContent(JsonConvert.SerializeObject(billToAdd), Encoding.UTF8, "application/json");
        //    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("addBill/" + id + "/" + id1, objectContent);
        //    return RedirectToAction("getAllBills", "Bill");

        //}



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Bill bill, string id, string id1)
        {



            string Baseurl = "http://localhost:8900/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
          
                var response = await client.PostAsJsonAsync("addBill/" + id + "/" + id1, bill);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("listSubjects");
                }
            }
            return View(bill);


        }


        // GET: Bill/Details/4
        public ActionResult Details(long id)
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "retrieve-bill/" + id).Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var bill = tokenResponse.Content.ReadAsAsync<Bill>().Result;
                return View(bill);
            }
            else
            {
                return View(new Bill());
            }
        }



        public async Task<ActionResult> getAllBills() 
        {
            IEnumerable<Bill> bills = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8900/");
                try
                {
                    var result = await client.GetAsync("bill");
                    if (result.IsSuccessStatusCode)
                    {
                        try
                        {
                            bills = await result.Content.ReadAsAsync<IList<Bill>>();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    else
                    {
                        //Error response received   
                        bills = Enumerable.Empty<Bill>();
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
                catch (NullReferenceException e)
                {
                    // Do something with e, please.
                }

            }

            return View(bills);
        }






        // GET: bill/Edit/5
        public ActionResult Edit(int id) 
        {
            var tokenResponse = httpClient.GetAsync(baseAddress + "retrieve-bill/" + id).Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var bill = tokenResponse.Content.ReadAsAsync<Bill>().Result;
                return View(bill);
                
            }
            else
            {
                return View(new Bill());
            }
        }

        // POST: bill/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Bill bill) 
        {
            try
            {
                var APIResponse = httpClient.PutAsJsonAsync<Bill>(baseAddress + "updateBill/" + id, bill).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("getAllBills",Redirect(Request.Url.AbsoluteUri));
                





            }
            catch
            {
                return View();
            }
        }


        // GET: Bill/Delete/4
        public ActionResult Delete(int id) 
        {
            var tokenResponse = httpClient.DeleteAsync(baseAddress + "remove-Bill/" + id).Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("getAllBills");
            }
            return RedirectToAction("getAllBills");
        }

    }
}

