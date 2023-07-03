using GraduationProjectBackend.DataAccess.Models.Member;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GraduationProjectBackend.Helper.Member
{
    public class JwtHelper
    {
        private readonly IConfiguration Configuration;

        public JwtHelper(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string GenerateToken(string UserId, List<Role> userRoles, int expireMinutes = 30)
        {
            var issuer = Configuration.GetValue<string>("JwtSettings:Issuer");
            var signKey = Configuration.GetValue<string>("JwtSettings:SignKey");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Iss, issuer),
                new Claim(JwtRegisteredClaimNames.Sub, UserId),
                new Claim(ClaimTypes.Name, UserId),
                new Claim(ClaimTypes.NameIdentifier, UserId),
                new Claim(JwtRegisteredClaimNames.Name, UserId),
                new Claim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(expireMinutes).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            if (userRoles.Count != 0)
            {
                foreach (Role role in userRoles)
                    claims.Add(new Claim(ClaimTypes.Role, role.roleName));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "Users"));
            }

            var userClaimsIdentity = new ClaimsIdentity(claims);


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Subject = userClaimsIdentity,
                Expires = DateTime.Now.AddMinutes(expireMinutes),
                SigningCredentials = signingCredentials
            };

            // Generate a JWT securityToken, than get the serialized Token result (string)
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var serializeToken = tokenHandler.WriteToken(securityToken);

            return serializeToken;
        }
    }
}
