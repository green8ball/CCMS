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
    [AllowAnonymous]
    //[Authorize]
    public class EmployeeController : Controller
    {
        private readonly CCMSContext _context;
        private readonly UserManager<CCMSUser> _userManager;
        private readonly SignInManager<CCMSUser> _signInManager;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmailSender _emailSender;

        public EmployeeController(
            CCMSContext context,
            UserManager<CCMSUser> userManager,
            SignInManager<CCMSUser> signInManager,
            ILogger<EmployeeController> logger,
            IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;

            //if (_context.Employees.ToList().Count == 0)
            //{
            //    if (_context.Departments.ToList().Count() == 0)
            //    {
            //        _context.Departments.Add(new Department
            //        {
            //            Name = "Temp",
            //            Description = "Temp"
            //        });
            //        _context.SaveChanges();
            //    }

            //    Employee newEmployee = new Employee
            //    {
            //        FirstName = "Joshua",
            //        MiddleName = "Ryan",
            //        LastName = "Ortmann",
            //        HireDate = DateTime.Parse("2018-08-27"),
            //        Department = _context.Departments.Single(d => d.Name == "Temp")
            //    };

            //    _context.Employees.Add(newEmployee);
            //    _context.SaveChanges();

            //    CCMSUser newUser = new CCMSUser
            //    {
            //        UserName = newEmployee.Id.ToString(),
            //        Employee = newEmployee,
            //        Email = newEmployee.Id.ToString() + "@ccms.com"
            //    };

            //    IdentityResult result = await _userManager.CreateAsync(newUser);
            //    if (result.IsCompletedSuccessfully)
            //    { }
            //}
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
                string pw = "Temp1234";

                IdentityResult result = await _userManager.CreateAsync(newUser);
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