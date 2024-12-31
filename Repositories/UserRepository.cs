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
            try
            {
                var data = await _dataContext.UserMaster.ToListAsync();
                if (data == null || data.Count == 0)
                {
                    return new List<UserVM>(); 
                }
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
               
        public async Task<UserVM> GetUserById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("Invalid user ID.");
                }
                var data = await _dataContext.UserMaster.FirstOrDefaultAsync(x => x.Id == id);
                if (data == null)
                {
                    throw new KeyNotFoundException($"User with ID {id} not found.");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> AddUserDetails(UserVM user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "User details cannot be null.");
                }

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    throw new ArgumentException("User email cannot be empty.");
                }
                var existingUser = await _dataContext.UserMaster.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (existingUser != null)
                {
                    throw new InvalidOperationException("A user with this email already exists.");
                }
                var data = await _dataContext.UserMaster.AddAsync(user);
                _dataContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<UserVM> GetUser(string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentException("Email and password must be provided.");
                }
                var data = await _dataContext.UserMaster.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
                if (data == null)
                {
                    throw new UnauthorizedAccessException("Invalid email or password.");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<UserVM> GetUserByEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException("Email must be provided.");
                }
                var data = await _dataContext.UserMaster.FirstOrDefaultAsync(_ => _.Email == email);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
