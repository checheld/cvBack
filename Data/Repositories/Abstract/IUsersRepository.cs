using Entities;

namespace Data.Repositories.Abstract
{
    public interface IUsersRepository
    {
        Task<UserEntity> AddUser(UserEntity user);
        Task<UserEntity> UpdateUser(UserEntity user);
        Task<UserEntity> GetUserById(int id);
        Task<List<UserEntity>> GetUsersBySearch(string search);
        Task<string> DeleteUserById(int id);
        Task<List<UserEntity>> GetAllUsers();

        Task RemoveAllEducations(int userId);
        Task RemoveAllWorkExperiences(int userId);
    }
}