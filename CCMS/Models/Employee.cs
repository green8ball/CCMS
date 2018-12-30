using CCMS.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.Models
{
    public class Employee
    {
        public long Id { get; set; }
        [ForeignKey("CCMSUser")]
        public string UserId { get; set; }
        public CCMSUser CCMSUser { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime HireDate { get; set; }
    }
}
