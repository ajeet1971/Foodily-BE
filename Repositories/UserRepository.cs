using Foodily.Data;
using Foodily.Interface;
using Foodily.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Foodily.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext) { _dataContext = dataContext; }

        public async Task<List<UserVM>> GetUsersList()
        {
            var data = await _dataContext.UserMaster.ToListAsync();
            return data;
        }

        public async Task<UserVM> GetUserById(int id)
        {
            var data= await _dataContext.UserMaster.FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public async Task<bool> AddUserDetails(UserVM user)
        {
            var data = await _dataContext.UserMaster.AddAsync(user);
            _dataContext.SaveChanges();
            return true;
        }
        public async Task<UserVM> GetUser(string email, string password)
        {
            return await _dataContext.UserMaster.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
        public async Task<UserVM> GetUserByEmail(string email)
        {
            var data=  await _dataContext.UserMaster.FirstOrDefaultAsync(_ => _.Email == email);
            return data;
        }
    }
}
