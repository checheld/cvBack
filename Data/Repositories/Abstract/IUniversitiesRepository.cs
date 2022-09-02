using Entities;

namespace Data.Repositories.Abstract
{
    public interface IUniversitiesRepository
    {
        Task<List<UniversityEntity>> AddUniversities(List<UniversityEntity> university);
        Task<UniversityEntity> UpdateUniversity(UniversityEntity university);
        Task<UniversityEntity> GetUniversityById(int id);
        Task<List<UniversityEntity>> GetUniversitiesBySearch(string search);
        Task<int> DeleteUniversityById(int id);
        Task<List<UniversityEntity>> GetAllUniversities();
    }
}
