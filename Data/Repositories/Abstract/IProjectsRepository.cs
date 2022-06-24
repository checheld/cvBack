using Entities;

namespace Data.Repositories.Abstract
{
    public interface IProjectsRepository
    {
        Task<ProjectEntity> AddProject(ProjectEntity project);
        Task<ProjectEntity> UpdateProject(ProjectEntity project);
        Task<ProjectEntity> GetProjectById(int id);
        Task<List<ProjectEntity>> GetProjectsBySearch(SearchProjectsEntity searchProjectsEntity);
        Task<string> DeleteProjectById(int id);
        Task RemoveAllProjectPhotos(int projectId);
        Task<List<ProjectEntity>> GetAllProjects();
    }
}