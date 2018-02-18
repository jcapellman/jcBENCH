using System;
using System.Security.Cryptography;
using System.Text;

namespace jcBENCH.lib.Benchmarks
{
    public class HashingBenchmark : BaseBenchmark
    {
        private static string ComputeSHA(string stringToHash)
        {
            using (var sha = new SHA1Managed())
            {
                var hashBytes = sha.ComputeHash(Encoding.ASCII.GetBytes(stringToHash));

                var sb = new StringBuilder();

                foreach (var b in hashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        protected override string Name => "Hashing";

        protected override string RunBenchmark() => ComputeSHA(DateTime.Now.Ticks.ToString());
    }
}