using CompanyRegister.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyRegister.ViewModels.Employees
{
    public class CreateEmployeeInputModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "\"{0}\" must be between {2} and {1} characters!")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "\"{0}\" must be between {2} and {1} characters!")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Starting Date")]
        public DateTime StartingDate { get; set; } = DateTime.Now;

        [Required]
        [Range(typeof(double), "0.01", "100000", ErrorMessage = "\"{0}\" must be between {1} and {2}!")]
        public double Salary { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "\"{0}\" must be number between {1} and {2}!")]
        [Display(Name = "Vacation Days")]
        public int VacationDays { get; set; }

        [Required]
        public ExperienceLevel ExperienceLevel { get; set; }

        [Display(Name = "Office")]        
        public string SelectedOffice { get; set; }
        public ICollection<SelectListItem> Offices { get; set; }
    }
}
