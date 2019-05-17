using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.Models
{
    public class EmployeeSchedule
    {
        public long Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public long ActivityCodeId { get; set; }
        public ActivityCode ActivityCode { get; set; }

        public long EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
