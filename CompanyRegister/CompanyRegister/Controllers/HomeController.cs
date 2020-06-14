using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CompanyRegister.Models;
using CompanyRegister.ViewModels.Home;
using CompanyRegister.Services.Contracts;

namespace CompanyRegister.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICompaniesService copaniesService;

        public HomeController(ILogger<HomeController> logger, ICompaniesService companiesService)
        {
            _logger = logger;
            this.copaniesService = companiesService;
        }

        public IActionResult Index()
        {
            List<Company> companies = this.copaniesService.GettAllCompanies().ToList();
            List<IndexCompanyViewModel> allCompanies = new List<IndexCompanyViewModel>();

            foreach (var currentCompany in companies)
            {
                var company = new IndexCompanyViewModel
                {
                    Id = currentCompany.Id,
                    Name = currentCompany.Name,
                    CreationDate = currentCompany.CreationDate,
                    Headquarter = currentCompany.Headquarter,
                    NumberOfEmployees = currentCompany.Employees.Count()
                };

                allCompanies.Add(company);                
            }

            return View(allCompanies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
