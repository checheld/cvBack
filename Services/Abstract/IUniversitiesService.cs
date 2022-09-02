using Domain;

namespace Services.Abstract
{
    public interface IUniversitiesService
    {
        Task<List<UniversityDTO>> AddUniversities(List<UniversityDTO> university);
        Task<int> DeleteUniversityById(int id);
        Task<UniversityDTO> GetUniversityById(int id);
        Task<List<UniversityDTO>> GetUniversitiesBySearch(string search);
        Task<UniversityDTO> UpdateUniversity(UniversityDTO university);
        Task<List<UniversityDTO>> GetAllUniversities();
    }
}
