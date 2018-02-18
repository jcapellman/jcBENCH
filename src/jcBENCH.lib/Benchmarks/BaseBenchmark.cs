using System;

namespace jcBENCH.lib.Benchmarks
{
    public abstract class BaseBenchmark
    {
        private const int SECONDS_TO_RUN = 20;

        protected abstract string Name { get; }

        protected abstract string RunBenchmark();

        public int Run()
        {
            Console.WriteLine($"Running {Name} benchmark for {SECONDS_TO_RUN} seconds...{Environment.NewLine}");

            var startTime = DateTime.Now;

            var numberIterations = 0;

            while (DateTime.Now.Subtract(startTime).TotalSeconds < SECONDS_TO_RUN)
            {
                var result = RunBenchmark();

                if (string.IsNullOrEmpty(result))
                {
                    continue;
                }

                if (DateTime.Now.Subtract(startTime).TotalSeconds > SECONDS_TO_RUN)
                {
                    break;
                }

                numberIterations++;
            }

            return numberIterations;
        }
    }
}