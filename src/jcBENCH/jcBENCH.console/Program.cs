using System;

using jcBENCH.lib;
using jcBENCH.lib.Common;

namespace jcBENCH.console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            Console.WriteLine($"{Constants.APP_NAME} {Constants.APP_VERSION} (.NET Core Edition)");
            Console.WriteLine("(C) 2012-2018 Jarred Capellman");
            Console.WriteLine($"Source code is available on https://github.com/jcapellman/jcBENCH{System.Environment.NewLine}");
            
            var cpuInformation = new DeviceInformation().GetCPUInformation();

            Console.WriteLine("CPU Information");
            Console.WriteLine("---------------");
            Console.WriteLine($"Manufacturer: {cpuInformation.manufacturer}");
            Console.WriteLine($"Model: {cpuInformation.model}");
            Console.WriteLine($"Count: {cpuInformation.numberCores}x{cpuInformation.frequency}");
            Console.WriteLine($"Architecture: {cpuInformation.architecture}");
            Console.WriteLine($"---------------{System.Environment.NewLine}");
            
            Console.WriteLine($"Running Benchmark....{System.Environment.NewLine}");
            
            var benchmark = new Benchmark();

            var benchmarkResult = benchmark.Run();

            Console.WriteLine($"Benchmark completed in {benchmarkResult.benchmarkDuration} seconds (hash of result {benchmarkResult.benchmarkHash}");
        }
    }
}