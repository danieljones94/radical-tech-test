using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace radical_tech_test.Interfaces
{
    public interface IJSONWebTokenService
    {
        string CreateToken(int id);
        JwtSecurityToken VerifyUser(string jwt);
    }
}
