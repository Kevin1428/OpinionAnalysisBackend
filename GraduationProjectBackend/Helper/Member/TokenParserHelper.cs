using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GraduationProjectBackend.Helper.Member
{
    public static class TokenParserHelper
    {
        public static int GetUserId(HttpContext httpContext)
        {
            return int.Parse(httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name));
        }
    }
}
