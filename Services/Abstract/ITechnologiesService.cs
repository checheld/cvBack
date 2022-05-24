using Domain;

namespace Services.Abstract
{
    public interface ITechnologiesService
    {
        Task<TechnologyDTO> AddTechnology(TechnologyDTO technology);
        Task<string> DeleteTechnologyById(int id);
        Task<TechnologyDTO> GetTechnologyById(int id);
        Task<List<TechnologyDTO>> GetTechnologiesBySearch(string search);
        Task<TechnologyDTO> UpdateTechnology(TechnologyDTO technology);
        Task<List<TechnologyDTO>> GetAllTechnologies();
    }
}