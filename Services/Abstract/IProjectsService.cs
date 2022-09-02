using Domain;

namespace Services.Abstract
{
    public interface IProjectsService
    {
        Task<ProjectDTO> AddProject(ProjectDTO project);
        Task<int> DeleteProjectById(int id);
        Task<ProjectDTO> GetProjectById(int id);
        Task<List<ProjectDTO>> GetProjectsBySearch(SearchProjectsDTO searchProjects);
        Task<ProjectDTO> UpdateProject(ProjectDTO project);
        Task<List<ProjectDTO>> GetAllProjects();
    }
}