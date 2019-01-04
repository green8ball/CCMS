using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CCMS.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeOffCodeController : ControllerBase
    {
        private readonly CCMSContext _context;

        public TimeOffCodeController(CCMSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeOffCode>>> GetTimeCodes()
        {
            return await _context.TimeOffCodes.ToListAsync();
        }
    }
}