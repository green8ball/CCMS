using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Areas.Identity.Data;
using CCMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CCMS.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly CCMSContext _context;

        public AccountController(CCMSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CCMSUser>>> Users()
        {
            return await _context.Users.ToListAsync();
        }
    }
}