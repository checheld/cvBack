using Domain;
using Microsoft.AspNetCore.Http;

namespace Services.Abstract
{
    public interface IProjectPhotoService
    {
        Task<string> AddProjectPhoto(IFormFile image);
        Task DeleteProjectPhotoById(int id);
        
    }
}