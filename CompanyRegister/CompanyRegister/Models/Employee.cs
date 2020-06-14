using CompanyRegister.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyRegister.Models
{
    public class Employee
    {
        [Key]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime StartingDate { get; set; }

        public double Salary { get; set; }

        public int VacationDays { get; set; }

        public ExperienceLevel ExperienceLevel { get; set; }
       

        public Company Company { get; set; }       

        public Office Office { get; set; }
    }
}
