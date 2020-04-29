using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentationLayer.Controllers
{
    public class FieldController : Controller
    {
        // GET: Field
        FieldManager _fieldManager = null;
        FarmManager _farmManager = null;
        CropManager _cropManager = null;

        public FieldController()
        {
            _fieldManager = new FieldManager();
            _farmManager = new FarmManager();
            _cropManager = new CropManager();
        }
        [Authorize(Roles = "Admin,LandOwner,Manager,Employee")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Field/Details/5
        [Authorize(Roles = "Admin,LandOwner,Manager,Employee")]
        public ActionResult Details(string id)
        {
            ViewBag.Title = "Field Details";
            var field = _fieldManager.RetrieveFieldByID(id);
            return View(field);
        }

        // GET: Field/Create
        [Authorize(Roles = "Admin,LandOwner,Manager,Employee")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Field/Create
        [Authorize(Roles = "Admin,LandOwner,Manager,Employee")]
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

        // GET: Field/Edit/id
        [Authorize(Roles = "Admin,LandOwner,Manager,Employee")]
        public ActionResult Edit(string id)
        {
            ViewBag.Title = "Edit Field: " + id;

            var field = _fieldManager.RetrieveFieldByID(id);

            var farms = _farmManager.GetFarmListByActive();
            List<string> farmids = new List<string>();
            foreach (var item in farms)
            {
                string farmID = item.FarmID;
                farmids.Add(farmID);
            }
            ViewBag.Farms = farmids;

            var crops = _cropManager.GetCropList();
            List<string> cropids = new List<string>();
            foreach (var item in crops)
            {
                string cropid = item.CropID;
                cropids.Add(cropid);
            }
            ViewBag.Crops = cropids;

            return View(field);
        }

        // POST: Field/Edit/5
        [Authorize(Roles = "Admin,LandOwner,Manager,Employee")]
        [HttpPost]
        public ActionResult Edit(string id, Field newField)
        {
            try
            {
                // TODO: Add update logic here
                _fieldManager.UpdateField(id, newField);
                return RedirectToAction("Details", "Field", new { id = newField.FarmFieldID });
            }
            catch
            {
                return View();
            }
        }

        // GET: Field/Delete/5
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Field/Delete/5
        [Authorize(Roles = "Admin,Manager")]
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
