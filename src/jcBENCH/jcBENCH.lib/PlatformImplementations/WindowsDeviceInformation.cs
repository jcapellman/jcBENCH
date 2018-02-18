using System;
using System.Runtime.InteropServices;

namespace jcBENCH.lib.PlatformImplementations
{
    public class WindowsDeviceInformation : BaseDeviceInformation
    {
        public override OSPlatform Platform => OSPlatform.Windows;

        public override string OperatingSystem => RuntimeInformation.OSDescription;

        public override (string manufacturer, string model, int numberCores, string frequency, string architecture) GetCPUInformation()
        {
            return (string.Empty, string.Empty, Environment.ProcessorCount, string.Empty, RuntimeInformation.OSArchitecture.ToString());
        }
    }
}