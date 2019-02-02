using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.Models
{
    public class TimeOffAllowed
    {
        public long Id { get; set; }

        public long EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public long Year { get; set; }
        public long UTO { get; set; }
        public long PTO { get; set; }
        
    }
}
