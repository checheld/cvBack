using AutoMapper;
using Data.Repositories.Abstract;
using Domain;
using Entities;
using Mappers;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstract;

namespace Services
{
    public class UniversitiesService : IUniversitiesService
    {
        private readonly IUniversitiesRepository _universitiesRepository;
        private readonly IMapper _mapper;
        public UniversitiesService(IMapper mapper, IServiceProvider _serviceProvider)
        {
            _mapper = mapper;
            _universitiesRepository = _serviceProvider.GetService<IUniversitiesRepository>();
        }

        // autoMapper
        public class AppMappingUniversity : Profile
        {
            public AppMappingUniversity()
            {
                CreateMap<UniversityDTO, UniversityEntity>().ReverseMap();
            }
        }
        //
        
        public async Task<UniversityDTO> AddUniversity(UniversityDTO university)
        {
            try
            {
                /*UniversityEntity newUniversity = UniversityMapper.ToEntity(university);*/
                var newUniversity = _mapper.Map<UniversityEntity>(university);
                newUniversity.CreatedAt = DateTime.Now;
                UniversityEntity u = await _universitiesRepository.AddUniversity(newUniversity);
                if (u != null)
                {
                    /*UniversityDTO item = UniversityMapper.ToDomain(u);*/
                    var item = _mapper.Map<UniversityDTO>(u);
                    return item;
                };
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return null;
        }
        
        public async Task<string> DeleteUniversityById(int id)
        {
            try
            {
                return await _universitiesRepository.DeleteUniversityById(id);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        public async Task<UniversityDTO> GetUniversityById(int id)
        {
            try
            {
                /*return UniversityMapper.ToDomain(await _universitiesRepository.GetUniversityById(id));*/
                return _mapper.Map<UniversityDTO>(await _universitiesRepository.GetUniversityById(id));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public async Task<List<UniversityDTO>> GetUniversitiesBySearch(string search)
        {
            try
            {
                /*return UniversityMapper.ToDomainList(await _universitiesRepository.GetUniversitiesBySearch(search));*/

                var searchUniversities = await _universitiesRepository.GetUniversitiesBySearch(search);
                List<UniversityDTO> universities = new List<UniversityDTO>();
                foreach (UniversityEntity university in searchUniversities)
                {
                    universities.Add(_mapper.Map<UniversityDTO>(university));
                }
                return universities;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public async Task<UniversityDTO> UpdateUniversity(UniversityDTO university)
        {
            try
            {
                /*return UniversityMapper.ToDomain(await _universitiesRepository.UpdateUniversity(UniversityMapper.ToEntity(university)));*/
                return _mapper.Map<UniversityDTO>(await _universitiesRepository.UpdateUniversity(_mapper.Map<UniversityEntity>(university)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<UniversityDTO>> GetAllUniversities()
        {
            try
            {
                List<UniversityEntity> universityEntityList = await _universitiesRepository.GetAllUniversities();
                List<UniversityDTO> universityDomainList = new List<UniversityDTO>();
                /*universityEntityList.ForEach(x => universityDomainList.Add(UniversityMapper.ToDomain(x)));*/
                universityEntityList.ForEach(x => universityDomainList.Add(_mapper.Map<UniversityDTO>(x)));
                return universityDomainList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
