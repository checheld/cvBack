using Data.Repositories.Abstract;

namespace Data.Repositories.Utility.Interface
{
    public interface IRepositoryManager
    {
        ICompaniesRepository CompaniesRepository { get; }
        ICVsRepository CVsRepository { get; }
        IProjectPhotoRepository ProjectPhotoRepository { get; }
        IProfilePhotoRepository ProfilePhotoRepository { get; }
        IProjectsRepository ProjectsRepository { get; }
        ITechnologiesRepository TechnologiesRepository { get; }
        IUniversitiesRepository UniversitiesRepository { get; }
        IUsersRepository UsersRepository { get; }
        IProjectTypesRepository ProjectTypesRepository { get; }
    }
}