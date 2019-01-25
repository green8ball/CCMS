using System;
using System.Collections.Generic;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Models;
using CCMS.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CCMS.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly CCMSContext _context;

        public EmployeeController(CCMSContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //ViewBag.Title = "Employee";
            IList<Employee> employees = _context.Employees.Include(e => e.Department).ToList();
            return View(employees);
        }

        public IActionResult Add()
        {
            AddEmployeeViewModel addEmployeeViewModel = new AddEmployeeViewModel(_context.Departments.ToList());
            return View(addEmployeeViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddEmployeeViewModel addEmployeeViewModel)
        {
            if(ModelState.IsValid)
            {
                Department newEmployeeDepartment = _context.Departments.Single(d => d.Id == addEmployeeViewModel.DepartmentId);
                Employee newEmployee = new Employee
                {
                    FirstName = addEmployeeViewModel.FirstName,
                    MiddleName = addEmployeeViewModel.MiddleName,
                    LastName = addEmployeeViewModel.LastName,
                    HireDate = addEmployeeViewModel.HireDate,
                    Department = newEmployeeDepartment
                };

                _context.Employees.Add(newEmployee);
                _context.SaveChanges();

                return Redirect("/Employee");
            }

            return View(addEmployeeViewModel);
        }

        //public IActionResult Update()
    }
}