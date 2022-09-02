using Entities;

namespace Data.Repositories.Abstract
{
    public interface IUsersRepository
    {
        Task<UserEntity> AddUser(UserEntity user);
        Task<UserEntity> UpdateUser(UserEntity user);
        Task<UserEntity> GetUserById(int id);
        Task<List<UserEntity>> GetUsersBySearch(string search);
        Task<int> DeleteUserById(int id);
        Task<List<UserEntity>> GetAllUsers();

        Task AddUserTechnology(List<UserTechnologyEntity> userTechnology);
        Task AddEducations(List<EducationEntity> education);
        Task AddWorkExperiences(List<WorkExperienceEntity> workExperience);
        Task<EducationEntity> GetEducationById(int id);
        Task<WorkExperienceEntity> GetWorkExperienceById(int id);
        Task<EducationEntity> UpdateEducation(EducationEntity education);
        Task<WorkExperienceEntity> UpdateWorkExperience(WorkExperienceEntity workExperience);
        Task DeleteEducation(int educationId);
        Task DeleteWorkExperience(int workExpId);
        Task RemoveAllEducations(int userId);
        Task RemoveAllWorkExperiences(int userId);
    }
}