using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClusterCalculator
{
    public static class FileOps
    {
        public static string[] GetHeaders(string fileName)
        {
            if(!File.Exists(fileName))
            {
                Console.WriteLine("ERROR ACCESSING INPUT FILE");
                return null;
            }

        }
    }
}
