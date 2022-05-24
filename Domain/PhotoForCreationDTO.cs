using Domain.Abstract;
using Microsoft.AspNetCore.Http;

namespace Domain
{
    public class PhotoForCreationDTO
    {
        public string Url { get; set; }
        public string PublicId { get; set; }
    }

    public class PhotoCreate
    {
        public IFormFile File { get; set; }

    }
}