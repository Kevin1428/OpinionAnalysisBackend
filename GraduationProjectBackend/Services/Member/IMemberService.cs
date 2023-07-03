using GraduationProjectBackend.DataAccess.Models.Member;

namespace GraduationProjectBackend.Services.Member
{
    public interface IMemberService
    {
        public bool AuthenticatePassword(User user, string password);
        public Task<User?> Register(string account, string password);
        public Task<String> GenerateToken(User user);



    }
}
