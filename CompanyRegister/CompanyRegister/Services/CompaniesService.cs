using CompanyRegister.Data;
using CompanyRegister.Models;
using CompanyRegister.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CompanyRegister.Services
{
    public class CompaniesService : ICompaniesService
    {
        private readonly CompanyRegisterDbContext context;

        public CompaniesService(CompanyRegisterDbContext context)
        {
            this.context = context;
        }

        public void AddCompany(Company company)
        {
            if (company == null)
            {
                return;
            }

            this.context.Companies.Add(company);
            this.context.SaveChanges();
        }

        public void Delete(Company company)
        {
            this.context.Companies.Remove(company);
            this.context.SaveChanges();
        }

        public Company GetCompanyById(string id)
        {
            Company company = this.context.Companies
                .Where(x => x.Id == id)
                .Include(x => x.Employees)
                .Include(x => x.Offices)                
                .FirstOrDefault();

            return company;
        }

        public IEnumerable<Company> GettAllCompanies()
        {
            List<Company> companies = this.context.Companies
                .Include(x => x.Employees)
                .OrderBy(x => x.Name)
                .ToList();

            return companies;
        }

        public void UpdateCompany(Company company)
        {
            if (company == null)
            {
                return;
            }

            this.context.Update(company);
            this.context.SaveChanges();
        }
    }
}
