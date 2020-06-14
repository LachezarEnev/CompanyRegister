using CompanyRegister.Data;
using CompanyRegister.Models;
using CompanyRegister.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CompanyRegister.Services
{
    public class OfficesService : IOfficesService
    {
        private CompanyRegisterDbContext context;

        public OfficesService(CompanyRegisterDbContext context)
        {
            this.context = context;
        }

        public void Delete(Office office)
        {
            this.context.Offices.Remove(office);
            this.context.SaveChanges();            
        }

        public void DeleteRange(List<Office> offices)
        {
            this.context.Offices.RemoveRange(offices);
            this.context.SaveChanges();
        }

        public Office GetOfficeById(string id)
        {
            Office office = this.context.Offices
                .Where(x => x.Id == id)
                .Include(x => x.Employees)
                .Include(x => x.Company)
                .FirstOrDefault();

            return office;
        }

        public IEnumerable<Office> GettAllOffices(string id)
        {
            List<Office> offices = this.context.Offices
                .Where(x => x.Company.Id == id)
                .Include(x => x.Employees)
                .OrderBy(x => x.Country)
                .ThenBy(x => x.City)
                .ToList();

            return offices;
        }

        public void UpdateOffice(Office office)
        {
            if (office == null)
            {
                return;
            }

            this.context.Update(office);
            this.context.SaveChanges();
        }
    }
}
