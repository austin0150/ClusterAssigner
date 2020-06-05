using System;
using System.Collections.Generic;
using System.Text;

namespace ClusterCalculator
{
    public class Initializer
    {
        private Calculator calc;

        public Initializer()
        {
            calc = new Calculator();
        }

        public Calculator Init()
        {
            bool badData = true;
            string[] headers;
            string[] centerHeaders;
            string tempString = "";

            Console.WriteLine("Welcome to the Cluster Calculator");
            Console.WriteLine("---------------------------------- \n");

            //Get input file
            Console.WriteLine("Press enter to choose the .csv file you want to evaluate");
            Console.ReadLine();
            while (badData)
            {
                calc.InputFileName = FileOps.SelectFile();

                if (calc.InputFileName == "")
                {
                    Console.WriteLine("Invalid selection, must select a file to open");
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                }
                else
                {
                    badData = false;
                }
            }

            //Get output directory
            Console.WriteLine("Press enter to select the folder for the modified file to be placed in");
            Console.ReadLine();
            badData = true;

            while (badData)
            {
                calc.OutputFileName = FileOps.SelectFolder();

                if (calc.OutputFileName == "")
                {
                    Console.WriteLine("Invalid selection, must select a folder to use");
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                }
                else
                {
                    badData = false;
                }
            }

            //Get the output file name
            Console.WriteLine("Please enter the file name to assign to the new CSV file (without the file extension)");
            tempString = Console.ReadLine();
            badData = true;

            while (badData)
            {
                if (tempString == "")
                {
                    Console.WriteLine("Invalid file name, please enter a valid file name");
                    Console.ReadLine();
                }
                else
                {
                    badData = false;
                    calc.OutputFileName += ("\\" + tempString + ".csv");
                }
            }

            headers = FileOps.GetHeaders(calc.InputFileName);
            if (headers == null)
            {
                Console.WriteLine("Press enter to exit the program...");
                Console.ReadLine();

                return null;
            }

            Console.WriteLine("\nThe following columns have been detected...");
            for (int i = 0; i < headers.Length; i++)
            {
                Console.WriteLine(i + ": " + headers[i]);
            }

            //Get the column that contains Latitude
            Console.WriteLine("Enter the column number that contains the Latitude");
            try
            {
                calc.LatColumn = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR in Input");
                Console.WriteLine(e.Message);
                FileOps.WriteToLog(e.Message);
                return null;
            }

            //Get the column that contains the Longitude
            Console.WriteLine("Enter the Column number that contains the Longitude");
            try
            {
                calc.LongColumn = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR in Input");
                Console.WriteLine(e.Message);
                FileOps.WriteToLog(e.Message);
            }

            //Ask if using cluster radius
            Console.WriteLine("Would you like to set a maximum radius for each cluster? (y/n)");
            badData = true;
            while (badData)
            {
                tempString = Console.ReadLine();
                if (tempString.ToLower() == "y")
                {
                    badData = false;
                    calc.UseClusterRadius = true;
                }
                else if (tempString.ToLower() == "n")
                {
                    badData = false;
                    calc.UseClusterRadius = false;
                }
                else
                {
                    Console.WriteLine("InvalidInput, please enter \'y\' or \'n\'");
                }
            }

            //Get radius distance if in use
            if (calc.UseClusterRadius)
            {
                Console.WriteLine("Enter the cluster radius");
                badData = true;
                while (badData)
                {
                    try
                    {
                        calc.ClusterRadius = Convert.ToDouble(Console.ReadLine());
                        badData = false;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid entry");
                        FileOps.WriteToLog("Invalid entry for cluster radius");
                    }
                }
            }

            //Get the file for cluster centers
            Console.WriteLine("Press enter to choose the .csv file that contains the cluster locations");
            Console.ReadLine();
            while (badData)
            {
                calc.CenterFile = FileOps.SelectFile();

                if (calc.CenterFile == "")
                {
                    Console.WriteLine("Invalid selection, must select a file to open");
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                }
                else
                {
                    badData = false;
                }
            }


            centerHeaders = FileOps.GetHeaders(calc.CenterFile);
            if (centerHeaders == null)
            {
                Console.WriteLine("Invalid File Format, Press enter to exit the program...");
                Console.ReadLine();

                return null;
            }

            Console.WriteLine("\nThe following columns have been detected...");
            for (int i = 0; i < centerHeaders.Length; i++)
            {
                Console.WriteLine(i + ": " + centerHeaders[i]);
            }

            //Get the column that contains Latitude
            Console.WriteLine("Enter the column number that contains the Latitude");
            try
            {
                calc.CenterLatColumn = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR in Input");
                Console.WriteLine(e.Message);
                FileOps.WriteToLog(e.Message);
                return null;
            }

            //Get the column that contains the Longitude
            Console.WriteLine("Enter the Column number that contains the Longitude");
            try
            {
                calc.CenterLongColumn = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR in Input");
                Console.WriteLine(e.Message);
                FileOps.WriteToLog(e.Message);
            }


            return this.calc;
        }
    }
}
