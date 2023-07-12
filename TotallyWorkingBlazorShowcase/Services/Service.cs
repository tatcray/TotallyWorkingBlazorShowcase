using System.Security.Cryptography;
using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using TotallyWorkingBlazorShowcase.Services;
using TotallyWorkingBlazorShowcase.Shared.Models;

namespace TotallyWorkingBlazorShowcase.Services
{
    public class UserService
    {
        private const int saltSize = 16;


        private readonly AppDbContext _context;

        public UserService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<ServisResponse> SaveUser(RegisterModel user)
        {
            byte[] salt = new byte[saltSize];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with a random value.
                rngCsp.GetBytes(salt);
            }
            var userDb = new UserDb();
            userDb.Id = Guid.NewGuid().ToString();
            userDb.UserName = user.UserName;
            userDb.Password = user.Password;
            userDb.Salt = Convert.ToBase64String(salt);
            var password = System.Text.Encoding.UTF8.GetBytes(userDb.Password);
            var argon2 = new Argon2d(password);
            
            argon2.DegreeOfParallelism = 16;
            argon2.MemorySize = 8192;
            argon2.Iterations = 40;

            argon2.Salt = salt;
            var hash = argon2.GetBytes(128);

            userDb.Password = Convert.ToBase64String(hash);
            
            _context.Users.Add(userDb);
            var insertedRowsCount = await _context.SaveChangesAsync();
            
            if (insertedRowsCount > 0)
            {
                return new ServisResponse()
                {
                    StatusCode = 200
                };
            }

            return new ServisResponse()
            {
                StatusCode = 400
            };
        }

        public async Task<ServisResponse> UserLogin(LoginInputModel inputModel)
        {
            var UserName = inputModel.UserName;

            var users = _context.Users.Where(user => user.UserName.Equals(UserName));
            /*return users.Any();*/
            var usersalt = users.First().Salt;
                byte[] salt = Convert.FromBase64String(usersalt);
                
                var password = System.Text.Encoding.UTF8.GetBytes(inputModel.Password);
                var argon2 = new Argon2d(password);
            
                argon2.DegreeOfParallelism = 16;
                argon2.MemorySize = 8192;
                argon2.Iterations = 40;

                argon2.Salt = salt;
                var hash = argon2.GetBytes(128);

                
                string hashPassword = Convert.ToBase64String(hash);

                UserDb userDb = users.First();
                
                if (userDb.Password.Equals(hashPassword))
                {
                    return new ServisResponse()
                    {
                        StatusCode = 200
                    };
                }

                return new ServisResponse()
                {
                    StatusCode = 400
                };
        }
    }
}