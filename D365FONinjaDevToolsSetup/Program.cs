using System;
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
            using (var extensionsRegKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\VisualStudio\14.0\ExtensionManager\EnabledExtensions"))
            {

                string axToolsKeyName = "";
                string path = "";

                if (extensionsRegKey != null)
                {
                    axToolsKeyName = extensionsRegKey.GetValueNames().Where(name => name.StartsWith("DynamicsRainierVSTools")).FirstOrDefault();
                }

                if (axToolsKeyName != null)
                {
                    path = (string)extensionsRegKey.GetValue(axToolsKeyName);
                }

                if (String.IsNullOrEmpty(path))
                {
                    throw new ApplicationException("Could not find D365FO tools in Windows registry.");
                }

                return Path.Combine(path, AddinFolder);
            }
        }
    }
}