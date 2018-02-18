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

            Console.WriteLine($"{Constants.APP_NAME} {Constants.APP_VERSION} (.NET Core 2.0 Edition)");
            Console.WriteLine("(C) 2012-2018 Jarred Capellman");
            Console.WriteLine($"Source code is available on https://github.com/jcapellman/jcBENCH{System.Environment.NewLine}");

            var deviceInformation = DeviceInformation.GetInformation();

            Console.WriteLine($"Operating System: {deviceInformation.OperatingSystem}{System.Environment.NewLine}");

            var (manufacturer, model, numberCores, frequency, architecture) = deviceInformation.GetCpuInformation();

            Console.WriteLine("CPU Information");
            Console.WriteLine("---------------");
            Console.WriteLine($"Manufacturer: {manufacturer}");
            Console.WriteLine($"Model: {model}");
            Console.WriteLine($"Count: {numberCores}x{frequency}");
            Console.WriteLine($"Architecture: {architecture}");
            Console.WriteLine($"---------------{System.Environment.NewLine}");
            
            Console.WriteLine($"Running Benchmark....{System.Environment.NewLine}");
            
            var benchmark = new Benchmark();

            var benchmarkResult = benchmark.Run();

            Console.WriteLine($"Benchmark completed in {benchmarkResult.benchmarkDuration} seconds (hash of result {benchmarkResult.benchmarkHash}");
        }
    }
}