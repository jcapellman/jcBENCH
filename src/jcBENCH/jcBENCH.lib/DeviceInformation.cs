using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using jcBENCH.lib.PlatformImplementations;

namespace jcBENCH.lib
{
    public class DeviceInformation
    {
        public (string manufacturer, string model, int numberCores, string frequency, string architecture) GetCPUInformation()
        {
            var deviceInformationImplementations = (List<BaseDeviceInformation>)Assembly.GetAssembly(typeof(DeviceInformation))
                .DefinedTypes.Where(a => a.BaseType == typeof(BaseDeviceInformation) && !a.IsAbstract)
                .Select(b => (BaseDeviceInformation)Activator.CreateInstance(b)).ToList();

            var deviceImplementation = deviceInformationImplementations.FirstOrDefault(a => RuntimeInformation.IsOSPlatform(a.Platform));

            if (deviceImplementation == null)
            {
                return (string.Empty, string.Empty, 0, string.Empty, string.Empty);
            }

            return deviceImplementation.GetCPUInformation();
        }
    }
}