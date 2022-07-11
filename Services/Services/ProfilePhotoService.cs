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
    public class ProfilePhotoService : IProfilePhotoService
    {
        #region Logic
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        public IConfiguration Configuration { get; }
        private Account CloudinaryAccount { get; }
        private Cloudinary _cloudinary;

        public ProfilePhotoService(IMapper mapper, IRepositoryManager repositoryManager, IConfiguration configuration)
        {
            _repositoryManager = repositoryManager;
            Configuration = configuration;
            _mapper = mapper;

            CloudinaryAccount = new Account(Configuration.GetSection("CloudinarySettings")["CloudName"],
                 Configuration.GetSection("CloudinarySettings")["ApiKey"],
                 Configuration.GetSection("CloudinarySettings")["ApiSecret"]);
        }

        public class AppMappingPhotoParams : Profile
        {
            public AppMappingPhotoParams()
            {
                CreateMap<PhotoParamsDTO, PhotoParamsEntity>().ReverseMap();
            }
        }
        #endregion

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

        public async Task<PhotoParamsDTO> AddPhotoParams(PhotoParamsDTO photoParams)
        {
            try
            {
                var newPhotoParams = _mapper.Map<PhotoParamsEntity>(photoParams);
                newPhotoParams.CreatedAt = DateTime.Now;

                var pp = await _repositoryManager.ProfilePhotoRepository.AddPhotoParams(newPhotoParams);
              
                var item = _mapper.Map<PhotoParamsDTO>(pp);

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PhotoParamsDTO> UpdatePhotoParams(PhotoParamsDTO photoParams)
        {
            try
            {
                var pp = await _repositoryManager.ProfilePhotoRepository.UpdatePhotoParams(_mapper.Map<PhotoParamsEntity>(photoParams));
              
                return _mapper.Map<PhotoParamsDTO>(pp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PhotoParamsDTO> GetPhotoParamsById(int id)
        {
            try
            {
                var photoParams = await _repositoryManager.ProfilePhotoRepository.GetPhotoParamsById(id);

                return _mapper.Map<PhotoParamsDTO>(photoParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}