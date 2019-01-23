using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Models;
using CCMS.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CCMS.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly CCMSContext _context;

        public EmployeeController(CCMSContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Employee";
            List<Employee> employees = _context.Employees.Include(e => e.Department).ToList();
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

                if (addEmployeeViewModel.FirstName != null && 
                    addEmployeeViewModel.LastName != null &&
                    addEmployeeViewModel.HireDate != null)
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
            }

            return View(addEmployeeViewModel);
        }
    }
}