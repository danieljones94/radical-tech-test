using radical_tech_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace radical_tech_test.Interfaces
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        User GetUserByEmail(string email);
        User GetUserById(int id);
    }
}
