using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.Models
{
    public class DepartmentAllotment
    {
        public long DepartmentID { get; set; }
        public Department Department { get; set; }

        public long AllotmentID { get; set; }
        public Allotment Allotment { get; set; }
    }
}
