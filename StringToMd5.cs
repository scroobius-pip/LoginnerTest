using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LoginnerTest2
{
    //Converting String to MD5
    internal class StringToMd5
    {
        public static string HMACMd5(string username, string password)
        {
            string Md5Hash = xBrainLab.Security.Cryptography.MD5.GetHashString(username); //"758E0474FD2DC19F529170A78448CC19"

            xBrainLab.Security.Cryptography.HMACMD5 hmac = new xBrainLab.Security.Cryptography.HMACMD5(Md5Hash);
            byte[] computedHashString = hmac.ComputeHash(password);
            string sbinary = "";
            for (int i = 0; i < computedHashString.Length; i++)
            {
                sbinary += computedHashString[i].ToString("X2");
            }
            return sbinary;
        }
    }
}