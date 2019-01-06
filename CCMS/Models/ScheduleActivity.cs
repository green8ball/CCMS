using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.Models
{
    public class ScheduleActivity
    {
        public long Id { get; set; }
        [ForeignKey("ScheduleActivityCode")]
        public long ScheduleActivityCodeID { get; set; }
        public ScheduleActivityCode ScheduleActivityCode { get; set; }
        public DateTime ActivityStart { get; set; }
        public DateTime ActivityEnd { get; set; }
    }
}
