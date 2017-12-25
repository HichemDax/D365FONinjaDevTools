using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetupD365FOAddins
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
               
                File.Copy($@"{Environment.CurrentDirectory}\D365FONinjaDevExtensions.dll",
                        @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\Extensions\ymhmh4ah.wnn\AddinExtensions\D365FONinjaDevExtensions.dll"
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
