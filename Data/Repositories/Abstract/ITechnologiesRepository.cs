using Entities;

namespace Data.Repositories.Abstract
{
    public interface ITechnologiesRepository
    {
        Task<List<TechnologyEntity>> AddTechnology(List<TechnologyEntity> technology);
        Task<TechnologyEntity> UpdateTechnology(TechnologyEntity technology);
        Task<TechnologyEntity> GetTechnologyById(int id);
        Task<List<TechnologyEntity>> GetTechnologiesBySearch(string search);
        Task<int> DeleteTechnologyById(int id);
        Task<List<TechnologyEntity>> GetAllTechnologies();
    }
}
