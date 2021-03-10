using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml;

namespace Waves.Horus
{
    public static class Utils
    {
        public static string StringToMd5(string source)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return GetMd5Hash(md5Hash, source);
            }
        }

        public static string AsciiCleaning(string source)
        {
            return Regex.Replace(source, @"[^\u0000-\u007F]+", string.Empty);
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string LoadAttribute(XmlNode node, string attr, string defaultValue)
        {
            string result;
            try
            {
                result = node.Attributes[attr].Value;
                return result;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string RemoveNonPathCharacters(this string str)
        {
            StringBuilder sb = new StringBuilder();
            string[] prohibited = { "\\", "/", ":", "*", "?", "\"", "'", "|" };
            foreach (char c in str)
            {
                bool add = true;
                foreach (string chara in prohibited)
                {
                    if (c.ToString().Equals(chara))
                    {
                        add = false;
                        break;
                    }
                }
                if (add)
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

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
