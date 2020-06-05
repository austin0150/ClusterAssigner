using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ClusterCalculator
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            FileOps.InitLogger();
            Initializer init = new Initializer();
            Calculator calc = init.Init();
            if(calc.Equals(null))
            {
                return;
            }
            calc.ProcessData();

            Console.WriteLine("Program completed, Press enter to exit");

        }

    }
}
