#region Imports
using Data.Repositories.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace Data.Repositories
{
    public class CompaniesRepository : ICompaniesRepository
    {
        private ApplicationContext db;
        public CompaniesRepository(IServiceProvider _serviceProvider)
        {
            db = _serviceProvider.GetService<ApplicationContext>();
        }

        public async Task<List<CompanyEntity>> AddCompanies(List<CompanyEntity> company)
        {
            try
            {
                await db.Companies.AddRangeAsync(company);
                await db.SaveChangesAsync();

                return company;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteCompanyById(int id)
        {
            try
            {
                db.Companies.Remove(await db.Companies.SingleOrDefaultAsync(x => x.Id == id));
                await db.SaveChangesAsync();

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CompanyEntity>> GetAllCompanies()
        {
            try
            {
                return await db.Companies.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CompanyEntity> GetCompanyById(int id)
        {
            try
            {
                return await db.Companies.SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CompanyEntity>> GetCompaniesBySearch(string search)
        {
            try
            {
                return await db.Companies
                    .Where(comp => comp.Name.Trim().ToLower().Contains(search))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
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
                throw ex;
            }

        }
    }
}
