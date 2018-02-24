using System;
using System.Runtime.InteropServices;

using jcBENCH.lib.PlatformImplementations;

namespace jcBENCH.lib.linux
{
    public class LinuxDeviceInformation : BaseDeviceInformation
    {
        public override OSPlatform Platform => OSPlatform.Linux;

        public override string OperatingSystem => ParseConsoleOutput("lsb_release", "Description:", "-a");

        public override (string manufacturer, string model, int numberCores, string frequency, string architecture) GetCpuInformation()
        {
            var manufacturer = string.Empty;
            var model = string.Empty;
            var frequency = string.Empty;

            return (manufacturer, model, Environment.ProcessorCount, frequency, RuntimeInformation.ProcessArchitecture.ToString());
        }
    }
}