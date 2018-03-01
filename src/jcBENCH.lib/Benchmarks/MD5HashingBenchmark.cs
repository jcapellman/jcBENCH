using System;

using jcBENCH.lib.Common;

namespace jcBENCH.lib.Benchmarks
{
    public class MD5HashingBenchmark : BaseBenchmark
    {
        protected override string Name => "MD5 Hashing";

        protected override string RunBenchmark() => DateTime.Now.Ticks.ToString().ComputeMD5();
    }
}