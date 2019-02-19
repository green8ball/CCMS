using CCMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.ViewModel
{
    public class AddTimeOffRequestViewModel
    {
        [Required(ErrorMessage ="A valid date must be entered")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
