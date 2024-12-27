using Foodily.ViewModels;

namespace Foodily.Interface
{
    public interface IUserRepository
    {
        Task<List<UserVM>> GetUsersList();
        Task<UserVM> GetUserById(int id);
        Task<UserVM> GetUser(string username, string password);
        Task<bool> AddUserDetails(UserVM users);
        Task<UserVM> GetUserByEmail(string email);
    }
}
  