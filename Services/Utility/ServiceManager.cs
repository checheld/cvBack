#region Imports
using AutoMapper;
using Data.Repositories.Utility.Interface;
using Services.Abstract;
using Services.Utility.Interface;
using Microsoft.Extensions.Configuration;
using CloudinaryDotNet;
#endregion

namespace Services.Utility
{
    public sealed class ServiceManager : IServiceManager
    {
        #region Logic
        private readonly Lazy<ICompaniesService> _lazyCompaniesService;
        private readonly Lazy<ICVsService> _lazyCVsService;
        private readonly Lazy<IProfilePhotoService> _lazyProfilePhotoService;
        private readonly Lazy<IProjectPhotoService> _lazyProjectPhotoService;
        private readonly Lazy<IProjectsService> _lazyProjectsService;
        private readonly Lazy<ITechnologiesService> _lazyTechnologiesService;
        private readonly Lazy<IUniversitiesService> _lazyUniversitiesService;
        private readonly Lazy<IUsersService> _lazyUsersService;
        #endregion

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IConfiguration configuration, Account account)
        {
            #region Logic
            _lazyCompaniesService = new Lazy<ICompaniesService>(() => new CompaniesService(mapper, repositoryManager));
            _lazyCVsService = new Lazy<ICVsService>(() => new CVsService(mapper, repositoryManager));
            _lazyProfilePhotoService = new Lazy<IProfilePhotoService>(() => new ProfilePhotoService(mapper, repositoryManager, account));
            _lazyProjectPhotoService = new Lazy<IProjectPhotoService>(() => new ProjectPhotoService(mapper, repositoryManager, account));
            _lazyProjectsService = new Lazy<IProjectsService>(() => new ProjectsService(mapper, repositoryManager, configuration));
            _lazyTechnologiesService = new Lazy<ITechnologiesService>(() => new TechnologiesService(mapper, repositoryManager));
            _lazyUniversitiesService = new Lazy<IUniversitiesService>(() => new UniversitiesService(mapper, repositoryManager));
            _lazyUsersService = new Lazy<IUsersService>(() => new UsersService(mapper, repositoryManager));
            #endregion
        }

        #region Resolve Lazy
        public ICompaniesService CompaniesService => _lazyCompaniesService.Value;
        public ICVsService CVsService => _lazyCVsService.Value;
        public IProfilePhotoService ProfilePhotoService => _lazyProfilePhotoService.Value;
        public IProjectPhotoService ProjectPhotoService => _lazyProjectPhotoService.Value;
        public IProjectsService ProjectsService => _lazyProjectsService.Value;
        public ITechnologiesService TechnologiesService => _lazyTechnologiesService.Value;
        public IUniversitiesService UniversitiesService => _lazyUniversitiesService.Value;
        public IUsersService UsersService => _lazyUsersService.Value;
        #endregion
    }
}
