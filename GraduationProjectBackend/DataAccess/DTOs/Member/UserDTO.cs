namespace GraduationProjectBackend.DataAccess.DTOs.Member
{
    public record UserDTO(string account, string password)
    {

        /// <summary>
        /// 帳號
        /// </summary>
        /// <example>user1</example>

        public string account { get; init; } = account;

        /// <summary>
        /// 密碼
        /// </summary>
        /// <example>user1</example>
        public string password { get; init; } = password;

    };
}
