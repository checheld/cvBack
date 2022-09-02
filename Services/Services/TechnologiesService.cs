#region Imports
using Domain;
using Entities;
using Services.Abstract;
using AutoMapper;
using Data.Repositories.Utility.Interface;
#endregion

namespace Services
{
    public class TechnologiesService : ITechnologiesService
    {
        #region Logic
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public TechnologiesService(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public class AppMappingTechnology : Profile
        {
            public AppMappingTechnology()
            {
                CreateMap<TechnologyDTO, TechnologyEntity>().ReverseMap();
            }
        }
        #endregion

        public async Task<List<TechnologyDTO>> AddTechnology(List<TechnologyDTO> technology)
        {
            try
            {
                var technologies = new List<TechnologyEntity>();

                foreach (var tech in technology)
                {
                    var newTech = _mapper.Map<TechnologyEntity>(tech);
                    newTech.CreatedAt = DateTime.UtcNow;
                    technologies.Add(newTech);
                }

                var returnTechnologies = await _repositoryManager.TechnologiesRepository.AddTechnology(technologies);
                var returnTechnologiesMap = new List<TechnologyDTO>();

                returnTechnologies.ForEach(c => returnTechnologiesMap.Add(_mapper.Map<TechnologyDTO>(c)));

                return returnTechnologiesMap;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<int> DeleteTechnologyById(int id)
        {
            try
            {
                await _repositoryManager.TechnologiesRepository.DeleteTechnologyById(id);

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<TechnologyDTO> GetTechnologyById(int id)
        {
            try
            {
                return _mapper.Map<TechnologyDTO>(await _repositoryManager.TechnologiesRepository.GetTechnologyById(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<List<TechnologyDTO>> GetTechnologiesBySearch(string search)
        {
            try
            {
                var searchTechnologies = await _repositoryManager.TechnologiesRepository.GetTechnologiesBySearch(search);

                List<TechnologyDTO> technologies = new List<TechnologyDTO>();

                foreach (TechnologyEntity technology in searchTechnologies)
                {
                    technologies.Add(_mapper.Map<TechnologyDTO>(technology));
                }

                return technologies;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TechnologyDTO> UpdateTechnology(TechnologyDTO technology)
        {
            try
            {
                return _mapper.Map<TechnologyDTO>(await _repositoryManager.TechnologiesRepository
                    .UpdateTechnology(_mapper.Map<TechnologyEntity>(technology)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TechnologyDTO>> GetAllTechnologies()
        {
            try
            {
                List<TechnologyEntity> technologyEntityList = await _repositoryManager.TechnologiesRepository.GetAllTechnologies();

                List<TechnologyDTO> technologyDomainList = new List<TechnologyDTO>();
                technologyEntityList.ForEach(x => technologyDomainList.Add(_mapper.Map<TechnologyDTO>(x)));

                return technologyDomainList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
