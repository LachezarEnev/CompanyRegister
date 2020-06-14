using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyRegister.Models
{
    public class Office
    {
        public Office()
        {
            this.Employees = new HashSet<Employee>();
        }

        [Key]
        public string Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public bool Headquarter { get; set; }

        public Company Company { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
