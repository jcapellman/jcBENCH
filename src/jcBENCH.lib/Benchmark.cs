using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using jcBENCH.lib.Common;

namespace jcBENCH.lib
{
    public class Benchmark
    {
        private static string ComputeSHA(string stringToHash)
        {
            using (var sha = new SHA1Managed())
            {
                var hashBytes = sha.ComputeHash(Encoding.ASCII.GetBytes(stringToHash));

                var sb = new StringBuilder();

                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
        
        public (double benchmarkDuration, string benchmarkHash) Run()
        {
            var startTime = DateTime.Now;

            var queue = new ConcurrentQueue<string>();

            Parallel.For(0, Constants.NUM_ITERATIONS, x =>
            {
                queue.Enqueue(ComputeSHA(x.ToString()));
            });

            var queueHash = ComputeSHA(string.Join(",", queue.ToArray()));

            return (DateTime.Now.Subtract(startTime).TotalSeconds, queueHash);
        }
    }
}