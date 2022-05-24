using Domain;
using Microsoft.AspNetCore.Http;

namespace Services.Abstract
{
    public interface IProfilePhotoService
    {
        Task<string> AddProfilePhoto(IFormFile image);
    }
}