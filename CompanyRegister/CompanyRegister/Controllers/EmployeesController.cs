using CompanyRegister.Models;
using CompanyRegister.Services.Contracts;
using CompanyRegister.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace CompanyRegister.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ICompaniesService companiesService;
        private readonly IOfficesService officesService;
        private readonly IEmployeesService employeesService;

        public EmployeesController(ICompaniesService companiesService, IOfficesService officesService, IEmployeesService employeesService)
        {
            this.companiesService = companiesService;
            this.officesService = officesService;
            this.employeesService = employeesService;
        }

        public IActionResult Create(string id)
        {
            Company company = this.companiesService.GetCompanyById(id);            

            var offices = company.Offices.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.City}"
            }).ToList();

            CreateEmployeeInputModel model = new CreateEmployeeInputModel { Offices = offices};

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeInputModel model)
        {
            Company company = this.companiesService.GetCompanyById(model.Id);

            model.Offices = company.Offices.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.City}"
            }).ToList();

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            Employee employee = new Employee
            {                
                FirstName = model.FirstName,
                LastName = model.LastName,  
                StartingDate = model.StartingDate,
                Salary = model.Salary,
                VacationDays = model.VacationDays,
                ExperienceLevel = model.ExperienceLevel                
            };

            if (model.SelectedOffice != null)
            {
                string officeId = model.SelectedOffice;
                Office office = this.officesService.GetOfficeById(officeId);
                office.Employees.Add(employee);
            }                      
            
            company.Employees.Add(employee);            
            this.companiesService.UpdateCompany(company);

            return this.RedirectToAction("Details", "Companies", new { @id = model.Id });
        }

        public IActionResult All(string id)
        {
            List<Employee> employees = this.employeesService.GettAllEmployees(id).ToList();
            Company company = this.companiesService.GetCompanyById(id);
            CompanyAllEmployeesViewModel viewModel = new CompanyAllEmployeesViewModel();            

            viewModel.CompanyName = company.Name;

            foreach (var currentEmployee in employees)
            {
                Office office = new Office();
                if (currentEmployee.Office != null)
                {
                    office = this.officesService.GetOfficeById(currentEmployee.Office.Id);
                }               

                var employee = new CompanyEmployeeViewModel
                {
                    Id = currentEmployee.Id,
                    FirstName = currentEmployee.FirstName,
                    LastName = currentEmployee.LastName,
                    ExperienceLevel = currentEmployee.ExperienceLevel.ToString(),
                    Office = office.City != null ? office.City : "Home Office"
                };

                viewModel.Employees.Add(employee);
            }
            return View(viewModel);
        }

        public IActionResult Details(string id)
        {
            Employee employee = this.employeesService.GetEmployeeById(id);

            if (employee == null)
            {
                return this.View("NotFound");
            }

            Office office = new Office();

            if (employee.Office != null)
            {
                office = this.officesService.GetOfficeById(employee.Office.Id);
            }            

            DetailsEmployeeViewModel employeeVieModel = new DetailsEmployeeViewModel
            {
               Id = employee.Id,
               FirstName = employee.FirstName,
               LastName = employee.LastName,
               StartingDate = employee.StartingDate,
               Salary = employee.Salary,
               VacationDays = employee.VacationDays,
               ExperienceLevel = employee.ExperienceLevel.ToString(),
               CompanyName = employee.Company.Name,
               Office = office.City != null ? office.City : "Home Office"
            };

            return this.View(employeeVieModel);
        }

        public IActionResult Edit(string id)
        {
            Employee employee = this.employeesService.GetEmployeeById(id);

            if (employee == null)
            {
                return this.View("NotFound");
            }

            Company company = this.companiesService.GetCompanyById(employee.Company.Id);

            var offices = company.Offices.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.City}"
            }).ToList();

            
            EditEmployeeInputModel model = new EditEmployeeInputModel 
            {   
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                StartingDate = employee.StartingDate,
                Salary = employee.Salary,
                VacationDays = employee.VacationDays,
                ExperienceLevel = employee.ExperienceLevel,
                Offices = offices
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditEmployeeInputModel model)
        {
            Employee employee = this.employeesService.GetEmployeeById(model.Id);
            Company company = this.companiesService.GetCompanyById(employee.Company.Id);

            model.Offices = company.Offices.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.City}"
            }).ToList();

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.StartingDate = model.StartingDate;
            employee.Salary = model.Salary;
            employee.VacationDays = model.VacationDays;
            employee.ExperienceLevel = model.ExperienceLevel;

            if (model.SelectedOffice != null)
            {                
                if (employee.Office != null)
                {
                    Office currentOffice = this.officesService.GetOfficeById(employee.Office.Id);
                    currentOffice.Employees.Remove(employee);
                    this.officesService.UpdateOffice(currentOffice);
                }                              

                Office office = this.officesService.GetOfficeById(model.SelectedOffice);
                office.Employees.Add(employee);
                
                this.officesService.UpdateOffice(office);
            }

            this.employeesService.UpdateEmployee(employee);

            return this.RedirectToAction("Details", new { @id = model.Id });
        }

        public IActionResult Delete (string id)
        {
            Employee employee = this.employeesService.GetEmployeeById(id);

            employee.Office.Employees.Remove(employee);
            employee.Company.Employees.Remove(employee);

            this.employeesService.Delete(employee);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
