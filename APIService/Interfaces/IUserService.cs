using APIService.Models;

namespace APIService.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByName(string username);
        List<User> GetUsers();
        User GetUser(string id);
        Task<User> CreateUser(User user);
        string Authenticate(string name, string password);
    }
}
