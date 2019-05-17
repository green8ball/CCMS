using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.ViewModel
{
    public class AddManagementUnitViewModel
    {
        [Required]
        [Display(Name="Management Unit Name")]
        public string Name { get; set; }
        [Display(Name="Description")]
        public string Description { get; set; }
    }
}
