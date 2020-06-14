using CompanyRegister.Models;
using System.Collections.Generic;

namespace CompanyRegister.Services.Contracts
{
    public interface IOfficesService
    {
        IEnumerable<Office> GettAllOffices(string id);

        Office GetOfficeById(string id);

        void UpdateOffice(Office office);

        void Delete(Office office);

        void DeleteRange(List<Office> offices);
    }
}
