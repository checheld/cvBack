
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
        public UniversitiesService(IServiceProvider _serviceProvider)
        {
            _universitiesRepository = _serviceProvider.GetService<IUniversitiesRepository>();
        }

        public async Task<University> AddUniversity(University university)
        {
            try
            {
                UniversityEntity newUniversity = UniversityMapper.ToEntity(university);
                UniversityEntity u = await _universitiesRepository.AddUniversity(newUniversity);
                if (u != null)
                {
                    University item = UniversityMapper.ToDomain(u);
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

        public async Task<University> GetUniversityById(int id)
        {
            try
            {
                return UniversityMapper.ToDomain(await _universitiesRepository.GetUniversityById(id));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<University>> GetUniversitiesBySearch(string search)
        {
            try
            {
                return UniversityMapper.ToDomainList(await _universitiesRepository.GetUniversitiesBySearch(search));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<University> UpdateUniversity(University university)
        {
            try
            {
                return UniversityMapper.ToDomain(await _universitiesRepository.UpdateUniversity(UniversityMapper.ToEntity(university)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<University>> GetAllUniversities()
        {
            try
            {
                List<UniversityEntity> universityEntityList = await _universitiesRepository.GetAllUniversities();
                List<University> universityDomainList = new List<University>();
                universityEntityList.ForEach(x => universityDomainList.Add(UniversityMapper.ToDomain(x)));
                return universityDomainList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
