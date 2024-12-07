using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary
{
    public class Encryption
    {

        //NEVER STORE KEYS HERE IN REAL LIFE
        private static readonly string EncryptionKey = "S3cureK3yForMyAP1Encrypt10n!";


        public String EncryptPassword(String plainPassword)
        {
            if (string.IsNullOrEmpty(plainPassword))
            {
                return "";
            }

            Aes aes;

            try
            {
                using (aes = Aes.Create())
                {
                    byte[] key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32).Substring(0, 32));
                    aes.Key = key;
                    aes.GenerateIV();

                    using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(plainPassword);
                        byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                        byte[] combined = new byte[aes.IV.Length + encryptedBytes.Length];
                        Array.Copy(aes.IV, 0, combined, 0, aes.IV.Length);
                        Array.Copy(encryptedBytes, 0, combined, aes.IV.Length, encryptedBytes.Length);

                        return Convert.ToBase64String(combined);
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public String DecryptPassword(string encryptedPassword)
        {
            if (string.IsNullOrEmpty(encryptedPassword))
            {
                return "";
            }

            Aes aes;

            try
            {
                byte[] combined = Convert.FromBase64String(encryptedPassword);

                using (aes = Aes.Create())
                {
                    byte[] key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32).Substring(0, 32));
                    aes.Key = key;

                    byte[] iv = new byte[aes.BlockSize / 8];
                    Array.Copy(combined, 0, iv, 0, iv.Length);
                    aes.IV = iv;

                    int encryptedPasswordLength = combined.Length - iv.Length;
                    byte[] encryptedBytes = new byte[encryptedPasswordLength];
                    Array.Copy(combined, iv.Length, encryptedBytes, 0, encryptedPasswordLength);

                    using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    {
                        byte[] plainBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                        return Encoding.UTF8.GetString(plainBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}

