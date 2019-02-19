using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Areas.Identity.Data;
using CCMS.Models;
using CCMS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CCMS.Controllers
{
    [Authorize]
    public class TimeOffController : Controller
    {
        private readonly CCMSContext _context;
        private readonly UserManager<CCMSUser> _userManager;
        private readonly SignInManager<CCMSUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TimeOffController(
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
            IList<TimeOffRequest> timeOffRequests = _context.TimeOffRequests.ToList();
            return View(timeOffRequests);
        }

        [Authorize(Roles = "WFM, Admin, Human Resources, Staff")]
        public IActionResult Add()
        {
            AddTimeOffRequestViewModel addTimeOffRequestViewModel = new AddTimeOffRequestViewModel { };
            
            return View(addTimeOffRequestViewModel);
        }

        [Authorize(Roles = "WFM, Admin, Human Resources, Staff")]
        [HttpPost]
        public IActionResult Add(AddTimeOffRequestViewModel addTimeOffRequestViewModel)
        {
            if (ModelState.IsValid)
            {
                Employee employeeUser = _context.Employees.Single(e => e.Id.ToString() == _userManager.GetUserName(User));

                TimeOffAllowed timeOffAllowed = _context.TimeOffAlloweds
                                                .Where(t => t.EmployeeId == employeeUser.Id)
                                                .Where(t => t.Year == addTimeOffRequestViewModel.Date.Date.Year)
                                                .Single();
                if (timeOffAllowed.UTO >= 8 || timeOffAllowed.PTO >= 8)
                {
                    Allotment allotment = _context.Allotments
                                                    .Where(a => a.DepartmentID == employeeUser.DepartmentId)
                                                    .Where(a => a.Date == addTimeOffRequestViewModel.Date.Date)
                                                    .Single();

                    int allotmentTimeOffTaken = _context.TimeOffRequests
                                                    .Where(t => t.Date == addTimeOffRequestViewModel.Date.Date)
                                                    .Where(t => t.Requester.DepartmentId == employeeUser.DepartmentId)
                                                    .Count() * 8;

                    int employeeTimeOffTaken = _context.TimeOffRequests
                                                    .Where(t => t.Date.Year == addTimeOffRequestViewModel.Date.Date.Year)
                                                    .Where(t => t.RequesterId == employeeUser.Id)
                                                    .Count() * 8;

                    if (allotmentTimeOffTaken + 8 <= allotment.Allowed && employeeTimeOffTaken + 8 <= timeOffAllowed.PTO)
                    {
                        TimeOffRequest newTimeOffRequest = new TimeOffRequest
                        {
                            Date = addTimeOffRequestViewModel.Date.Date,
                            Requester = employeeUser,
                            SubmissionTimeStamp = DateTime.Now,
                            Status = "Approved"
                        };

                        _context.TimeOffRequests.Add(newTimeOffRequest);
                        _context.SaveChanges();

                        return Redirect("/TimeOff/" + newTimeOffRequest.ToString());
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}