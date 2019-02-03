using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Models;
using CCMS.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CCMS.Areas.Identity.Data;
using CCMS.Utils;

namespace CCMS.Controllers
{

    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly CCMSContext _context;
        private readonly UserManager<CCMSUser> _userManager;
        private readonly SignInManager<CCMSUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public EmployeeController(
            CCMSContext context,
            UserManager<CCMSUser> userManager,
            SignInManager<CCMSUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "WFM, Admin, Human Resources")]
        public IActionResult Index()
        {
            //ViewBag.Title = "Employee";
            IList<Employee> employees = _context.Employees.Include(e => e.Department).ToList();
            return View(employees);
        }

        [Authorize(Roles = "WFM, Admin, Human Resources, Staff")]
        public async Task<IActionResult> View(long id)
        {
            Employee employeeUser = _context.Employees.Single(e => e.Id.ToString() == _userManager.GetUserName(User));
            if (User.IsInRole("Staff"))
            {
                if (employeeUser.Id != id)
                {
                    return NotFound();
                }
            }
            //Employee employeeUser = _context.Employees.Single(e => e.Id.ToString() == _userManager.GetUserName(User));
            Employee employee = await _context.Employees.AsNoTracking().SingleAsync(e => e.Id == id);
            Department department = await _context.Departments.AsNoTracking().SingleAsync(d => d.Id == employee.DepartmentId);
            IList<TimeOffAllowed> timeOffAlloweds = await _context.TimeOffAlloweds.AsNoTracking().Where(t => t.EmployeeId == id).ToListAsync();
            ViewEmployeeViewModel viewEmployeeViewModel = new ViewEmployeeViewModel
            {
                Employee = employee,
                Department = department,
                TimeOffAlloweds = timeOffAlloweds
            };
            return View(viewEmployeeViewModel);
        }

        [Authorize(Roles = "WFM, Admin, Human Resources")]
        public IActionResult Add()
        {
            AddEmployeeViewModel addEmployeeViewModel = new AddEmployeeViewModel(_context.Departments.ToList());
            return View(addEmployeeViewModel);
        }

        [Authorize(Roles = "WFM, Admin, Human Resources, Staff")]
        public async Task<IActionResult> TimeOffRequest()
        {

        }

        [Authorize(Roles = "WFM, Admin, Human Resources")]
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

                foreach (int year in Helper.EachYear(newEmployee.HireDate.Year, newEmployee.HireDate.Year + 2))
                {
                    TimeOffAllowed newTimeOffAllowed = new TimeOffAllowed
                    {
                        Employee = newEmployee,
                        Year = year,
                        UTO = 40,
                        PTO = 40
                    };
                    _context.TimeOffAlloweds.Add(newTimeOffAllowed);
                    _context.SaveChanges();
                }

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