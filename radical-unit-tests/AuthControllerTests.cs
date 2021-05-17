using Microsoft.VisualStudio.TestTools.UnitTesting;
using radical_tech_test.Interfaces;
using radical_tech_test.Models;

namespace radical_unit_tests
{
    [TestClass]
    public class AuthControllerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IJSONWebTokenService _jsonWebTokenService;

        public AuthControllerTests(IUserRepository userRepository, IJSONWebTokenService jsonWebTokenService)
        {
            _userRepository = userRepository;
            _jsonWebTokenService = jsonWebTokenService;
        }

        [TestMethod]
        public void CreateUserTest()
        {
            User user = new User
            {
                Name = "Test",
                Email = "Test@Test.com",
                PassWord = "Test123"
            };

            User toReturn = _userRepository.CreateUser(user);

            Assert.AreEqual(user.Name, toReturn.Name);
            Assert.AreEqual(user.Email, toReturn.Email);
        }

        [TestMethod]
        public void Login()
        {
            User user = _userRepository.GetUserByEmail("Test@Test.com");

            var jwt = _jsonWebTokenService.CreateToken(user.Id);

            Assert.IsNotNull(jwt);
        }

        [TestMethod]
        public void GetUser()
        {
            User user = _userRepository.GetUserByEmail("Test@Test.com");

            var jwt = _jsonWebTokenService.CreateToken(user.Id);

            int userId = user.Id;

            User currentUser = _userRepository.GetUserById(userId);

            Assert.IsNotNull(jwt);
            Assert.IsNotNull(currentUser);
        }
    }
}
