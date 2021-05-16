using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using radical_tech_test.Interfaces;
using radical_tech_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace radical_tech_test.Controllers
{
    [Route(template:"api")]
    [ApiController]

    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IJSONWebTokenService _jsonWebTokenService;
        public AuthController(IUserRepository userRepository, IJSONWebTokenService jsonWebTokenService)
        {
            _userRepository = userRepository;
            _jsonWebTokenService = jsonWebTokenService;
        }
        
        [HttpPost(template:"signup")]
        public IActionResult SignUp(User signUpObj)
        {
            User user = new User()
            {
                Name = signUpObj.Name,
                Email = signUpObj.Email,
                PassWord = BCrypt.Net.BCrypt.HashPassword(signUpObj.PassWord)
            };

            User toReturn = _userRepository.CreateUser(user);

            return Ok(toReturn);
        }
        
        [HttpPost(template:"login")]
        public IActionResult Login(User loginObj)
        {
            User user = _userRepository.GetUserByEmail(loginObj.Email);

            var passWordMatch = BCrypt.Net.BCrypt.Verify(loginObj.PassWord, user.PassWord);

            if (user == null || !passWordMatch)
            {
                return BadRequest(new { message = "Details provided invalid" });
            }

            var jwt = _jsonWebTokenService.CreateToken(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(jwt);
        }
        
        [HttpGet(template:"user")]
        public IActionResult GetUser()
        {
            try
            {
                string jwt = Request.Cookies["jwt"];

                var token = _jsonWebTokenService.VerifyUser(jwt);

                int userId = int.Parse(token.Issuer);

                User currentUser = _userRepository.GetUserById(userId);

                return Ok(currentUser);
            }

            catch (Exception e)
            {
                return Unauthorized();
            }
        }
        
        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok();
        }
    }
}
