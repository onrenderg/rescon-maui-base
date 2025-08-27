using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Maui;

namespace ResillentConstruction

{
    public class AESCryptography
    {
        static string key = "ResilientConstruction$NicHP@23";
        public static string EncryptAES(string plainText)
        {
            try
            {
               
                var plainBytes = Encoding.UTF8.GetBytes(plainText);
                return Convert.ToBase64String(Encrypt(plainBytes, CreateAes(key)));
            }
            catch (Exception)
            {
                return  "";
            }
        }

        public static string DecryptAES(string encryptedText)
        {
            try
            {
               
                var encryptedBytes = Convert.FromBase64String(encryptedText);
                return Encoding.UTF8.GetString(Decrypt(encryptedBytes, CreateAes(key)));
            }
            catch (Exception)
            {
                return  "";
            }
        }
        private static Aes CreateAes(string secretKey)
        {
            var keyBytes = new byte[16];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
            var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            aes.BlockSize = 128;
            aes.Key = keyBytes;
            aes.IV = keyBytes;
            return aes;
        }
        private static byte[] Encrypt(byte[] plainBytes, Aes aes)
        {
            return aes.CreateEncryptor()
                .TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }
        private static byte[] Decrypt(byte[] encryptedData, Aes aes)
        {
            return aes.CreateDecryptor()
                .TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }
    }
}