using GraduationProjectBackend.DataAccess.Models.Member;
using GraduationProjectBackend.DataAccess.Repositories.Member;
using GraduationProjectBackend.Helper.Member;

namespace GraduationProjectBackend.Services.Member
{
    public class MemberService : IMemberService
    {
        readonly EncryptHelper _encryptHelper;
        readonly JwtHelper _jwtHelper;

        readonly UserRepository _userRepository;

        public MemberService(EncryptHelper encryptHelper, JwtHelper jwtHelper, UserRepository userRepository)
        {

            _encryptHelper = encryptHelper;
            _jwtHelper = jwtHelper;
            _userRepository = userRepository;
        }

        public bool AuthenticatePassword(User user, string password)
        {
            bool isAuth;
            byte[] hashedPassword = _encryptHelper.Encrypt(password);

            isAuth = user.password?.SequenceEqual(hashedPassword) ?? false;

            return isAuth;
        }

        public async Task<string> GenerateToken(User user)
        {
            String token;

            List<Role> userRoles = user.UserRoles.Select(ur => ur.Role!).ToList();
            token = _jwtHelper.GenerateToken(user.userId.ToString(), userRoles);

            return token;

        }

        public async Task<User?> Register(string account, string password)
        {
            User? user = new()
            {
                account = account,
                password = _encryptHelper.Encrypt(password)
            };

            await _userRepository.Add(user);
            user = await _userRepository.GetByCondition(u => u.account == account);

            return user;
        }
    }
}
