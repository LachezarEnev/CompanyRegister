using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyRegister.Models
{
    public class Company
    {
        public Company()
        {
            this.Offices = new HashSet<Office>();
            this.Employees = new HashSet<Employee>();
        }

        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public string Headquarter { get; set; }

        public ICollection<Office> Offices { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
