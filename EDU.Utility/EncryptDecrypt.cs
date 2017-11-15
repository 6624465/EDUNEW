using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace EZY.EDU.Utility
{
    public static class EncryptDecrypt
    {
        //'A string of 16 numbers.  {n1, .. , n16} // = 1111111111111111
        private static Byte[] sharedkey = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        private static Byte[] sharedvector = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

        /// <summary>
        /// Method to Encrypt a string
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string Encrypt(string val)
        {
            var tdes = new TripleDESCryptoServiceProvider();

            Byte[] toEncrypt = Encoding.UTF8.GetBytes(val);
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, tdes.CreateEncryptor(sharedkey, sharedvector), CryptoStreamMode.Write);

            cs.Write(toEncrypt, 0, toEncrypt.Length);
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());

        }




        /// <summary>
        /// Method to Decrypt a string
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string Decrypt(string val)
        {

            var tdes = new TripleDESCryptoServiceProvider();

            Byte[] toDecrypt = Convert.FromBase64String(val);

            var ms = new MemoryStream();

            var cs = new CryptoStream(ms, tdes.CreateDecryptor(sharedkey, sharedvector), CryptoStreamMode.Write);

            cs.Write(toDecrypt, 0, toDecrypt.Length);
            cs.FlushFinalBlock();

            return Encoding.UTF8.GetString(ms.ToArray());

        }
    }
}
