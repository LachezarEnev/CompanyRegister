using CompanyRegister.Models;
using System.Collections.Generic;

namespace CompanyRegister.Services.Contracts
{
    public interface IEmployeesService
    {
        IEnumerable<Employee> GettAllEmployees(string id);

        Employee GetEmployeeById(string id);

        void UpdateEmployee(Employee employee);

        void Delete(Employee employee);

        void DeleteRange(List<Employee> employees);
    }
}
