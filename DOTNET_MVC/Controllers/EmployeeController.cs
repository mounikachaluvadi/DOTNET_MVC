using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOTNET_MVC.Models;

namespace DOTNET_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeContext db = new EmployeeContext();
        // GET: Employee
        public ActionResult Index()
        {
            return View(db.GetEmployeeDetails());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection frm)
        {
            Employee emp = new Employee();
            emp.Name = frm["Name"];
            emp.Salary = Convert.ToInt32(frm["Salary"]);
            emp.Email = frm["Email"];
            emp.Password = frm["Password"];
            emp.Confirm_pwd = frm["Confirm_pwd"];
            emp.Age = Convert.ToInt32(frm["Age"]);
            emp.Address = frm["Address"];
            

            int i=db.CreateEmployee(emp);
            if (i == 1)
            {
                ViewBag.Rowsinserted = "Data entered successfully";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Rowsinserted = "Data failed to insert";
                return View();
            }
            
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Employee obj = db.GetEmployeeDetailsById(Id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            int i = db.UpdateEmployee(emp);
            if (i == 1)
            {
                ViewBag.Rowsinserted = "Data entered successfully";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Rowsinserted = "Data failed to insert";
                return View();
            }

            
        }
        
        //[HttpPost]
        public ActionResult Delete(int Id)
        {
            int i=db.DeleteEmployee(Id);
            if (i == 1)
            {
                ViewBag.RowDeleted = "Row Deleted successfully";
               //return RedirectToAction("Index");
            }
            else
            {
                ViewBag.RowDeleted = "Failed";
                //return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}