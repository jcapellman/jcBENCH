using System.Runtime.InteropServices;

using jcBENCH.lib.PlatformImplementations;

namespace jcBENCH.lib.osx
{
    public class MacOSDeviceInformation : BaseDeviceInformation
    {
        public override OSPlatform Platform => OSPlatform.OSX;

        public override string OperatingSystem => RuntimeEnvironment.GetSystemVersion();

        public override (string manufacturer, string model, int numberCores, string frequency, string architecture) GetCpuInformation()
        {
            return (string.Empty, string.Empty, 0, string.Empty, string.Empty);
        }
    }
}