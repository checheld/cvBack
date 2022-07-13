#region Imports
using Domain;
using Entities;
using Services.Abstract;
using AutoMapper;
using Data.Repositories.Utility.Interface;
#endregion

namespace Services
{
    public class CompaniesService : ICompaniesService
    {
        #region Logic
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CompaniesService(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public class AppMappingCompany : Profile
        {
            public AppMappingCompany()
            {
                CreateMap<CompanyDTO, CompanyEntity>().ReverseMap();
            }
        }
        #endregion

        public async Task<List<CompanyDTO>> AddCompanies(List<CompanyDTO> company)
        {
            try
            {
                var companies = new List<CompanyEntity>();

                foreach (var c in company)
                {
                    var newCompany = _mapper.Map<CompanyEntity>(c);
                    newCompany.CreatedAt = DateTime.UtcNow;
                    companies.Add(newCompany);
                }

                var returnCompanies = await _repositoryManager.CompaniesRepository.AddCompanies(companies);
                var returnCompaniesMap = new List<CompanyDTO>();

                returnCompanies.ForEach(c => returnCompaniesMap.Add(_mapper.Map<CompanyDTO>(c)));

                return returnCompaniesMap;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteCompanyById(int id)
        {
            try
            {
                await _repositoryManager.CompaniesRepository.DeleteCompanyById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<CompanyDTO> GetCompanyById(int id)
        {
            try
            {
                return _mapper.Map<CompanyDTO>(await _repositoryManager.CompaniesRepository.GetCompanyById(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<List<CompanyDTO>> GetCompaniesBySearch(string search)
        {
            try
            {
                var searchCompanies = await _repositoryManager.CompaniesRepository.GetCompaniesBySearch(search);

                List<CompanyDTO> companies = new List<CompanyDTO>();

                foreach (CompanyEntity company in searchCompanies)
                {
                    companies.Add(_mapper.Map<CompanyDTO>(company));
                }

                return companies;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<CompanyDTO> UpdateCompany(CompanyDTO company)
        {
            try
            {
                return _mapper.Map<CompanyDTO>(await _repositoryManager.CompaniesRepository.UpdateCompany(_mapper.Map<CompanyEntity>(company)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CompanyDTO>> GetAllCompanies()
        {
            try
            {
                List<CompanyEntity> companyEntityList = await _repositoryManager.CompaniesRepository.GetAllCompanies();

                List<CompanyDTO> companyDomainList = new List<CompanyDTO>();
                companyEntityList.ForEach(x => companyDomainList.Add(_mapper.Map<CompanyDTO>(x)));

                return companyDomainList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
