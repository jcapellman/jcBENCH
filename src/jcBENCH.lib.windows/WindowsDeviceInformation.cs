﻿using System;
using System.Runtime.InteropServices;

using jcBENCH.lib.PlatformImplementations;

using Microsoft.Win32;

namespace jcBENCH.lib.windows
{
    public class WindowsDeviceInformation : BaseDeviceInformation
    {
        private const string REGISTRY_KEY_CPU = @"HARDWARE\DESCRIPTION\System\CentralProcessor\0\";
        private const string REGISTRY_VALUE_CPU_NAME = "ProcessorNameString";
        private const string REGISTRY_VALUE_CPU_MANUFACTURER = "VendorIdentifier";
        private const string REGISTRY_VALUE_CPU_SPEED = "~MHz";

        private const string REGISTRY_KEY_OS = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";

        public override OSPlatform Platform => OSPlatform.Windows;

        public override string OperatingSystem
        {
            get
            {
                var productName = GetRegistryValue("ProductName", REGISTRY_KEY_OS);
                var version = GetRegistryValue("CurrentVersion", REGISTRY_KEY_OS);

                return $"{productName} (Version {version})";
            }
        }

        private static string GetRegistryValue(string value, string key = REGISTRY_KEY_CPU)
        {
            var registryKey = Registry.LocalMachine.OpenSubKey(key);

            return registryKey?.GetValue(value)?.ToString() ?? "Unknown";
        }

        public override (string manufacturer, string model, int numberCores, string frequency, string architecture) GetCpuInformation() => 
            (GetRegistryValue(REGISTRY_VALUE_CPU_MANUFACTURER), GetRegistryValue(REGISTRY_VALUE_CPU_NAME), Environment.ProcessorCount, $"{GetRegistryValue(REGISTRY_VALUE_CPU_SPEED)}MHz", RuntimeInformation.OSArchitecture.ToString());
    }
}