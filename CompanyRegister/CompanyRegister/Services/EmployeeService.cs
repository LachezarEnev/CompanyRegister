using CompanyRegister.Data;
using CompanyRegister.Models;
using CompanyRegister.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyRegister.Services
{
    public class EmployeeService : IEmployeesService
    {
        private readonly CompanyRegisterDbContext context;

        public EmployeeService(CompanyRegisterDbContext context)
        {
            this.context = context;
        }

        public void Delete(Employee employee)
        {
            this.context.Employees.Remove(employee);
            this.context.SaveChanges();
        }

        public void DeleteRange(List<Employee> employees)
        {
            this.context.Employees.RemoveRange(employees);
            this.context.SaveChanges();
        }

        public Employee GetEmployeeById(string id)
        {
            Employee employee = this.context.Employees
                .Where(x => x.Id == id)
                .Include(x => x.Office)
                .Include(x => x.Company)
                .FirstOrDefault();

            return employee;
        }

        public IEnumerable<Employee> GettAllEmployees(string id)
        {
            List<Employee> employees = this.context.Employees
                .Where(x => x.Company.Id == id)                
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)                
                .ToList();

            return employees;
        }

        public void UpdateEmployee(Employee employee)
        {
            if (employee == null)
            {
                return;
            }

            this.context.Update(employee);
            this.context.SaveChanges();
        }
    }
}
