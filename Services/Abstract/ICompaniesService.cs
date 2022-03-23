using Domain;

namespace Services.Abstract
{
    public interface ICompaniesService
    {
        Task<Company> AddCompany(Company company);
        Task<string> DeleteCompanyById(int id);
        Task<Company> GetCompanyById(int id);
        Task<List<Company>> GetCompaniesBySearch(string search);
        Task<Company> UpdateCompany(Company company);
        Task<List<Company>> GetAllCompanies();
    }
}
