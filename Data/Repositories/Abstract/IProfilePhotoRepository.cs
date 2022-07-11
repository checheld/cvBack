using Data.Entities;

namespace Data.Repositories.Abstract
{
    public interface IProfilePhotoRepository
    {
        Task<PhotoParamsEntity> AddPhotoParams(PhotoParamsEntity photoParams);
        Task<PhotoParamsEntity> GetPhotoParamsById(int id);
        Task<PhotoParamsEntity> UpdatePhotoParams(PhotoParamsEntity photoParams);
    }
}