using CCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.ViewModel
{
    public class ViewManagementUnitViewModel
    {
        public ManagementUnit ManagementUnit { get; set; }
        public IList<int> AllotmentYears { get; set; }
    }
}
