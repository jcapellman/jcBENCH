using System.Security.Cryptography;
using System.Text;

namespace jcBENCH.MVC.Common
{
    public static class Extensions
    {
        public static string ToSha256(this string str) => BitConverter.ToString(SHA256.HashData(Encoding.ASCII.GetBytes(str))).Replace("-", "").ToUpper();
    }
}