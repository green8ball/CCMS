using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.ViewModel
{
    public class AddDepartmentViewModel
    {
        [Required]
        [Display(Name="Department Name")]
        public string Name { get; set; }
        [Display(Name="Description")]
        public string Description { get; set; }
    }
}
