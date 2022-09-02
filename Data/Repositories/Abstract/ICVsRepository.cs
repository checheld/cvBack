using Entities;

namespace Data.Repositories.Abstract
{
    public interface ICVsRepository
    {
        Task<CVEntity> AddCV(CVEntity cv);
        Task<CVEntity> UpdateCV(CVEntity cv);
        Task<CVEntity> GetCVById(int id);
        Task<List<CVEntity>> GetCVsBySearch(string search);
        Task<int> DeleteCVById(int id);
        Task<List<CVEntity>> GetAllCVs();

        Task AddProjectCVs(List<ProjectCVEntity> projectCV);
        Task<ProjectCVEntity> GetProjectCVById(int id);
        Task<ProjectCVEntity> UpdateProjectCV(ProjectCVEntity projectCV);
        Task DeleteProjectCV(int projectCVId);
        Task RemoveAllProjectCVs(int userId);
    }
}