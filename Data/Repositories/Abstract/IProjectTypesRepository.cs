using Data.Entities;
using Entities;

namespace Data.Repositories.Abstract
{
    public interface IProjectTypesRepository
    {
        Task<List<ProjectTypeEntity>> AddProjectTypes(List<ProjectTypeEntity> projectType);
        Task<ProjectTypeEntity> UpdateProjectType(ProjectTypeEntity projectType);
        Task<ProjectTypeEntity> GetProjectTypeById(int id);
        Task<List<ProjectTypeEntity>> GetProjectTypesBySearch(string search);
        Task DeleteProjectTypeById(int id);
        Task<List<ProjectTypeEntity>> GetAllProjectTypes();
    }
}