using Data.Repositories.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Repositories
{
    public class CompaniesRepository : ICompaniesRepository
    {
        private ApplicationContext db;
        public CompaniesRepository(IServiceProvider _serviceProvider)
        {
            db = _serviceProvider.GetService<ApplicationContext>();
        }
        public async Task<CompanyEntity> AddCompany(CompanyEntity company)
        {
            var foundCompany = await db.Companies.SingleOrDefaultAsync(x => x.Id == company.Id);
            if (foundCompany == null)
            {
                await db.Companies.AddAsync(company);
                await db.SaveChangesAsync();
                return company;
            }
            return null;
        }

        public async Task<string> DeleteCompanyById(int id)
        {
            var foundCompany = await db.Companies.SingleOrDefaultAsync(x => x.Id == id);
            if (foundCompany != null)
            {
                db.Companies.Remove(foundCompany);
                await db.SaveChangesAsync();
            }
            return null;
        }

        public async Task<List<CompanyEntity>> GetAllCompanies()
        {
            var companies = await db.Companies.ToListAsync();
            if (companies != null)
            {
                return companies;
            }
            return null;
        }

        public async Task<CompanyEntity> GetCompanyById(int id)
        {
            return await db.Companies.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CompanyEntity>> GetCompaniesBySearch(string search)
        {
            try
            {
                return await db.Companies
                    .Where(comp => comp.Name.Contains(search))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public async Task<CompanyEntity> UpdateCompany(CompanyEntity company)
        {
            try
            {
                db.Companies.Update(company);
                await db.SaveChangesAsync();
                return company;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
    }
}
