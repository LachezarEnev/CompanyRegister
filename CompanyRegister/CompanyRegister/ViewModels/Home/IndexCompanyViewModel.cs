using System;

namespace CompanyRegister.ViewModels.Home
{
    public class IndexCompanyViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public string Headquarter { get; set; }

        public int NumberOfEmployees { get; set; }
    }
}
