using Domain;

namespace Services.Abstract
{
    public interface ITechnologiesService
    {
        Task<Technology> AddTechnology(Technology technology);
        Task<string> DeleteTechnologyById(int id);
        Task<Technology> GetTechnologyById(int id);
        Task<List<Technology>> GetTechnologiesBySearch(string search);
        Task<Technology> UpdateTechnology(Technology technology);
        Task<List<Technology>> GetAllTechnologies();
    }
}