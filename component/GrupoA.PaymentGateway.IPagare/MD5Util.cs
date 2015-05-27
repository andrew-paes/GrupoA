using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace GrupoA.PaymentGateway.IPagare
{
    public static class MD5Util
    {
        private static readonly MD5CryptoServiceProvider Md5Hasher;

       static MD5Util()
       {
           Md5Hasher = new MD5CryptoServiceProvider();
       }

        /// <summary>
        /// Cria um hash baseado no algoritmo MD5.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CreateHash(string value)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = Md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();

        }
    }
}
