using Services.Abstract;

namespace Services.Utility.Interface
{
    public interface IServiceManager
    {
        ICompaniesService CompaniesService { get; }
        ICVsService CVsService { get; }
        IProfilePhotoService ProfilePhotoService { get; }
        IProjectPhotoService ProjectPhotoService { get; }
        IProjectsService ProjectsService { get; }
        ITechnologiesService TechnologiesService { get; }
        IUniversitiesService UniversitiesService { get; }
        IUsersService UsersService { get; }
    }
}
