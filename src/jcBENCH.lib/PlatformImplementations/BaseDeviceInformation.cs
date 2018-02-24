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
            var process = new Process();

            process.StartInfo.FileName = command;

            if (!string.IsNullOrEmpty(argument))
            {
                process.StartInfo.Arguments = argument;
            }

            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();

            var processOutput = string.Empty;

            while (!process.StandardOutput.EndOfStream)
            {
                processOutput = process.StandardOutput.ReadLine();

                if (string.IsNullOrEmpty(searchMatch)) {
                    return processOutput;
                }

                if (processOutput.Contains(searchMatch))
                {
                    return processOutput = processOutput.Replace(searchMatch, "").Trim();
                }
            }

            return null;
        }
    }
}