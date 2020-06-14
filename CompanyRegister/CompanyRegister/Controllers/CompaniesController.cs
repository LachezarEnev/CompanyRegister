using CompanyRegister.Models;
using CompanyRegister.Services.Contracts;
using CompanyRegister.ViewModels.Companies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CompanyRegister.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompaniesService companiesService;
        private readonly IOfficesService officesService;
        private readonly IEmployeesService employeesService;

        public CompaniesController(ICompaniesService companiesService, IOfficesService officesService, IEmployeesService employeesService)
        {
            this.companiesService = companiesService;
            this.officesService = officesService;
            this.employeesService = employeesService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateCompanyInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            Company company = new Company
            {
                Name = model.Name,
                CreationDate = model.CreationDate
            };

            this.companiesService.AddCompany(company);

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Details(string id)
        {
            Company company = this.companiesService.GetCompanyById(id);

            if (company == null)
            {
                return this.View("NotFound");
            }

            DetailsCompanyViewModel companyVieModel = new DetailsCompanyViewModel
            {
                Id = company.Id,
                Name = company.Name,
                CreationDate = company.CreationDate,
                HeadQuarter = company.Headquarter,
                NumberOfEmployees = company.Employees.Count(),
                NumberOfOffices = company.Offices.Count()
            };

            return this.View(companyVieModel);
        }

        public IActionResult Edit(string id)
        {
            Company company = this.companiesService.GetCompanyById(id);

            if (company == null)
            {
                return this.View("NotFound");
            }

            EditCompanyInputModel model = new EditCompanyInputModel
            {
                Id = company.Id,
                Name = company.Name,
                CreationDate = company.CreationDate
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditCompanyInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            Company company = this.companiesService.GetCompanyById(model.Id);

            company.Name = model.Name;
            company.CreationDate = model.CreationDate;

            this.companiesService.UpdateCompany(company);

            return this.RedirectToAction("Details", new { @id = company.Id });
        }

        public IActionResult Delete(string id)
        {
            Company company = this.companiesService.GetCompanyById(id);

            List<Office> officesForRemove = new List<Office>();
            List<Employee> employeesForRemove = new List<Employee>();

            foreach (var office in company.Offices)
            {
                officesForRemove.Add(office);
            }

            foreach (var employee in company.Employees)
            {
                employeesForRemove.Add(employee);
            }

            this.officesService.DeleteRange(officesForRemove);
            this.employeesService.DeleteRange(employeesForRemove);
            this.companiesService.Delete(company);

            return this.RedirectToAction("index", "Home");
        }
    }
}
