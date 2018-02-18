using System;

namespace jcBENCH.lib
{
    public class DeviceInformation
    {
        public (string manufacturer, string model, int numberCores, string frequency, string architecture) GetCPUInformation()
        {
            return (string.Empty, string.Empty, Environment.ProcessorCount, string.Empty, string.Empty);
        }
    }
}