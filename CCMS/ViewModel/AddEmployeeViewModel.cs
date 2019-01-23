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
        [Display(Name ="Department")]
        public long DepartmentId { get; set; }

        [Required(ErrorMessage ="Employee must have a hire date")]
        [Display(Name ="Hire Date")]
        public DateTime HireDate { get; set; }

        public List<SelectListItem> Departments { get; set; }
        
        public AddEmployeeViewModel(IEnumerable<Department> departments)
        {
            Departments = new List<SelectListItem>();

            foreach (Department department in departments)
            {
                Departments.Add(new SelectListItem
                {
                    Value = department.Id.ToString(),
                    Text = department.Name.ToString()
                });
            }
        }

        public AddEmployeeViewModel(){}
    }
}
