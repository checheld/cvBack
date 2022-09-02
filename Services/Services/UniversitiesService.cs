#region Imports
using AutoMapper;
using Data.Repositories.Utility.Interface;
using Domain;
using Entities;
using Services.Abstract;
#endregion

namespace Services
{
    public class UniversitiesService : IUniversitiesService
    {
        #region Logic
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public UniversitiesService(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public class AppMappingUniversity : Profile
        {
            public AppMappingUniversity()
            {
                CreateMap<UniversityDTO, UniversityEntity>().ReverseMap();
            }
        }
        #endregion

        public async Task<List<UniversityDTO>> AddUniversities(List<UniversityDTO> university)
        {
            try
            {
                var universities = new List<UniversityEntity>();

                foreach (var u in university)
                {
                    var newUniversity = _mapper.Map<UniversityEntity>(u);
                    newUniversity.CreatedAt = DateTime.UtcNow;
                    universities.Add(newUniversity);
                }

                var returnUniversities = await _repositoryManager.UniversitiesRepository.AddUniversities(universities);
                var returnUniversitiesMap = new List<UniversityDTO>();

                returnUniversities.ForEach(c => returnUniversitiesMap.Add(_mapper.Map<UniversityDTO>(c)));

                return returnUniversitiesMap;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<int> DeleteUniversityById(int id)
        {
            try
            {
                await _repositoryManager.UniversitiesRepository.DeleteUniversityById(id);

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<UniversityDTO> GetUniversityById(int id)
        {
            try
            {
                return _mapper.Map<UniversityDTO>(await _repositoryManager.UniversitiesRepository.GetUniversityById(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<List<UniversityDTO>> GetUniversitiesBySearch(string search)
        {
            try
            {
                var searchUniversities = await _repositoryManager.UniversitiesRepository.GetUniversitiesBySearch(search);

                List<UniversityDTO> universities = new List<UniversityDTO>();

                foreach (UniversityEntity university in searchUniversities)
                {
                    universities.Add(_mapper.Map<UniversityDTO>(university));
                }

                return universities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<UniversityDTO> UpdateUniversity(UniversityDTO university)
        {
            try
            {
                return _mapper.Map<UniversityDTO>(await _repositoryManager.UniversitiesRepository
                    .UpdateUniversity(_mapper.Map<UniversityEntity>(university)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UniversityDTO>> GetAllUniversities()
        {
            try
            {
                List<UniversityEntity> universityEntityList = await _repositoryManager.UniversitiesRepository.GetAllUniversities();

                List<UniversityDTO> universityDomainList = new List<UniversityDTO>();
                universityEntityList.ForEach(x => universityDomainList.Add(_mapper.Map<UniversityDTO>(x)));

                return universityDomainList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
