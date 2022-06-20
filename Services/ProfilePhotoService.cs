using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Services.Abstract;

namespace Services
{
    public class ProfilePhotoService : IProfilePhotoService
    {
        private readonly IUsersService _usersService;
        public IConfiguration Configuration { get; }
        private Account CloudinaryAccount { get; }
        private Cloudinary _cloudinary;

        public ProfilePhotoService( IServiceProvider _serviceProvider, IConfiguration configuration)
        {
            
            Configuration = configuration;

            //_cloudinarySettings = (CloudinarySettingsDTO?)Configuration.GetSection("CloudinarySettings");
            CloudinaryAccount = new Account(Configuration.GetSection("CloudinarySettings")["CloudName"],
                 Configuration.GetSection("CloudinarySettings")["ApiKey"],
                 Configuration.GetSection("CloudinarySettings")["ApiSecret"]);
        }

        public async Task<string> AddProfilePhoto(IFormFile image)
        {
            try
            {
                var uploadResult = new ImageUploadResult();
                
                using (var stream = image.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.Name, stream)
                    };


                    uploadResult = new Cloudinary(CloudinaryAccount).Upload(uploadParams);
                }

                return uploadResult.SecureUrl.AbsoluteUri;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}