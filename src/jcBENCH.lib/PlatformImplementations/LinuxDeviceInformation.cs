using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace jcBENCH.lib.PlatformImplementations
{
    public class LinuxDeviceInformation : BaseDeviceInformation
    {
        private const string VENDOR_ID = "Vendor ID:";
        private const string ARCHITECTURE = "Architecture:";

        public override OSPlatform Platform => OSPlatform.Linux;

        public override string OperatingSystem => RuntimeEnvironment.GetSystemVersion();

        public override (string manufacturer, string model, int numberCores, string frequency, string architecture) GetCpuInformation()
        {
            try
            {
                Process cmd = new Process();
                cmd.StartInfo.FileName = "lscpu";
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.Start();

                var sr = cmd.StandardOutput;
                var output = sr.ReadToEnd();
                cmd.WaitForExit();

                var manufacturer = string.Empty;
                var model = string.Empty;
                var numberCores = 0;
                var frequency = string.Empty;
                var architecture = string.Empty;

                foreach (var line in output.Split('\n'))
                {
                    if (line.StartsWith(VENDOR_ID))
                    {
                        manufacturer = line.Remove(VENDOR_ID.Length).Trim();
                    }
                    else if (line.StartsWith(ARCHITECTURE))
                    {
                        architecture = line.Remove(ARCHITECTURE.Length).Trim();
                    }
                    else if (line.StartsWith("CPU MHz:"))
                    {
                        frequency = line.Remove(8).Trim();
                    }
                    else if (line.StartsWith("Model name:"))
                    {
                        model = line.Remove(11).Trim();
                    }
                }

                return (manufacturer, model, numberCores, frequency, architecture);
            } catch (Exception ex)
            {
                Console.WriteLine(ex);

                return (null, null, 0, null, null);
            }
        }
    }
}