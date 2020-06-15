using CompanyRegister.Models;
using System.Collections.Generic;

namespace CompanyRegister.Services.Contracts
{
    public interface ICompaniesService
    {
        void AddCompany(Company company);

        IEnumerable<Company> GettAllCompanies();

        Company GetCompanyById(string id);

        void UpdateCompany(Company company);

        void Delete(Company company);

        IEnumerable<Company> GettSearchedCompanies(string search);
    }
}
