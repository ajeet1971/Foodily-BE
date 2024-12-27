using Foodily.ViewModels;

namespace Foodily.Interface
{
    public interface IUserService
    {
        Task<List<UserVM>> GetUserListAsync();
        Task<UserVM> GetUserById(int id);
        Task<int> AddUserDetails(UserVM users);
        string GenerateToken(UserVM user);
        Task<UserVM> GetUser(string email, string password);
    }
}
