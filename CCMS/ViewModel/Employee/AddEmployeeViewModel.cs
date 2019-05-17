using CCMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.ViewModel
{
    public class AddEmployeeViewModel
    {
        [Required(ErrorMessage ="First name is required")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Display(Name ="Middle Name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name ="ManagementUnit")]
        public long ManagementUnitId { get; set; }

        [Required(ErrorMessage ="Employee must have a hire date")]
        [Display(Name ="Hire Date")]
        [DataType(DataType.Date)]
        //[Column(TypeName = "Date")]
        public DateTime HireDate { get; set; }

        public List<SelectListItem> ManagementUnits { get; set; }
        
        public AddEmployeeViewModel(IEnumerable<ManagementUnit> managementUnits)
        {
            ManagementUnits = new List<SelectListItem>();

            foreach (ManagementUnit managementUnit in managementUnits)
            {
                ManagementUnits.Add(new SelectListItem
                {
                    Value = managementUnit.Id.ToString(),
                    Text = managementUnit.Name.ToString()
                });
            }
        }

        public AddEmployeeViewModel(){}
    }
}
