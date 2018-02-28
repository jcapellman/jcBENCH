using System;

using jcBENCH.lib.Common;

namespace jcBENCH.lib.Benchmarks
{
    public class HashingBenchmark : BaseBenchmark
    {
        protected override string Name => "Hashing";

        protected override string RunBenchmark() => DateTime.Now.Ticks.ToString().ComputeSHA1();
    }
}