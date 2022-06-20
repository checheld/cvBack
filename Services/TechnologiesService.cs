using Data.Repositories.Abstract;
using Domain;
using Entities;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstract;
using AutoMapper;

namespace Services
{
    public class TechnologiesService : ITechnologiesService
    {
        private readonly ITechnologiesRepository _technologiesRepository;
        private readonly IMapper _mapper;
        public TechnologiesService(IMapper mapper, IServiceProvider _serviceProvider)
        {
            _mapper = mapper;
            _technologiesRepository = _serviceProvider.GetService<ITechnologiesRepository>();
        }

        // autoMapper
        public class AppMappingTechnology : Profile
        {
            public AppMappingTechnology()
            {
                CreateMap<TechnologyDTO, TechnologyEntity>().ReverseMap();
            }
        }
        //

        public async Task<TechnologyDTO> AddTechnology(TechnologyDTO technology)
        {
            try
            {
                /*TechnologyEntity newTechnology = TechnologyMapper.ToEntity(technology);*/
                var newTechnology = _mapper.Map<TechnologyEntity>(technology);
                newTechnology.CreatedAt = DateTime.Now;
                TechnologyEntity u = await _technologiesRepository.AddTechnology(newTechnology);
                if (u != null)
                {
                    /*TechnologyDTO item = TechnologyMapper.ToDomain(u);*/
                    var item = _mapper.Map<TechnologyDTO>(u);
                    return item;
                };
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return null;
        }
        
        public async Task<string> DeleteTechnologyById(int id)
        {
            try
            {
                return await _technologiesRepository.DeleteTechnologyById(id);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        public async Task<TechnologyDTO> GetTechnologyById(int id)
        {
            try
            {
                /*return TechnologyMapper.ToDomain(await _technologiesRepository.GetTechnologyById(id));*/
                return _mapper.Map<TechnologyDTO>(await _technologiesRepository.GetTechnologyById(id));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public async Task<List<TechnologyDTO>> GetTechnologiesBySearch(string search)
        {
            try
            {
                /*return TechnologyMapper.ToDomainList(await _technologiesRepository.GetTechnologiesBySearch(search));*/

                var searchTechnologies = await _technologiesRepository.GetTechnologiesBySearch(search);
                List<TechnologyDTO> technologies = new List<TechnologyDTO>();
                foreach (TechnologyEntity technology in searchTechnologies)
                {
                    technologies.Add(_mapper.Map<TechnologyDTO>(technology));
                }
                return technologies;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TechnologyDTO> UpdateTechnology(TechnologyDTO technology)
        {
            try
            {
                /*return TechnologyMapper.ToDomain(await _technologiesRepository.UpdateTechnology(TechnologyMapper.ToEntity(technology)));*/
                return _mapper.Map<TechnologyDTO>(await _technologiesRepository.UpdateTechnology(_mapper.Map<TechnologyEntity>(technology)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<TechnologyDTO>> GetAllTechnologies()
        {
            try
            {
                List<TechnologyEntity> technologyEntityList = await _technologiesRepository.GetAllTechnologies();
                List<TechnologyDTO> technologyDomainList = new List<TechnologyDTO>();
                /*technologyEntityList.ForEach(x => technologyDomainList.Add(TechnologyMapper.ToDomain(x)));*/
                technologyEntityList.ForEach(x => technologyDomainList.Add(_mapper.Map<TechnologyDTO>(x)));
                return technologyDomainList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
