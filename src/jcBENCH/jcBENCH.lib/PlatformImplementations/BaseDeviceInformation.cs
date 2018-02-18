using System.Runtime.InteropServices;

namespace jcBENCH.lib.PlatformImplementations
{
    public abstract class BaseDeviceInformation
    {
        public abstract OSPlatform Platform { get; }

        public abstract string OperatingSystem { get; }

        public abstract (string manufacturer, string model, int numberCores, string frequency, string architecture) GetCPUInformation();
    }
}