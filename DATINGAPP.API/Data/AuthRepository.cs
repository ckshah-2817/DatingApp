using System;
using System.Linq;
using System.Threading.Tasks;
using DATINGAPP.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DATINGAPP.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dC;

        public AuthRepository(DataContext DC)
        {
            _dC = DC;
        }
        public async Task<User> Login(string username, string password)
        {
            var user= await  _dC.Users.FirstOrDefaultAsync(x=>x.Username == username);

             if (user == null)
             return null;

             if (!validateUser(password,user))
             return null;

 
            return user;
        }

        private bool validateUser(string password, User user)
        {
          using( var hvac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt))
          {
            var passwordNewHash=hvac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
             for (int i=0 ; i< passwordNewHash.Length;i++){
                 if (passwordNewHash[i] == user.PasswordHash[i] ) return true;
             }

          }
          return false;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash,passwordSalt;
            
            createUser(password,out passwordHash,out passwordSalt);

            user.PasswordHash=passwordHash;
            user.PasswordSalt=passwordSalt;

            await _dC.Users.AddAsync(user);
            await _dC.SaveChangesAsync();
            return user;
        }

        private void createUser(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
          using( var hvac = new System.Security.Cryptography.HMACSHA512())
          {
            passwordHash=hvac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            passwordSalt = hvac.Key;

          }
        }

        public async Task<bool> UserExist(string username)
        {
            if (await _dC.Users.AnyAsync(x=>x.Username==username)) return true;

            return false;
        }
    }
}