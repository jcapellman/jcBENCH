using System;
using System.Runtime.InteropServices;

using jcBENCH.lib;
using jcBENCH.lib.Benchmarks;
using jcBENCH.lib.Common;
using jcBENCH.lib.Handlers;

namespace jcBENCH.console
{
    public class ConsoleApp
    {
        private void WriteCenteredText(string text, ConsoleColor backgroundColor, ConsoleColor foregroundColor = ConsoleColor.White)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;

            var centerPosition = ((Console.WindowWidth - text.Length) / 2);

            text = text.PadLeft(Console.WindowWidth - centerPosition, ' ');

            text = text.PadRight(Console.WindowWidth, ' ');

            Console.Write(text);
        }

        public async void Run()
        {
            Console.SetWindowSize(120, 40);

            Console.BackgroundColor = ConsoleColor.Black;

            Console.Clear();

            WriteCenteredText($"{Constants.APP_NAME} {Constants.APP_VERSION} (.NET Core 2.0 Edition)", ConsoleColor.DarkRed);
            WriteCenteredText("(C) 2012-2018 Jarred Capellman", ConsoleColor.DarkRed);

            WriteCenteredText("Source code is available on https://github.com/jcapellman/jcBENCH", ConsoleColor.DarkRed);

            Console.BackgroundColor = ConsoleColor.Black;

            var deviceInformation = DeviceInformation.GetInformation();

            Console.WriteLine($"{Environment.NewLine}Operating System: {deviceInformation.OperatingSystem}{Environment.NewLine}");

            var (manufacturer, model, numberCores, frequency, architecture) = deviceInformation.GetCpuInformation();

            Console.WriteLine("---------------");
            Console.WriteLine("CPU Information");
            Console.WriteLine("---------------");
            Console.WriteLine($"Manufacturer: {manufacturer}");
            Console.WriteLine($"Model: {model}");
            Console.WriteLine($"Count: {numberCores}x{frequency}");
            Console.WriteLine($"Architecture: {architecture}");
            Console.WriteLine($"---------------{Environment.NewLine}");

            var benchmark = new HashingBenchmark();

            var benchmarkResult = benchmark.Run();

            Console.WriteLine($"Hashing Benchmark Score: {benchmarkResult}{Environment.NewLine}");

            Console.WriteLine($"Do you want to submit your result (y/n)?");

            var key = Console.ReadKey();

            if (key.Key == ConsoleKey.N)
            {
                return;
            }

            var submissionResult = await new SubmissionHandler().SubmitResultsAsync(new lib.Objects.ResultSubmissionItem
            {
                BenchmarkID = "Hashing",
                BenchmarkResult = benchmarkResult,
                CPUArchitecture = architecture,
                CPUFrequency = $"{numberCores}x{frequency}",
                CPUManufacturer = manufacturer,
                CPUName = model,
                OperatingSystem = deviceInformation.OperatingSystem,
                PlatformID = OSPlatform.Windows.ToString()
            });

            if (submissionResult)
            {
                Console.WriteLine("Submission was successful");
            }
            else
            {
                Console.WriteLine("Submission failed");
            }
        }
    }
}