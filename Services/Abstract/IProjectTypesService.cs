using Services.Domain;

namespace Services.Abstract
{
    public interface IProjectTypesService
    {
        Task<List<ProjectTypeDTO>> AddProjectTypes(List<ProjectTypeDTO> projectType);
        Task DeleteProjectTypeById(int id);
        Task<ProjectTypeDTO> GetProjectTypeById(int id);
        Task<List<ProjectTypeDTO>> GetProjectTypesBySearch(string search);
        Task<ProjectTypeDTO> UpdateProjectType(ProjectTypeDTO projectType);
        Task<List<ProjectTypeDTO>> GetAllProjectTypes();
    }
}