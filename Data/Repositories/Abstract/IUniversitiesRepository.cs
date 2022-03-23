
using Entities;

namespace Data.Repositories.Abstract
{
    public interface IUniversitiesRepository
    {
        Task<UniversityEntity> AddUniversity(UniversityEntity university);
        Task<UniversityEntity> UpdateUniversity(UniversityEntity university);
        Task<UniversityEntity> GetUniversityById(int id);
        Task<List<UniversityEntity>> GetUniversitiesBySearch(string search);
        Task<string> DeleteUniversityById(int id);
        Task<List<UniversityEntity>> GetAllUniversities();
    }
}
