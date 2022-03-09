
using Domain;
using Entities;

namespace Services.Abstract
{
    public interface IUniversitiesService
    {
        Task<University> AddUniversity(University university);
        Task<string> DeleteUniversityById(int id);
        Task<University> GetUniversityById(int id);
        Task<University> UpdateUniversity(University company);
        Task<List<University>> GetAllUniversities();
    }
}
