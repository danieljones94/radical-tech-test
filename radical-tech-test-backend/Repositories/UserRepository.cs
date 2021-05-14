using radical_tech_test.Interfaces;
using radical_tech_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace radical_tech_test.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public User CreateUser(User user)
        {
            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();
            return user;
        }

        public User GetUserByEmail(string email)
        {
            return _appDbContext.Users.FirstOrDefault(o => o.Email == email);
        }

        public User GetUserById(int id)
        {
            return _appDbContext.Users.FirstOrDefault(o => o.Id == id);
        }
    }
}
