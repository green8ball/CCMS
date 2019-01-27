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

    [Authorize(Roles = "WFM, Admin, Human Resources")]
    public class EmployeeController : Controller
    {
        private readonly CCMSContext _context;
        private readonly UserManager<CCMSUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public EmployeeController(
            CCMSContext context,
            UserManager<CCMSUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
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
                {
                    await _userManager.AddToRoleAsync(newUser, "Staff");
                }

                return Redirect("/Employee");
            }

            return View(addEmployeeViewModel);
        }

        //public IActionResult Update()
    }
}