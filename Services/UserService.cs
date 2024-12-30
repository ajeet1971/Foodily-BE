using Foodily.Interface;
using Foodily.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Foodily.Services
{
    public class UserService : IUserService
    {
        private IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }
        public async Task<List<UserVM>> GetUserListAsync()
        {
            try
            {
                var data = await _userRepository.GetUsersList();
                if (data == null || data.Count == 0)
                {
                    return null;
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
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
                var data = await _userRepository.GetUserById(id);
                if (data == null)
                {
                    return null; 
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public bool ValidatePassword(string password)
        {
            if (password.Length < 8)
                return false;

            //at least one lowercase letter
            if (!password.Any(char.IsLower))
                return false;

            //at least one uppercase letter
            if (!password.Any(char.IsUpper))
                return false;

            //at least one digit
            if (!password.Any(char.IsDigit))
                return false;

            //at least one special character
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                return false;

            return true;
        }

        private bool IsValidEmail(string email)
        {
            var emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailRegex);
        }

        public async Task<int> AddUserDetails(UserVM users)
        {
            try 
            {
                if (users == null)
                {
                    return 0;
                }
                var existingUser = await _userRepository.GetUserByEmail(users.Email);
                if (existingUser != null)
                {
                    return -1; 
                }
                if (!ValidatePassword(users.Password))
                {
                    return -2;
                }
                if (!IsValidEmail(users.Email))
                {
                    return -3; 
                }
                UserVM userDetails = new()
                {
                    Id=users.Id,
                    UserName = users.UserName,
                    Email = users.Email,
                    Password = users.Password,  
                    Bio=users.Bio,
                };

                string encryptedPassword = EncryptPassword(users.Password);
                userDetails.Password = encryptedPassword;
                var userdata = await _userRepository.AddUserDetails(userDetails);
                return userdata ? 1 : 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public static string EncryptPassword(string password)
        {
            var key = Encoding.UTF8.GetBytes("E546C8DF278CD5931069B522E695D4F2");

            using (var aesAlg = Aes.Create())
            {
                aesAlg.GenerateIV();
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(password);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }
        public static string DecryptedPassword(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);
            var key = Encoding.UTF8.GetBytes("E546C8DF278CD5931069B522E695D4F2");

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }    
        public async Task<UserVM> GetUser(string email, string password)
        {
            var data = await _userRepository.GetUserByEmail(email);
            var decryptedPassword = DecryptedPassword(data.Password);
            if (decryptedPassword == password)
            {
                return data;
            }
            return null;
        }   
        public string GenerateToken(UserVM user)
        {

            var claims = new[]
            {
                new Claim("useName", user.UserName),
                new Claim("email", user.Email),
                new Claim("id", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
