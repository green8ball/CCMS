using CCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.ViewModel
{
    public class ViewDepartmentViewModel
    {
        public Department Department { get; set; }
        public IList<int> AllotmentYears { get; set; }
    }
}
