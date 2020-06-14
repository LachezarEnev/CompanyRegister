using CompanyRegister.Models;
using CompanyRegister.Services.Contracts;
using CompanyRegister.ViewModels.Offices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CompanyRegister.Controllers
{
    public class OfficesController : Controller
    {
        private readonly ICompaniesService companiesService;
        private readonly IOfficesService officesService;

        public OfficesController(ICompaniesService companiesService, IOfficesService officesService)
        {
            this.companiesService = companiesService;
            this.officesService = officesService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateOfficeInputModel model)
        {           
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            
            Office office = new Office
            {
                Country = model.Country,
                City = model.City,
                Street = model.Street,
                StreetNumber = model.StreetNumber,
                Headquarter = model.Headquarter,                
            };

            Company company = this.companiesService.GetCompanyById(model.Id);
            
            if (office.Headquarter == true)
            {
                foreach (var currentOffice in company.Offices)
                {
                    if (currentOffice.Headquarter == true)
                    {
                        currentOffice.Headquarter = false;
                    }
                }
                company.Headquarter = office.City;
            }

            company.Offices.Add(office);
            this.companiesService.UpdateCompany(company);

            return this.RedirectToAction("Details", "Companies", new {@id = model.Id});
        }

        public IActionResult All(string id)
        {
            List<Office> offices = this.officesService.GettAllOffices(id).ToList();            
            Company company = this.companiesService.GetCompanyById(id);
            CompanyAllOfficesViewModel viewModel = new CompanyAllOfficesViewModel();

            viewModel.CompanyName = company.Name;

            foreach (var currentOffice in offices)
            {
                var office = new CompanyOfficesViewModel
                {
                    Id = currentOffice.Id,
                    CompanyName = company.Name,
                    Country = currentOffice.Country,
                    City = currentOffice.City,
                    Headquarter = currentOffice.Headquarter == true ? "YES" : "NO",
                    CountOfEmployees = currentOffice.Employees.Count()
                };

                viewModel.Offices.Add(office);                
            }

            return View(viewModel);
        }

        public IActionResult Details(string id)
        {
            Office office = this.officesService.GetOfficeById(id);

            if (office == null)
            {
                return this.View("NotFound");
            }

            DetailsOfficeViewModel officeVieModel = new DetailsOfficeViewModel
            {
               Id = office.Id, 
               Country = office.Country,
               City = office.City,
               Street = office.Street,
               StreetNumber = office.StreetNumber,
               Company = office.Company.Name,
               NumberOfEmployees = office.Employees.Count(),
               Headquarter = office.Headquarter == true ? "Yes" : "No"
            };

            return this.View(officeVieModel);
        }

        public IActionResult Edit(string id)
        {
            Office office = this.officesService.GetOfficeById(id);

            if (office == null)
            {
                return this.View("NotFound");
            }

            EditOfficeInputViewModel model = new EditOfficeInputViewModel
            {
                Id = office.Id,
                Country = office.Country,
                City = office.City,
                Street = office.Street,
                StreetNumber = office.StreetNumber,
                Headquarter = office.Headquarter                             
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditOfficeInputViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            
            Office office = this.officesService.GetOfficeById(model.Id);
            Company company = this.companiesService.GetCompanyById(office.Company.Id);

            office.Country = model.Country;
            office.City = model.City;
            office.Street = model.Street;
            office.StreetNumber = model.StreetNumber;
            office.Headquarter = model.Headquarter;             

            if (model.Headquarter == true)
            {
                foreach (var currentOffice in company.Offices)
                {
                    if (currentOffice.Headquarter == true && currentOffice.Id != office.Id)
                    {
                        currentOffice.Headquarter = false;

                        this.officesService.UpdateOffice(currentOffice);
                    }
                }
                office.Company.Headquarter = office.City;
            }

            this.officesService.UpdateOffice(office);            

            return this.RedirectToAction("Details", new { @id = office.Id });
        }

        public IActionResult Delete(string id)
        {
            Office office = this.officesService.GetOfficeById(id);

            office.Company.Offices.Remove(office);

            if (office.Headquarter == true)
            {
                office.Company.Headquarter = null;
            }

            this.officesService.Delete(office);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
