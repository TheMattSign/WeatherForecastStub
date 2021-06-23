using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security.Cryptography;

namespace TestWebApplication.Utils
{
    public class EncryptionUtils
    {

        private readonly IConfiguration _configuration;
        private ICryptoTransform encryptor;
        private ICryptoTransform decryptor;

        public EncryptionUtils(IConfiguration configuration)
        {
            this._configuration = configuration;
            loadAes();
        }

        private void loadAes()
        {
            AesManaged aes = new AesManaged();
            aes.Key = Convert.FromBase64String(_configuration["Key"]);
            aes.IV = Convert.FromBase64String(_configuration["IV"]);

            encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        }

        public String encrypt(String textToEncrypt)
        {
            byte[] encrypted;

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(textToEncrypt);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public String decrypt(String textToDecrypt)
        {
            byte[] encrypted = Convert.FromBase64String(textToDecrypt);
            string plaintext = null;

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(encrypted))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

            return plaintext.Trim();
        }
    }
}
