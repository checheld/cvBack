using Entities;
using System.Collections.Generic;

namespace Data.Repositories.Abstract
{
    public interface ICompaniesRepository
    {
        Task<CompanyEntity> AddCompany(CompanyEntity company);
        Task<CompanyEntity> UpdateCompany(CompanyEntity company);
        Task<CompanyEntity> GetCompanyById(int id);
        Task<string> DeleteCompanyById(int id);
        Task<List<CompanyEntity>> GetAllCompanies();
    }
}
