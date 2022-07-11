using Entities;

namespace Data.Repositories.Abstract
{
    public interface ICompaniesRepository
    {
        Task<List<CompanyEntity>> AddCompanies(List<CompanyEntity> company);
        Task<CompanyEntity> UpdateCompany(CompanyEntity company);
        Task<CompanyEntity> GetCompanyById(int id);
        Task<List<CompanyEntity>> GetCompaniesBySearch(string search);
        Task DeleteCompanyById(int id);
        Task<List<CompanyEntity>> GetAllCompanies();
    }
}
