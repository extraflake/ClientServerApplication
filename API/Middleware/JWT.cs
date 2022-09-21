using API.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Middleware
{
    public interface IJWTHandler
    {
        string GenerateToken(Account account);
        string GetName(string token);
        string GetEmail(string token);
    }

    public class JWT : IJWTHandler
    {
        private readonly IConfiguration configuration;
        private readonly string key;

        public JWT(string key, IConfiguration configuration)
        {
            this.key = key;
            this.configuration = configuration;
        }

        public string GenerateToken(Account account)
        {
            double tokenExpire = configuration.GetValue<double>("JWTConfigs:Expire");

            if (account == null)
            {
                return null;
            }

            var subject = new ClaimsIdentity(new Claim[] {
                new Claim("id", account.Id.ToString()),
                new Claim("name", account.Name),
                new Claim(ClaimTypes.Email, account.Email),
            });
            //foreach (var role in account.Roles)
            //{
            //    subject.AddClaim(new Claim(ClaimTypes.Role, role.Name));
            //}

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddMinutes(tokenExpire),
                SigningCredentials =
                new SigningCredentials
                (
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GetName(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken result = tokenHandler.ReadJwtToken(token);

            return result.Claims.FirstOrDefault(claim => claim.Type.Equals("name")).Value;
        }

        public string GetEmail(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken result = tokenHandler.ReadJwtToken(token);

            return result.Claims.FirstOrDefault(claim => claim.Type.Equals("email")).Value;
        }
    }
}
