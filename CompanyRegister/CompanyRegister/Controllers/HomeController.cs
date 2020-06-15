using CompanyRegister.Models;
using CompanyRegister.Services.Contracts;
using CompanyRegister.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

        [Route("{*url}", Order = 999)]
        public IActionResult Error404()
        {
            Response.StatusCode = 404;
            return View();
        }

        public IActionResult Index()
        {
            List<Company> companies = this.copaniesService.GettAllCompanies().ToList();

            List<IndexCompanyViewModel> allCompanies = MapAllCompaniesView(companies);          

            return View(allCompanies);
        }      

        public IActionResult Search(SearchCompanyViewModel model)
        {
            if (model.SearchString == null)
            {
                return this.View("NotFound");
            }

            List<Company> companies = this.copaniesService.GettSearchedCompanies(model.SearchString).ToList();
           
            if (companies.Count == 0)
            {
                return this.View("NotFound");
            }

            List<IndexCompanyViewModel> allCompanies = MapAllCompaniesView(companies);

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

        private List<IndexCompanyViewModel> MapAllCompaniesView(List<Company> companies)
        {
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

            return allCompanies;
        }
    }
}
