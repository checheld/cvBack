
using Data.Entities;

namespace Data.Repositories.Abstract
{
    public interface IProjectPhotoRepository
    {
        Task<ProjectPhotoEntity> GetProjectPhotoById(int id);
        Task<string> DeleteProjectPhotoById(int id);
    }
}