using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.Models
{
    public class ScheduleActivityCode
    {
        //public enum ScheduleTrade { KEEP_WITH_AGENT, DELETE, TRADE_WITH_SCHEDULE, NO_TRADES_ALLOWED }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Priority { get; set; }
        public bool Open { get; set; }
        public bool OT { get; set; }
        public bool WorkHours { get; set; }
        //public ScheduleTrade TradeSchedule { get; set; }
    }
}
