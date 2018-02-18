using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using jcBENCH.lib.PlatformImplementations;

namespace jcBENCH.lib
{
    public static class DeviceInformation
    {
        public static BaseDeviceInformation GetInformation()
        {
            var deviceInformationImplementations = Assembly.GetAssembly(typeof(DeviceInformation))
                .DefinedTypes.Where(a => a.BaseType == typeof(BaseDeviceInformation) && !a.IsAbstract)
                .Select(b => (BaseDeviceInformation)Activator.CreateInstance(b)).ToList();

            return deviceInformationImplementations.FirstOrDefault(a => RuntimeInformation.IsOSPlatform(a.Platform));
        }
    }
}