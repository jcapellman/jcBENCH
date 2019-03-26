using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using jcBENCH.lib.Objects;

namespace jcBENCH.lib.Benchmarks
{
    public abstract class BaseBenchmark
    {
        private const int SECONDS_TO_RUN = 20;

        protected abstract string Name { get; }

        protected abstract string RunBenchmark();
        
        private int RunMultiThreaded()
        {
            Console.WriteLine($"Running {Name} benchmark (MULTI-THREADED) for {SECONDS_TO_RUN} seconds...{Environment.NewLine}");

            var cts = new CancellationTokenSource();

            var parallelOptions = new ParallelOptions
            {
                CancellationToken = cts.Token, MaxDegreeOfParallelism = System.Environment.ProcessorCount
            };

            var numIterations = new List<int>();

            var startTime = DateTime.Now;

            try
            {
                Parallel.For(0, int.MaxValue, parallelOptions, num =>
                {
                    var result = RunBenchmark();

                    if (string.IsNullOrEmpty(result))
                    {
                        return;
                    }

                    if (DateTime.Now.Subtract(startTime).TotalSeconds > SECONDS_TO_RUN)
                    {
                        cts.Cancel();
                    }

                    parallelOptions.CancellationToken.ThrowIfCancellationRequested();

                    lock (numIterations)
                    {
                        numIterations.Add(1);
                    }
                });
            }
            catch (OperationCanceledException)
            {
                // expected
            }

            return numIterations.Count;
        }

        private int RunSingleThreaded()
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

        public int Run(CommandLineSettings settings, bool multiThreaded = false)
        {
            return multiThreaded ? RunMultiThreaded() : RunSingleThreaded();
        }
    }
}