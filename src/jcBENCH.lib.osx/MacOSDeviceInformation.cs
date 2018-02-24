using System;
using System.Runtime.InteropServices;

using jcBENCH.lib.PlatformImplementations;

namespace jcBENCH.lib.osx
{
    public class MacOSDeviceInformation : BaseDeviceInformation
    {
        public override OSPlatform Platform => OSPlatform.OSX;

        public override string OperatingSystem {
            get {
                return ParseConsoleOutput("system_profiler", "System Version:", "SPSoftwareDataType") ?? "Unknown";
            }
        }

        public override (string manufacturer, string model, int numberCores, string frequency, string architecture) GetCpuInformation()
        {
            var cpuString = ParseConsoleOutput("sysctl", argument: "-n machdep.cpu.brand_string");

            var manufacturer = cpuString.Split(' ')[0].Trim();

            cpuString = cpuString.Replace(manufacturer, "").Trim();

            var model = cpuString.Split("CPU @ ")[0].Trim();

            cpuString = cpuString.Replace(model, "").Replace("CPU @", "").Trim();

            var frequency = cpuString;

            return (manufacturer, model, Environment.ProcessorCount, frequency, RuntimeInformation.ProcessArchitecture.ToString());
        }
    }
}