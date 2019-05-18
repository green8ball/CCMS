using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Areas.Identity.Data;
using CCMS.Models;
using CCMS.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CCMS.Controllers
{
    [Authorize]
    public class SchedulePatternController : Controller
    {
        private readonly CCMSContext _context;
        private readonly UserManager<CCMSUser> _userManager;
        private readonly SignInManager<CCMSUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SchedulePatternController(
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
            return View();
        }
    }
}