using System;
using System.Security.Cryptography;
using System.Text;

namespace jcBENCH.lib.Common
{
    public static class ExtensionMethods
    {
        private static char GetHexValue(int value)
        {
            if (value < 0 || value > 15)
            {
                throw new ArgumentOutOfRangeException("value", "value must be between 0 and 15.");
            }

            if (value < 10)
            {
                return (char)(value + '0');
            }

            return (char)(value - 10 + 'a');
        }

        public static string ComputeSHA1(this String str)
        {
            using (var sha = new SHA1Managed())
            {
                var hashBytes = sha.ComputeHash(Encoding.ASCII.GetBytes(str));

                var stringArray = hashBytes.Length * 2;
                var charArray = new char[stringArray];
                var index = 0;

                for (var x = 0; x < stringArray; x += 2)
                {
                    var byteValue = hashBytes[index++];

                    charArray[x] = GetHexValue(byteValue / 16);
                    charArray[x + 1] = GetHexValue(byteValue % 16);
                }

                return new string(charArray);
            }
        }
    }
}