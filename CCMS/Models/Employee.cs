using CCMS.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.Models
{
    public class Employee
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        //[ForeignKey("Department")]
        public long DepartmentId { get; set; }
        public Department Department { get; set; }

        [Column(TypeName = "Date")]
        public DateTime HireDate { get; set; }
        
        //We need to link the employee to the account in some way, however for testing purposes we will disable this link
        //[ForeignKey("CCMSUser")]
        //public string UserId { get; set; }
        //public CCMSUser CCMSUser { get; set; }
    }
}
