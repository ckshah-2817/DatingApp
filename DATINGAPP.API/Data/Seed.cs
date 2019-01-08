using DATINGAPP.API.Models;
using System.Collections.Generic;

namespace DATINGAPP.API.Data
{
    public class Seed
    {
        private readonly DataContext _dc;
      
        public Seed(DataContext dc)
        {
           _dc= dc;
           
            
        }
        public void SeedFeed(){
            var UserData = System.IO.File.ReadAllText("Data/UserSeedData.json");
            var ObjectDecode = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(UserData);

            foreach (var userItem in ObjectDecode)
            {
                byte[] passwordHash,passwordSalt;
                createUser("Password",out passwordHash,out passwordSalt);
                userItem.Username = userItem.Username.ToLower();
                userItem.PasswordHash=passwordHash;
                userItem.PasswordSalt=passwordSalt;

               this._dc.Users.Add(userItem);
            
            }
             this._dc.SaveChanges();
        }
        private void createUser(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
          using( var hvac = new System.Security.Cryptography.HMACSHA512())
          {
            passwordHash=hvac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            passwordSalt = hvac.Key;

          }
        }
    }
}