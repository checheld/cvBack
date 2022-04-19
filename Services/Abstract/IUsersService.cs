using Domain;
using LeviossaCV.Services.Models;

namespace Services.Abstract
{
    public interface IUsersService
    {
        Task<User> AddUser(User user);
        Task<string> DeleteUserById(int id);
        Task<User> GetUserById(int id);
        Task<List<User>> GetUsersBySearch(string search);
        Task<User> UpdateUser(User user);
        Task<List<User>> GetAllUsers();
    }
}