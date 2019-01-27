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
using Microsoft.AspNetCore.Identity;
using CCMS.Areas.Identity.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.ComponentModel.DataAnnotations;

namespace CCMS.Controllers
{
    //[AllowAnonymous]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly CCMSContext _context;
        private readonly UserManager<CCMSUser> _userManager;
        
        public EmployeeController(
            CCMSContext context,
            UserManager<CCMSUser> userManager)
        {
            _context = context;
            _userManager = userManager;

            //create default login
            if (_context.Employees.ToList().Count == 0)
            {
                if (_context.Departments.ToList().Count() == 0)
                {
                    _context.Departments.Add(new Department
                    {
                        Name = "Temp",
                        Description = "Temp"
                    });
                    _context.SaveChanges();
                }

                Employee newEmployee = new Employee
                {
                    FirstName = "Joshua",
                    MiddleName = "Ryan",
                    LastName = "Ortmann",
                    HireDate = DateTime.Parse("2018-08-27"),
                    Department = _context.Departments.Single(d => d.Name == "Temp")
                };

                _context.Employees.Add(newEmployee);
                _context.SaveChanges();

                CCMSUser newUser = new CCMSUser
                {
                    UserName = newEmployee.Id.ToString(),
                    Employee = newEmployee
                };

                Task<IdentityResult> result = _userManager.CreateAsync(newUser, "Temp1234!");
                if (result.IsCompletedSuccessfully)
                { }
            }
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
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeViewModel)
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

                var newUser = new CCMSUser
                {
                    UserName = newEmployee.Id.ToString(),
                    Employee = newEmployee
                };

                //[DataType(DataType.Password)]
                string pw = "Temp1234!";

                IdentityResult result = await _userManager.CreateAsync(newUser, pw);
                if (result.Succeeded)
                { }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return Redirect("/Employee");
            }

            return View(addEmployeeViewModel);
        }

        //public IActionResult Update()
    }
}