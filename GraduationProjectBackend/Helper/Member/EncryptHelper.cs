using System.Security.Cryptography;
using System.Text;

namespace GraduationProjectBackend.Helper.Member
{
    public class EncryptHelper
    {
        private int iterations = 10000;
        private int keySize = 256;
        private IConfiguration _configuration;
        public EncryptHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public byte[] Encrypt(string password)
        {
            byte[] salt = getSalt();

            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            byte[] key = pbkdf2.GetBytes(keySize);
            return key;
        }

        private byte[] getSalt()
        {
            byte[] salt;

            String rawSalt = _configuration.GetValue<String>("Salt");
            salt = Encoding.UTF8.GetBytes(rawSalt);

            return salt;
        }

        public static byte[] EncryptStatic(string password)
        {
            byte[] salt = Encoding.UTF8.GetBytes("testSalt");

            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            byte[] key = pbkdf2.GetBytes(256);
            return key;
        }






    }
}
