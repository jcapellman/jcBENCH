using System.Diagnostics;
using System.Runtime.InteropServices;

namespace jcBENCH.lib.PlatformImplementations
{
    public abstract class BaseDeviceInformation
    {
        public abstract OSPlatform Platform { get; }

        public abstract string OperatingSystem { get; }

        public abstract (string manufacturer, string model, int numberCores, string frequency, string architecture) GetCpuInformation();

        protected string ParseConsoleOutput(string command, string searchMatch = null, string argument = null)
        {
            var process = new Process {StartInfo = {FileName = command}};

            if (!string.IsNullOrEmpty(argument))
            {
                process.StartInfo.Arguments = argument;
            }

            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();

            while (!process.StandardOutput.EndOfStream)
            {
                var processOutput = process.StandardOutput.ReadLine();

                if (string.IsNullOrEmpty(searchMatch)) {
                    return processOutput;
                }

                if (processOutput != null && processOutput.Contains(searchMatch))
                {
                    return processOutput.Replace(searchMatch, "").Trim();
                }
            }

            return null;
        }
    }
}