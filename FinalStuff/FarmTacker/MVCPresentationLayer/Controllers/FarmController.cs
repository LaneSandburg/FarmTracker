using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentationLayer.Controllers
{
    public class FarmController : Controller
    {
        private FarmManager _farmManager = null;

        public FarmController()
        {
            _farmManager = new FarmManager();
        }

        // GET: Farm
        public ActionResult Index()
        {
            ViewBag.Title = "Farm Manager";
            var farms = _farmManager.GetFarmListByActive();
            return View(farms);
        }

        // GET: Farm/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.Title = "Specified Farm";
            var farm = _farmManager.GetFarmByFarmID(id);
            return View(farm);
        }

        // GET: Farm/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Farm/Create
        [HttpPost]
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

        // GET: Farm/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Farm/Edit/5
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

        // GET: Farm/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Farm/Delete/5
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
