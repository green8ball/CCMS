using CCMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.ViewModel.SchedulePattern
{
    public class AddSchedulePatternViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name="Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public IList<SchedulePatternRow> Rows { get; set; }
    }
}
