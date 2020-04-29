using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentationLayer.Controllers
{
    public class CropController : Controller
    {
        private CropManager _cropManager = null;

        public CropController() 
        {
            _cropManager = new CropManager();
        }

        // GET: Crop
        [Authorize(Roles = "Admin,LandOwner,Manager,Employee")]
        public ActionResult Index()
        {
            ViewBag.Title = "Crop Manager";
            var crops = _cropManager.GetCropList();
            return View(crops);
        }

        // GET: Crop/Details/5
        [Authorize(Roles = "Admin,LandOwner,Manager,Employee")]
        public ActionResult Details(string id)
        {
            ViewBag.Title = "Crop: " + id + " Details";
            var crop = _cropManager.GetCropByID(id);
            return View(crop);
        }

        // GET: Crop/Create
        [Authorize(Roles = "Admin,LandOwner,Manager,Employee")]
        public ActionResult Create()
        {
            ViewBag.Title = "Create a Crop Item";
            return View();
        }

        // POST: Crop/Create
        [Authorize(Roles = "Admin,LandOwner,Manager,Employee")]
        [HttpPost]
        public ActionResult Create(Crop crop)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    try
                    {
                        _cropManager.AddCrop(crop);
                       
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
                return RedirectToAction("Details", "Crop", new { id = crop.CropID });
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Crop/Edit/5
        [Authorize(Roles = "Admin,LandOwner,Manager,Employee")]
        public ActionResult Edit(string id)
        {
            ViewBag.Title = "Edit: " + id;
            var crop = _cropManager.GetCropByID(id);
            return View(crop);
        }

        // POST: Crop/Edit/5
        [Authorize(Roles = "Admin,LandOwner,Manager,Employee")]
        [HttpPost]
        public ActionResult Edit(string id, Crop newCrop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var oldCrop = _cropManager.GetCropByID(id);
                        _cropManager.EditCrop(oldCrop, newCrop);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }

                return RedirectToAction("Details", "Crop", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Crop/Delete/5
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Delete(string id)
        {
            Crop crop = null;
            try
            {
                crop = _cropManager.GetCropByID(id);
            }
            catch (Exception)
            {

                RedirectToAction("index");
            }
            ViewBag.Title = "Deactivate " + id;
            return View(crop);
        }

        // POST: Crop/Delete/5
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                if (ModelState.IsValid)
                {
                    try
                    {
                        _cropManager.DeleteCrop(id);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
