using Data.Repositories.Abstract;
using Domain;
using Entities;
using Mappers;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstract;
using AutoMapper;

namespace Services
{
    public class CompaniesService : ICompaniesService
    {
        private readonly ICompaniesRepository _companiesRepository;
        private readonly IMapper _mapper;

        public CompaniesService(IMapper mapper, IServiceProvider _serviceProvider)
        {
            _mapper = mapper;
            _companiesRepository = _serviceProvider.GetService<ICompaniesRepository>();
        }

        // autoMapper
        public class AppMappingCompany : Profile
        {
            public AppMappingCompany()
            {
                CreateMap<CompanyDTO, CompanyEntity>().ReverseMap();
            }
        }
        //

        public async Task<CompanyDTO> AddCompany(CompanyDTO company)
        {
            try
            {
                /*CompanyEntity newCompany = CompanyMapper.ToEntity(company);*/
                var newCompany = _mapper.Map<CompanyEntity>(company);
                newCompany.CreatedAt = DateTime.Now;
                CompanyEntity c = await _companiesRepository.AddCompany(newCompany);
                if (c != null)
                {
                    /*CompanyDTO item = CompanyMapper.ToDomain(c);*/
                    var item = _mapper.Map<CompanyDTO>(c);

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
        
        public async Task<CompanyDTO> GetCompanyById(int id)
        {
            try
            {
                /*return CompanyMapper.ToDomain(await _companiesRepository.GetCompanyById(id));*/
                return _mapper.Map<CompanyDTO>(await _companiesRepository.GetCompanyById(id));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public async Task<List<CompanyDTO>> GetCompaniesBySearch(string search)
        {
            try
            {
                /*return CompanyMapper.ToDomainList(await _companiesRepository.GetCompaniesBySearch(search));*/

                var searchCompanies = await _companiesRepository.GetCompaniesBySearch(search);
                List<CompanyDTO> companies = new List<CompanyDTO>();
                foreach (CompanyEntity company in searchCompanies)
                {
                    companies.Add(_mapper.Map<CompanyDTO>(company));
                }
                return companies;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public async Task<CompanyDTO> UpdateCompany(CompanyDTO company)
        {
            try
            {
                /*return CompanyMapper.ToDomain(await _companiesRepository.UpdateCompany(CompanyMapper.ToEntity(company)));*/
                return _mapper.Map<CompanyDTO>(await _companiesRepository.UpdateCompany(_mapper.Map<CompanyEntity>(company)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<CompanyDTO>> GetAllCompanies()
        {
            try
            {
                List<CompanyEntity> companyEntityList = await _companiesRepository.GetAllCompanies();
                List<CompanyDTO> companyDomainList = new List<CompanyDTO>();
                /*companyEntityList.ForEach(x => companyDomainList.Add(CompanyMapper.ToDomain(x)));*/
                companyEntityList.ForEach(x => companyDomainList.Add(_mapper.Map<CompanyDTO>(x)));
                return companyDomainList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
