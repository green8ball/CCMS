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
    public class ActivityCodeController
    {
        private readonly CCMSContext _context;
        private readonly UserManager<CCMSUser> _userManager;
        private readonly SignInManager<CCMSUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ActivityCodeController(
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
    }
}
