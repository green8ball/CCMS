using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.Models
{
    public enum DOW : byte
    {
        Mon = 0,
        Tue = 1 << 0,
        Wed = 1 << 1,
        Thu = 1 << 2,
        Fri = 1 << 3,
        Sat = 1 << 4,
        Sun = 1 << 5
    }
    public class SchedulePatternRow
    {
        public long Id { get; set; }
        public int WeekNumber { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public bool Mon { get;set; }
        public bool Tue { get; set; }
        public bool Wed { get; set; }
        public bool Thu { get; set; }
        public bool Fri { get; set; }
        public bool Sat { get; set; }
        public bool Sun { get; set; }
    }
}
