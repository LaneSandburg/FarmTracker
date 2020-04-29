using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentationLayer.Controllers
{
    public class MachineController : Controller
    {
        private IMachineManager _machineManager = null;

        public MachineController() 
        {
            _machineManager = new MachineManager();
        }

        // GET: Machine
        [Authorize(Roles = "Admin,LandOwner,Manager,Employee,Mechanic")]
        public ActionResult Index()
        {
            ViewBag.Title = "Machine Manager";
            var machines = _machineManager.GetMachineListByActive();
            return View(machines);
        }

        // GET: Machine/Details/5
        [Authorize(Roles = "Admin,LandOwner,Manager,Employee,Mechanic")]
        public ActionResult Details(string id)
        {            
            var machine =_machineManager.GetMachineByID(id);
            ViewBag.Title = machine.MachineTypeID + " " + id + " Details:";
            return View(machine);
        }

        // GET: Machine/Create
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Create()
        {
            var types = _machineManager.RetreiveMachineTypes();
            var status = _machineManager.RetreiveMachineStatus();
            ViewBag.Types = types;
            ViewBag.Status = status;
            ViewBag.Title = "Create New Machine.";
            return View();
        }

        // POST: Machine/Create
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public ActionResult Create(Machine machine)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    try
                    {
                        _machineManager.AddMachine(machine);

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                return RedirectToAction("Details", "Machine", new { id = machine.MachineID });
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Machine/Edit/5
        [Authorize(Roles = "Admin,Manager,Mechanic")]
        public ActionResult Edit(string id)
        {
            var types = _machineManager.RetreiveMachineTypes();
            var status = _machineManager.RetreiveMachineStatus();
            var machine = _machineManager.GetMachineByID(id);
            ViewBag.Types = types;
            ViewBag.Status = status;
            ViewBag.Title = "Edit: " +machine.MachineTypeID+" " + id;
            return View(machine);
        }

        // POST: Machine/Edit/5
        [Authorize(Roles = "Admin,Manager,Mechanic")]
        [HttpPost]
        public ActionResult Edit(string id, Machine newMachine)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here
                    var oldMachine = _machineManager.GetMachineByID(id);
                    _machineManager.EditMachine(oldMachine, newMachine);
                    return RedirectToAction("Details", "Machine", new { id = newMachine.MachineID });
                }
                catch
                {
                    return View();
                }

            }
            return View();
        }

        // GET: Machine/Delete/5
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Delete(string id)
        {
            Machine machine = null;
            try
            {
                machine = _machineManager.GetMachineByID(id);
            }
            catch (Exception)
            {

                RedirectToAction("index");
            }
            ViewBag.Title = "Deactivate " + machine.MachineTypeID + " " + id; ;
            return View(machine);
        }

        // POST: Machine/Delete/5
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                _machineManager.SetMachineActiveState(false, id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
