using Domain;

namespace Services.Abstract
{
    public interface IUniversitiesService
    {
        Task<University> AddUniversity(University university);
        Task<string> DeleteUniversityById(int id);
        Task<University> GetUniversityById(int id);
        Task<List<University>> GetUniversitiesBySearch(string search);
        Task<University> UpdateUniversity(University university);
        Task<List<University>> GetAllUniversities();
    }
}
