using Microsoft.VisualStudio.TestTools.UnitTesting;
using radical_tech_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace radical_unit_tests
{
    class UserRepositoryTests
    {
        private readonly AppDbContext _appDbContext;

        public UserRepositoryTests(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [TestMethod]
        public void CreateAndGetUserTest()
        {
            User user = new User
            {
                Name = "Test1",
                Email = "Test1@Test.com",
                PassWord = "Test123!"
            };

            _appDbContext.Users.Add(user);
            var nameCount = _appDbContext.Users.Where(o => o.Name == user.Name).ToList().Count;
            var emailCount = _appDbContext.Users.Where(o => o.Email == user.Email).ToList().Count;

            Assert.AreEqual(nameCount, 1);
            Assert.AreEqual(emailCount, 1);
        }

    }
}
