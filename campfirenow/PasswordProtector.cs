using System;
using System.Security.Cryptography;
using System.Text;

namespace Flare
{
    public class PasswordProtector
    {
        private static readonly byte[] AdditionalEntropy = { 9, 8, 7, 6, 5 };

        public static byte[] Protect(string data, string salt)
        {
            try
            {
                // Encrypt the data using DataProtectionScope.CurrentUser. The result can be decrypted
                // only by the same current user.
                var dataWithSalt = string.Format("{0}-=-{1}", data, salt);
                return ProtectedData.Protect(Encoding.UTF8.GetBytes(dataWithSalt), AdditionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Data was not encrypted. An error occurred.");
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static string Unprotect(byte[] data, string salt)
        {
            try
            {
                // Decrypt the data using DataProtectionScope.CurrentUser.
                var dataWithSalt = ProtectedData.Unprotect(data, AdditionalEntropy, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(dataWithSalt).Replace(string.Concat("-=-", salt), string.Empty);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Data was not decrypted. An error occurred.");
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}