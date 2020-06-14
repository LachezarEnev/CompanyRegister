using System;

namespace CompanyRegister.ViewModels.Employees
{
    public class DetailsEmployeeViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime StartingDate { get; set; }

        public double Salary { get; set; }

        public int VacationDays { get; set; }

        public string ExperienceLevel { get; set; }

        public string CompanyName { get; set; }

        public string Office { get; set; }
    }
}
