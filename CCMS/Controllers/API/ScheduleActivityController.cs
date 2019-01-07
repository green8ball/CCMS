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
    public class ScheduleActivityController : ControllerBase
    {
        private readonly CCMSContext _context;

        public ScheduleActivityController(CCMSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleActivity>>> GetScheduleActivitys()
        {
            return await _context.ScheduleActivities.ToListAsync();
        }

        [HttpGet("{EmployeeId}")]
        public async Task<ActionResult<IEnumerable<ScheduleActivity>>> GetScheduleActivity(long EmployeeID)
        {
            var ScheduleActivity = await _context.ScheduleActivities.Where(x => x.EmployeeID == EmployeeID).ToListAsync();

            if (ScheduleActivity == null)
            {
                return NotFound();
            }

            return ScheduleActivity;
        }

        [HttpPost]
        public async Task<ActionResult<ScheduleActivity>> PostScheduleActivity(ScheduleActivity ScheduleActivity)
        {
            _context.ScheduleActivities.Add(ScheduleActivity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScheduleActivity", new { EmployeeID = ScheduleActivity.EmployeeID }, ScheduleActivity);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutScheduleActivity(long id, ScheduleActivity ScheduleActivity)
        //{
        //    if (id != ScheduleActivity.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(ScheduleActivity).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult<ScheduleActivity>> DeleteScheduleActivity(long id)
        {
            var ScheduleActivity = await _context.ScheduleActivities.FindAsync(id);
            if (ScheduleActivity == null)
            {
                return NotFound();
            }

            _context.ScheduleActivities.Remove(ScheduleActivity);
            await _context.SaveChangesAsync();

            return ScheduleActivity;
        }
    }
}