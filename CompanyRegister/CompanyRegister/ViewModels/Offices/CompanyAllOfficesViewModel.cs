using System.Collections.Generic;

namespace CompanyRegister.ViewModels.Offices
{
    public class CompanyAllOfficesViewModel
    {
        public CompanyAllOfficesViewModel()
        {
            this.Offices = new List<CompanyOfficesViewModel>();
        }
        public string CompanyName { get; set; }

        public List<CompanyOfficesViewModel> Offices { get; set; }
    }
}
