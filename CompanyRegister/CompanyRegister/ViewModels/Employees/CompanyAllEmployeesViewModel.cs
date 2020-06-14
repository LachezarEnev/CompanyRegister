using System.Collections.Generic;

namespace CompanyRegister.ViewModels.Employees
{
    public class CompanyAllEmployeesViewModel
    {
        public CompanyAllEmployeesViewModel()
        {
            this.Employees = new List<CompanyEmployeeViewModel>();
        }

        public string CompanyName { get; set; }

        public List<CompanyEmployeeViewModel> Employees { get; set; }
    }
}
