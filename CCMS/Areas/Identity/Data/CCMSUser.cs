using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CCMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace CCMS.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the CCMSUser class
    public class CCMSUser : IdentityUser
    {
        public long EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
