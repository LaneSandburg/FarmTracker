using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;

namespace MVCPresentationLayer.Controllers
{
    public class FarmController : Controller
    {
        private FarmManager _farmManager = null;
        private UserManager _userManager = null;
        private FieldManager _fieldManager = null;

        public FarmController()
        {
            _fieldManager = new FieldManager();
            _userManager = new UserManager();
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
            ViewBag.Title = "Farm: " + id + " Details";
            var farm = _farmManager.GetFarmByFarmID(id);
            var fields = _farmManager.RetreiveFarmFields(id);

            ViewBag.Fields = fields;
            return View(farm);
        }

        // GET: Farm/Create
        public ActionResult Create()
        {
            var Owners = _userManager.RetreiveUserByRole("LandOwner");
            var OwnerIDs = new List<int>();
            foreach (var item in Owners)
            {
                OwnerIDs.Add(item.UserID);
            }
            ViewBag.OwnerList = OwnerIDs;
            ViewBag.Title = "Create A Farm";
            return View();
        }

        // POST: Farm/Create
        [HttpPost]
        public ActionResult Create(Farm farm)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    try
                    {
                        _farmManager.AddFarm(farm);

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                return RedirectToAction("Details", "Farm", new { id = farm.FarmID });
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Farm/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.Title = "Edit Farm: " + id;
            var farm = _farmManager.GetFarmByFarmID(id);
            var fields = _farmManager.RetreiveFarmFields(id);
            var Owners = _userManager.RetreiveUserByRole("LandOwner");
            var OwnerIDs = new List<int>();
            foreach (var item in Owners)
            {
                OwnerIDs.Add(item.UserID);
            }
            ViewBag.OwnerList = OwnerIDs;


            ViewBag.Fields = fields;
            return View(farm);
        }

        // POST: Farm/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Farm newFarm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here
                    _farmManager.EditFarm(id, newFarm);
                    return RedirectToAction("Details", "Farm", new { id = newFarm.FarmID });
                }
                catch
                {
                    return View();
                }

            }
            return View();

        }

        // GET: Farm/Delete/5
        public ActionResult Delete(string id)
        {
            Farm farm = null;
            try
            {
                farm = _farmManager.GetFarmByFarmID(id);
            }
            catch (Exception)
            {

                RedirectToAction("index");
            }
            ViewBag.Title = "Deactivate " + id;
            return View(farm);
        }

        // POST: Farm/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                _farmManager.SetFarmActiveState(false, id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
