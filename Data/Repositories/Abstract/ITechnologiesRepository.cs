using Entities;

namespace Data.Repositories.Abstract
{
    public interface ITechnologiesRepository
    {
        Task<TechnologyEntity> AddTechnology(TechnologyEntity technology);
        Task<TechnologyEntity> UpdateTechnology(TechnologyEntity technology);
        Task<TechnologyEntity> GetTechnologyById(int id);
        Task<List<TechnologyEntity>> GetTechnologiesBySearch(string search);
        Task<string> DeleteTechnologyById(int id);
        Task<List<TechnologyEntity>> GetAllTechnologies();
    }
}
