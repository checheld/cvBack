using Domain;

namespace Services.Abstract
{
    public interface ICompaniesService
    {
        Task<CompanyDTO> AddCompany(CompanyDTO company);
        Task<string> DeleteCompanyById(int id);
        Task<CompanyDTO> GetCompanyById(int id);
        Task<List<CompanyDTO>> GetCompaniesBySearch(string search);
        Task<CompanyDTO> UpdateCompany(CompanyDTO company);
        Task<List<CompanyDTO>> GetAllCompanies();
    }
}
