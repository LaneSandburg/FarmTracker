using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentationLayer.Controllers
{
    public class AssignmentsController : Controller
    {
        AssignmentManager _assignmentsManager = null;
        UserManager _userManager = null;
        MachineManager _machineManager = null;
        FieldManager _fieldManager = null;

        public AssignmentsController() 
        {
            _userManager = new UserManager();
            _assignmentsManager = new AssignmentManager();
            _machineManager = new MachineManager();
            _fieldManager = new FieldManager();
        }
        // GET: Assignments
        [Authorize(Roles = "Admin,LandOwner,Manager,Employee")]
        public ActionResult Index()
        {
            ViewBag.Title = "Assignment Manager";
            var assignments = _assignmentsManager.GetAssignmentByCompleted();
            return View(assignments);
        }

        // GET: Assignments/Details/5
        [Authorize(Roles = "Admin,LandOwner,Manager,Employee")]
        public ActionResult Details(int id)
        {            
            var assignment = _assignmentsManager.GetAssignmentByID(id);
            ViewBag.Title = "Assignment: " + id;

            return View(assignment);
        }

        // GET: Assignments/Create
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Create()
        {
            ViewBag.Title = "Create a New Assignment";

            var usageTypes = _assignmentsManager.RetreiveUsageTypes();
            ViewBag.UsageTypes = usageTypes;

            var employees = _userManager.GetUserListByActive();
            List<int> empIDs = new List<int>();
            foreach (var item in employees)
            {
                empIDs.Add(item.UserID);
            }
            ViewBag.EmployeeIDs = empIDs;

            var fieldIDs = _fieldManager.RetreiveAllFields();
            ViewBag.FieldIDs = fieldIDs;

            var machines = _machineManager.GetMachineListByActive();
            List<string> machineIDs = new List<string>();
            foreach (var item in machines)
            {
                machineIDs.Add(item.MachineID);
            }
            ViewBag.MachineIDs = machineIDs;            
            
            return View();
        }

        // POST: Assignments/Create
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public ActionResult Create(Assignment assignment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _assignmentsManager.AddAssignment(assignment);
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

        // GET: Assignments/Edit/5
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit Assignment:" + id.ToString();
            var assignment = _assignmentsManager.GetAssignmentByID(id);

            var usageTypes = _assignmentsManager.RetreiveUsageTypes();
            ViewBag.UsageTypes = usageTypes;

            var employees = _userManager.GetUserListByActive();
            List<int> empIDs = new List<int>();
            foreach (var item in employees)
            {
                empIDs.Add(item.UserID);
            }
            ViewBag.EmployeeIDs = empIDs;

            var fieldIDs = _fieldManager.RetreiveAllFields();
            ViewBag.FieldIDs = fieldIDs;

            var machines = _machineManager.GetMachineListByActive();
            List<string> machineIDs = new List<string>();
            foreach (var item in machines)
            {
                machineIDs.Add(item.MachineID);
            }
            ViewBag.MachineIDs = machineIDs;

            return View(assignment);
        }

        // POST: Assignments/Edit/5
       [Authorize(Roles = "Admin,Manager")]
       [HttpPost]
        public ActionResult Edit(int id, Assignment newAssignment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var oldAssignment = _assignmentsManager.GetAssignmentByID(id);
                        _assignmentsManager.EditAssignment(oldAssignment,newAssignment);
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

        // GET: Assignments/Delete/5
        [Authorize(Roles = "Admin,Manager,Employee,Mechanic")]
        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Compelete Assignment: " + id.ToString();
            var assignment = _assignmentsManager.GetAssignmentByID(id);
            return View(assignment);
        }

        // POST: Assignments/Delete/5
        //marks record as compelte
        [Authorize(Roles = "Admin,Manager,Employee,Mechanic")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _assignmentsManager.SetAssignmentCompletionState(true, id);
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
