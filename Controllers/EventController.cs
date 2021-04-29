using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using PiDevEsprit.Models;
using Type = PiDevEsprit.Models.Type;

namespace PiDevEsprit.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            return View();
        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [System.Web.Mvc.HttpPost]
        public ActionResult Create(
            [FromBody] string event_name,
            [FromBody] string event_description,
            [FromBody] string event_type,
            [FromBody] string event_start_date,
            [FromBody] string event_end_date,
            [FromBody] string event_capacity
        )
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Event/Edit/{id}
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Event/Edit/{id}
        [System.Web.Mvc.HttpPost]
        public ActionResult Edit([FromUri] int id, 
            [FromBody] string event_name,
            [FromBody] string event_description,
            [FromBody] string event_type,
            [FromBody] string event_start_date,
            [FromBody] string event_end_date,
            [FromBody] string event_capacity
        )
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

        // GET: Event/Delete/5
        public ActionResult Delete([FromUri] int id)
        {
            return View();
        }

        // POST: Event/Delete/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete([FromUri] int id, FormCollection collection)
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