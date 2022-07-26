using Data.Repositories.Abstract;
using Data.Repositories.Utility.Interface;

namespace Data.Repositories.Utility
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        #region Logic
        private readonly Lazy<ICompaniesRepository> _lazyCompaniesRepository;
        private readonly Lazy<ICVsRepository> _lazyCVsRepository;
        private readonly Lazy<IProjectPhotoRepository> _lazyProjectPhotoRepository;
        private readonly Lazy<IProfilePhotoRepository> _lazyProfilePhotoRepository;
        private readonly Lazy<IProjectsRepository> _lazyProjectsRepository;
        private readonly Lazy<ITechnologiesRepository> _lazyTechnologiesRepository;
        private readonly Lazy<IUniversitiesRepository> _lazyUniversitiesRepository;
        private readonly Lazy<IUsersRepository> _lazyUsersRepository;
        private readonly Lazy<IProjectTypesRepository> _lazyProjectTypesRepository;
        
        #endregion
        public RepositoryManager(IServiceProvider serviceManager)
        {
            #region Logic
            _lazyCompaniesRepository = new Lazy<ICompaniesRepository>(() => new CompaniesRepository(serviceManager));
            _lazyCVsRepository = new Lazy<ICVsRepository>(() => new CVsRepository(serviceManager));
            _lazyProjectPhotoRepository = new Lazy<IProjectPhotoRepository>(() => new ProjectPhotoRepository(serviceManager));
            _lazyProfilePhotoRepository = new Lazy<IProfilePhotoRepository>(() => new ProfilePhotoRepository(serviceManager));
            _lazyProjectsRepository = new Lazy<IProjectsRepository>(() => new ProjectsRepository(serviceManager));
            _lazyTechnologiesRepository = new Lazy<ITechnologiesRepository>(() => new TechnologiesRepository(serviceManager));
            _lazyUniversitiesRepository = new Lazy<IUniversitiesRepository>(() => new UniversitiesRepository(serviceManager));
            _lazyUsersRepository = new Lazy<IUsersRepository>(() => new UsersRepository(serviceManager));
            _lazyProjectTypesRepository = new Lazy<IProjectTypesRepository>(() => new ProjectTypesRepository(serviceManager));
            #endregion
        }
        #region Resolve Lazy
        public ICompaniesRepository CompaniesRepository => _lazyCompaniesRepository.Value;
        public ICVsRepository CVsRepository => _lazyCVsRepository.Value;
        public IProjectPhotoRepository ProjectPhotoRepository => _lazyProjectPhotoRepository.Value;
        public IProfilePhotoRepository ProfilePhotoRepository => _lazyProfilePhotoRepository.Value;
        public IProjectsRepository ProjectsRepository => _lazyProjectsRepository.Value;
        public ITechnologiesRepository TechnologiesRepository => _lazyTechnologiesRepository.Value;
        public IUniversitiesRepository UniversitiesRepository => _lazyUniversitiesRepository.Value;
        public IUsersRepository UsersRepository => _lazyUsersRepository.Value;
        public IProjectTypesRepository ProjectTypesRepository => _lazyProjectTypesRepository.Value;
        #endregion
    }
}