using Data.Entities;

namespace Data.Repositories.Abstract
{
    public interface IProjectPhotoRepository
    {
        Task<ProjectPhotoEntity> GetProjectPhotoById(int id);
        Task DeleteProjectPhotoById(int id);
    }
}