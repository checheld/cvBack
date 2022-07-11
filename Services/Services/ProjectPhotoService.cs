#region Imports
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Data.Entities;
using Data.Repositories.Utility.Interface;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Services.Abstract;
#endregion

namespace Services
{
    public class ProjectPhotoService : IProjectPhotoService
    {
        #region Logic
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public IConfiguration Configuration { get; }
        private Account CloudinaryAccount { get; }
        private Cloudinary _cloudinary;

        public ProjectPhotoService(IMapper mapper, IRepositoryManager repositoryManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
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
        #endregion

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

        public async Task DeleteProjectPhotoById(int id)
        {
            try
            {
                var pp = _mapper.Map<ProjectPhotoDTO>(await _repositoryManager.ProjectPhotoRepository.GetProjectPhotoById(id));
                var prodId1 = pp.Url.Split("upload/")[1];
                var prodId2 = prodId1.Split("/")[1];
                var prodId3 = prodId2.Split(".")[0];
                new Cloudinary(CloudinaryAccount).DeleteResourcesAsync(prodId3);
 
                await _repositoryManager.ProjectPhotoRepository.DeleteProjectPhotoById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}