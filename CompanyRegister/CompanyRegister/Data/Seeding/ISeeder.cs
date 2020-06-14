namespace CompanyRegister.Data.Seeding
{
    using CompanyRegister.Data;
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(CompanyRegisterDbContext dbContext, IServiceProvider serviceProvider);
    }
}
