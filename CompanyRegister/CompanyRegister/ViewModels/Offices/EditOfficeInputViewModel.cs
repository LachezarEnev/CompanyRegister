using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyRegister.ViewModels.Offices
{
    public class EditOfficeInputViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "\"{0}\" must be between {2} and {1} characters!")]
        public string Country { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "\"{0}\" must be between {2} and {1} characters!")]
        public string City { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "\"{0}\" must be between {2} and {1} characters!")]
        public string Street { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "\"{0}\" must be number between {1} and {2}!")]
        [Display(Name = "Street Number")]
        public int StreetNumber { get; set; }

        public bool Headquarter { get; set; }       
    }
}
