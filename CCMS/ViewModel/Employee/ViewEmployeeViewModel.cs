using CCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.ViewModel
{
    public class ViewEmployeeViewModel
    {
        public Employee Employee { get; set; }
        public ManagementUnit ManagementUnit { get; set; }
        public IList<TimeOffAllowed> TimeOffAlloweds { get; set; }
    }
}
