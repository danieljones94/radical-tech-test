using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using radical_tech_test.Interfaces;

namespace radical_tech_test.Services
{
    public class JSONWebTokenService : IJSONWebTokenService
    {
        private string randomKey = "asdvjklhbgasplfmvsjkacghasodfk";

        public string CreateToken(int id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(randomKey));

            var userCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(userCredentials);

            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));

            var token = new JwtSecurityToken(header, payload);

            var toReturn = new JwtSecurityTokenHandler().WriteToken(token);

            return toReturn;
        }

        public JwtSecurityToken VerifyUser(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(randomKey);

            handler.ValidateToken(jwt, new TokenValidationParameters{ 
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false, 
                ValidateAudience = false
            }, out SecurityToken verifiedToken);

            return (JwtSecurityToken)verifiedToken;
        }
    }
}
