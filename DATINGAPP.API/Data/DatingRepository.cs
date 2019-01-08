using System.Collections.Generic;
using System.Threading.Tasks;
using DATINGAPP.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DATINGAPP.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _dc;

        public DatingRepository(DataContext dc)
        {
            
            _dc = dc;
        }
        public void Add<T>(T entity) where T : class
        {
            _dc.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _dc.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
           var user= await _dc.Users.Include(x=>x.Photos).FirstOrDefaultAsync(y=>y.Id== id);

           return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users= await _dc.Users.Include(x=>x.Photos).ToListAsync();

           return users;
        }

        public async Task<bool> SaveAll()
        {
           return await _dc.SaveChangesAsync() > 0;
        }
    }
}