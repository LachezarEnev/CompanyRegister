using System;

namespace CompanyRegister.ViewModels.Companies
{
    public class DetailsCompanyViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public string HeadQuarter { get; set; }

        public int NumberOfOffices { get; set; }

        public int NumberOfEmployees { get; set; }
    }
}
