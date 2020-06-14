using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyRegister.ViewModels.Companies
{
    public class EditCompanyInputModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "\"{0}\" must be between {2} and {1} characters!")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Creation Date")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
    }
}
