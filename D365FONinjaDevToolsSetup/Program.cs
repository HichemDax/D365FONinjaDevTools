﻿using System;
using System.IO;
using Microsoft.Win32;
using System.Linq;

namespace D365FONinjaDevToolsSetup
{
    class Program
    {
        private const string DllName = "D365FONinjaDevTools.dll";
        private const string VsixName = "D365FONinjaDevTools.vsix";
        private const string AddinFolder = "AddinExtensions";

        static void Main(string[] args)
        {
            try
            {
                string sourcePath = Path.Combine(Environment.CurrentDirectory, DllName);
                string targetPath = Path.Combine(FindExtensionFolder(), DllName);
                File.Copy(sourcePath, targetPath, true);
                System.Diagnostics.Process.Start(VsixName);
                Console.WriteLine("Setup finished! Close and enjoy!");
            }
            catch (Exception ee)
            {
                Console.Error.WriteLine(ee);
                Console.Error.WriteLine("Seems that an issue prevented me from doing my job :(");
            }

            Console.ReadLine();
        }

        private static string FindExtensionFolder()
        {
            String path;

            path = Environment.GetEnvironmentVariable("DynamicsVSTools");

            if (string.IsNullOrEmpty(path))
            {
                throw new ApplicationException("Could not find D365FO tools in Windows registry.");
            }
            return Path.Combine(path, AddinFolder);
        }
    }
}