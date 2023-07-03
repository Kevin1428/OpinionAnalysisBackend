namespace GraduationProjectBackend.DataAccess.DTOs
{

    public class UserDTO
    {

        public string? account { get; set; }
        public string? password { get; set; }

        public UserDTO(string account, string password)
        {
            this.account = account;
            this.password = password;
        }
    }
}
