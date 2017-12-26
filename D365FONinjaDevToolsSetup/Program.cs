using System;
using System.IO;

namespace D365FONinjaDevToolsSetup
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
               
                File.Copy($@"{Environment.CurrentDirectory}\D365FONinjaDevTools.dll",
                        @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\Extensions\ymhmh4ah.wnn\AddinExtensions\D365FONinjaDevTools.dll"
                        , true);



                Console.WriteLine("Setup finished! Close and enjoy!");
                Console.ReadLine();
            
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
                Console.WriteLine("Seems that an issue prevented me from doing my job :(");
                Console.ReadLine();

            }
            
        }
    }
}
