using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Areas.Identity.Data;
using CCMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CCMS.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<CCMSUser> _rolesManager;
        private readonly CCMSContext _context;
        public IActionResult Index()
        {
            return View();
        }
    }
}