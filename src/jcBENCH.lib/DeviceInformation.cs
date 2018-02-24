using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using jcBENCH.lib.PlatformImplementations;

namespace jcBENCH.lib
{
    public static class DeviceInformation
    {
        public static BaseDeviceInformation GetInformation(OSPlatform osPlatform)
        {
            var fileName = $"jcBENCH.lib.{osPlatform.ToString().ToLower()}.dll";

            if (!File.Exists(fileName))
            {
                throw new Exception($"Could not find {osPlatform} Assembly ({fileName}");
            }

            var assembly = Assembly.LoadFile(Path.Combine(AppContext.BaseDirectory, fileName));

            if (assembly == null)
            {
                throw new Exception("Failure loading Dynamic Platform Assembly");
            }

            return assembly.DefinedTypes.Where(a => a.BaseType == typeof(BaseDeviceInformation) && !a.IsAbstract)
                .Select(b => (BaseDeviceInformation)Activator.CreateInstance(b)).FirstOrDefault(c => RuntimeInformation.IsOSPlatform(osPlatform));
        }
    }
}