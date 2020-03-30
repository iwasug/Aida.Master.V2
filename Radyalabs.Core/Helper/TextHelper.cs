using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Radyalabs.Core.Helper
{
    public static class TextHelper
    {
        public static string ConvertToHtmlNewLine(string str)
        {
            return string.IsNullOrEmpty(str) ? "" : str.Replace(System.Environment.NewLine, "<br/>");
        }

        public static string RandomString(int length, string chars)
        {
            Random random = new Random();

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetRandAlphanumeric(int length)
        {
            return RandomString(length, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
        }

        public static string GetRandNumeric(int length)
        {
            return RandomString(length, "0123456789");
        }

        public static string GetRandAlphabet(int length)
        {
            return RandomString(length, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }

        public static string GetRandomUsername(int length)
        {
            return RandomString2(length);
        }

        public static string GetSixRandomNumeric()
        {
            Random generator = new Random();

            return generator.Next(0, 999999).ToString("D6");
        }

        private static string RandomString2(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public static string ConvertToSha256(string str)
        {
            SHA256Managed crypt = new SHA256Managed();

            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(str), 0, Encoding.ASCII.GetByteCount(str));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }

            return hash.ToLower();
        }

        public static string ToSHA256(this string str)
        {
            return ConvertToSha256(str);
        }

        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString().ToLower();
        }
    }
}
