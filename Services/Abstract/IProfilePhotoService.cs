using Domain;
using Microsoft.AspNetCore.Http;

namespace Services.Abstract
{
    public interface IProfilePhotoService
    {
        Task<string> AddProfilePhoto(IFormFile image);
        Task<PhotoParamsDTO> AddPhotoParams(PhotoParamsDTO photoParams);
        Task<PhotoParamsDTO> UpdatePhotoParams(PhotoParamsDTO photoParams);
        Task<PhotoParamsDTO> GetPhotoParamsById(int id);
    }
}