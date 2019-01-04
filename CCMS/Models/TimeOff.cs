using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.Models
{
    public class TimeOff
    {
        public long Id { get; set; }
        [ForeignKey("Employee")]
        public long EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("TimeOffCode")]
        public long TimeOffCodeId { get; set; }
        public TimeOffCode TimeOffCode { get; set; }

    }
}