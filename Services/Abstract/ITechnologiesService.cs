using Domain;

namespace Services.Abstract
{
    public interface ITechnologiesService
    {
        Task<List<TechnologyDTO>> AddTechnology(List<TechnologyDTO> technology);
        Task<int> DeleteTechnologyById(int id);
        Task<TechnologyDTO> GetTechnologyById(int id);
        Task<List<TechnologyDTO>> GetTechnologiesBySearch(string search);
        Task<TechnologyDTO> UpdateTechnology(TechnologyDTO technology);
        Task<List<TechnologyDTO>> GetAllTechnologies();
    }
}