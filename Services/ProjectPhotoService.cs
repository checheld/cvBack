using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Data.Entities;
using Data.Repositories.Abstract;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstract;

namespace Services
{
    public class ProjectPhotoService : IProjectPhotoService
    {
        private readonly IProjectPhotoRepository _projectPhotoRepository;
        private readonly IMapper _mapper;
        public IConfiguration Configuration { get; }
        private Account CloudinaryAccount { get; }
        private Cloudinary _cloudinary;

        public ProjectPhotoService(IMapper mapper, IServiceProvider _serviceProvider, IConfiguration configuration)
        {
            _mapper = mapper;
            _projectPhotoRepository = _serviceProvider.GetService<IProjectPhotoRepository>();
            Configuration = configuration;

            CloudinaryAccount = new Account(Configuration.GetSection("CloudinarySettings")["CloudName"],
                 Configuration.GetSection("CloudinarySettings")["ApiKey"],
                 Configuration.GetSection("CloudinarySettings")["ApiSecret"]);
        }
        public class AppMappingProjectPhoto : Profile
        {
            public AppMappingProjectPhoto()
            {
                CreateMap<ProjectPhotoDTO, ProjectPhotoEntity>().ReverseMap();
            }
        }
        public async Task<string> AddProjectPhoto(IFormFile image)
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

        public async Task<string> DeleteProjectPhotoById(int id)
        {
            try
            {
                var pp = _mapper.Map<ProjectPhotoDTO>(await _projectPhotoRepository.GetProjectPhotoById(id));
                var prodId1 = pp.Url.Split("upload/")[1];
                var prodId2 = prodId1.Split("/")[1];
                var prodId3 = prodId2.Split(".")[0];
                new Cloudinary(CloudinaryAccount).DeleteResourcesAsync(prodId3);
 
                return await _projectPhotoRepository.DeleteProjectPhotoById(id);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}