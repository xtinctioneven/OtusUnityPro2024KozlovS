
using System;
using System.Text;

namespace SaveSystem
{
    public class Base64DataEncrypter : IDataEncrypter
    {
        public string Encrypt(string decryptedData)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(decryptedData));
        }

        public string Decrypt(string encryptedData)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encryptedData));
        }
    }

    public interface IDataEncrypter
    {
        public string Encrypt(string decryptedData);
        public string Decrypt(string encryptedData);
    }
}