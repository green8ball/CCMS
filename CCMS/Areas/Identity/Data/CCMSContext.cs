using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CCMS.Models
{
    public class CCMSContext : IdentityDbContext<CCMSUser>
    {
        public CCMSContext(DbContextOptions<CCMSContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<ScheduleActivity> ScheduleActivities { get; set; }
        public DbSet<ScheduleActivityCode> ScheduleActivityCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<ScheduleActivity>()
                .HasKey(e => new { e.EmployeeID, e.ScheduleActivityCodeID });
        }
    }
}
