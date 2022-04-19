using Domain;

namespace Services.Abstract
{
    public interface IProjectsService
    {
        Task<Project> AddProject(Project project);
        Task<string> DeleteProjectById(int id);
        Task<Project> GetProjectById(int id);
        Task<List<Project>> GetProjectsBySearch(SearchProjects searchProjects);
        Task<Project> UpdateProject(Project project);
        Task<List<Project>> GetAllProjects();
    }
}