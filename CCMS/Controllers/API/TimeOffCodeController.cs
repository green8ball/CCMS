using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CCMS.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TimeOffCodeController : ControllerBase
    {
        private readonly CCMSContext _context;

        public TimeOffCodeController(CCMSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeOffCode>>> GetTimeOffCodes()
        {
            return await _context.TimeOffCodes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeOffCode>> GetTimeOffCode(long id)
        {
            var timeOffCode = await _context.TimeOffCodes.FindAsync(id);

            if (timeOffCode == null)
            {
                return NotFound();
            }

            return timeOffCode;
        }

        [HttpPost]
        public async Task<ActionResult<TimeOffCode>> PostTimeOffCode(TimeOffCode timeOffCode)
        {
            _context.TimeOffCodes.Add(timeOffCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTimeOffCode", new { id = timeOffCode.Id }, timeOffCode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimeOffCode(long id, TimeOffCode timeOffCode)
        {
            if (id != timeOffCode.Id)
            {
                return BadRequest();
            }

            _context.Entry(timeOffCode).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TimeOffCode>> DeleteTimeOffCode(long id)
        {
            var timeOffCode = await _context.TimeOffCodes.FindAsync(id);
            if (timeOffCode == null)
            {
                return NotFound();
            }

            _context.TimeOffCodes.Remove(timeOffCode);
            await _context.SaveChangesAsync();

            return timeOffCode;
        }
    }
}