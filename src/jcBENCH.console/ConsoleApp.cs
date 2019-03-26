using System;
using System.Reflection;
using System.Runtime.InteropServices;

using jcBENCH.lib;
using jcBENCH.lib.Benchmarks;
using jcBENCH.lib.Common;
using jcBENCH.lib.Handlers;
using jcBENCH.lib.Objects;

namespace jcBENCH.console
{
    public class ConsoleApp
    {
        private static OSPlatform CurrentPlatform {
            get {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                    return OSPlatform.Windows;
                }

                return RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? OSPlatform.OSX : OSPlatform.Linux;
            }
        }

        private void WriteCenteredText(string text, ConsoleColor backgroundColor, ConsoleColor foregroundColor = ConsoleColor.White)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;

            var centerPosition = ((Console.WindowWidth - text.Length) / 2);

            text = text.PadLeft(Console.WindowWidth - centerPosition, ' ');

            text = text.PadRight(Console.WindowWidth, ' ');

            Console.Write(text);
        }

        public async void Run(string[] args)
        {
            try
            {
                var settings = new CommandLineSettings(args);

                if (CurrentPlatform == OSPlatform.Windows)
                {
                    Console.SetWindowSize(120, 40);
                }

                Console.BackgroundColor = ConsoleColor.Black;

                Console.Clear();

                WriteCenteredText(
                    $"{Constants.APP_NAME} {Assembly.GetExecutingAssembly().GetName().Version} (.NET Core 3.0 Edition)",
                    ConsoleColor.DarkRed);
                WriteCenteredText("(C) 2012-2019 Jarred Capellman", ConsoleColor.DarkRed);

                WriteCenteredText("Source code is available on https://github.com/jcapellman/jcBENCH",
                    ConsoleColor.DarkRed);

                Console.BackgroundColor = ConsoleColor.Black;

                var deviceInformation = DeviceInformation.GetInformation(CurrentPlatform);

                if (deviceInformation == null)
                {
                    Console.WriteLine($"Could not load Platform Library for {CurrentPlatform}");

                    return;
                }

                Console.WriteLine(
                    $"{Environment.NewLine}Operating System: {deviceInformation.OperatingSystem}{Environment.NewLine}");

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

                var benchmarkResult = benchmark.Run(settings);

                Console.WriteLine($"Hashing Benchmark Score: {benchmarkResult}{Environment.NewLine}");

                Console.Write("Do you want to submit your result (y/n)?");

                var key = Console.ReadKey();

                if (key.Key != ConsoleKey.Y)
                {
                    return;
                }

                var submissionResult = await new SubmissionHandler().SubmitResultsAsync(
                    new lib.Objects.ResultSubmissionItem
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

                Console.WriteLine(submissionResult ? "Submission was successful" : "Submission failed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error running {Constants.APP_NAME}: {Environment.NewLine}{ex}");
            }
        }
    }
}