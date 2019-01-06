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
    public class ScheduleActivityCodeController : ControllerBase
    {
        private readonly CCMSContext _context;

        public ScheduleActivityCodeController(CCMSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleActivityCode>>> GetScheduleActivityCodes()
        {
            return await _context.ScheduleActivityCodes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleActivityCode>> GetScheduleActivityCode(long id)
        {
            var ScheduleActivityCode = await _context.ScheduleActivityCodes.FindAsync(id);

            if (ScheduleActivityCode == null)
            {
                return NotFound();
            }

            return ScheduleActivityCode;
        }

        [HttpPost]
        public async Task<ActionResult<ScheduleActivityCode>> PostScheduleActivityCode(ScheduleActivityCode ScheduleActivityCode)
        {
            _context.ScheduleActivityCodes.Add(ScheduleActivityCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScheduleActivityCode", new { id = ScheduleActivityCode.Id }, ScheduleActivityCode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutScheduleActivityCode(long id, ScheduleActivityCode ScheduleActivityCode)
        {
            if (id != ScheduleActivityCode.Id)
            {
                return BadRequest();
            }

            _context.Entry(ScheduleActivityCode).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ScheduleActivityCode>> DeleteScheduleActivityCode(long id)
        {
            var ScheduleActivityCode = await _context.ScheduleActivityCodes.FindAsync(id);
            if (ScheduleActivityCode == null)
            {
                return NotFound();
            }

            _context.ScheduleActivityCodes.Remove(ScheduleActivityCode);
            await _context.SaveChangesAsync();

            return ScheduleActivityCode;
        }
    }
}