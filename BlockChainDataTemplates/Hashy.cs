using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace BlockChainDataTemplates
{
    
    public static class HashGenerator
    {
        
        public static HashAlgorithm CSP { get; set; } = new SHA1CryptoServiceProvider();

        
        public static int HASHLEN = CSP.HashSize >> 2;

       
        public static string HashZero = new string('0', HASHLEN);

       
        public static string HashGen(params string[] strings)
        {

            HASHLEN = CSP.HashSize >> 2;
            byte[] hash = new byte[CSP.HashSize >> 4];

            using (MemoryStream memStream = new MemoryStream())
            {

                foreach (string s in strings)
                {
                    byte[] dataArray = (new UnicodeEncoding()).GetBytes(s);
                    memStream.Write(dataArray, 0, dataArray.Length);
                }
                memStream.Seek(0, SeekOrigin.Begin);
                hash = CSP.ComputeHash(memStream);
            }

            StringBuilder builder = new StringBuilder(HASHLEN);
            foreach (byte b in hash) builder.AppendFormat("{0:x2}", b);
            return builder.ToString();
        }
    }
}
