using Data.Repositories.Abstract;
using Domain;
using Entities;
using Mappers;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstract;

namespace Services
{
    public class CompaniesService : ICompaniesService
    {
        private readonly ICompaniesRepository _companiesRepository;
        public CompaniesService(IServiceProvider _serviceProvider)
        {
            _companiesRepository = _serviceProvider.GetService<ICompaniesRepository>();
        }

        public async Task<Company> AddCompany(Company company)
        {
            try
            {
                CompanyEntity newCompany = CompanyMapper.ToEntity(company);
                CompanyEntity c = await _companiesRepository.AddCompany(newCompany);
                if (c != null)
                {
                    Company item = CompanyMapper.ToDomain(c);
                    return item;
                };
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return null;
        }

        public async Task<string> DeleteCompanyById(int id)
        {
            try
            {
                return await _companiesRepository.DeleteCompanyById(id);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<Company> GetCompanyById(int id)
        {
            try
            {
                return CompanyMapper.ToDomain(await _companiesRepository.GetCompanyById(id));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Company>> GetCompaniesBySearch(string search)
        {
            try
            {
                return CompanyMapper.ToDomainList(await _companiesRepository.GetCompaniesBySearch(search));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Company> UpdateCompany(Company company)
        {
            try
            {
                return CompanyMapper.ToDomain(await _companiesRepository.UpdateCompany(CompanyMapper.ToEntity(company)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<Company>> GetAllCompanies()
        {
            try
            {
                List<CompanyEntity> companyEntityList = await _companiesRepository.GetAllCompanies();
                List<Company> companyDomainList = new List<Company>();
                companyEntityList.ForEach(x => companyDomainList.Add(CompanyMapper.ToDomain(x)));
                return companyDomainList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
