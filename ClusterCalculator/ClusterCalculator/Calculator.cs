using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ClusterCalculator
{
    class Calculator
    {
        public List<Point> ClusterCenters;
        public List<Record> Records;
        public string InputFileName;
        public string OutputFileName;
        public bool UseClusterRadius;
        public double ClusterRadius;
        public int LatColumn;
        public int LongColumn;

        public Calculator()
        {
            InitCalculator();
            ClusterCenters = new List<Point>();
            Records = new List<Record>();
            LoadCenters();
            LoadRecords();
            ProcessData();
            OutputData();
        }

        public int InitCalculator()
        {
            bool badData = true;
            string[] headers;
            string tempString = "";

            Console.WriteLine("Welcome to the Cluster Calculator");
            Console.WriteLine("---------------------------------- \n");

            //Get input file
            Console.WriteLine("Press enter to choose the .csv file you want to evaluate");
            Console.ReadLine();
            while(badData)
            {
                InputFileName = FileOps.SelectFile();

                if(InputFileName == "")
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

            while(badData)
            {
                OutputFileName = FileOps.SelectFolder();

                if(OutputFileName == "")
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
            
            while(badData)
            {
                if(tempString == "")
                {
                    Console.WriteLine("Invalid file name, please enter a valid file name");
                    Console.ReadLine();
                }
                else
                {
                    badData = false;
                    OutputFileName += ("\\" + tempString + ".csv");
                }
            }

            headers = FileOps.GetHeaders(InputFileName);
            if(headers == null)
            {
                Console.WriteLine("Press enter to exit the program...");
                Console.ReadLine();

                return 0;
            }

            Console.WriteLine("\nThe following columns have been detected...");
            for(int i = 0; i < headers.Length; i++)
            {
                Console.WriteLine(i + ": " + headers[i]);
            }

            //Get the column that contains Latitude
            Console.WriteLine("Enter the column number that contains the Latitude");
            try
            {
                LatColumn = Convert.ToInt32(Console.ReadLine());
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR in Input");
                Console.WriteLine(e.Message);
                FileOps.WriteToLog(e.Message);
                return 0;
            }

            //Get the column that contains the Longitude
            Console.WriteLine("Enter the Column number that contains the Longitude");
            try
            {
                LongColumn = Convert.ToInt32(Console.ReadLine());
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
            while(badData)
            {
                tempString = Console.ReadLine();
                if (tempString.ToLower() == "y")
                {
                    badData = false;
                    UseClusterRadius = true;
                }
                else if (tempString.ToLower() == "n")
                {
                    badData = false;
                    UseClusterRadius = false;
                }
                else
                {
                    Console.WriteLine("InvalidInput, please enter \'y\' or \'n\'");
                }
            }

            //Get raiud distance if in use
            if(UseClusterRadius)
            {
                Console.WriteLine("Enter the cluster radius");
                badData = true;
                while(badData)
                {
                    try
                    {
                        ClusterRadius = Convert.ToDouble(Console.ReadLine());
                        badData = false;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Invalid entry");
                        FileOps.WriteToLog("Invalid entry for cluster radius");
                    }
                }
            }

            return 1;
        }

        

        

        public void ProcessData()
        {
            foreach(Record record in Records)
            {
                ChooseCluster(record);
            }
        }

        /// <summary>
        /// Writes the new record CSV data to file
        /// </summary>
        public void OutputData()
        {
            string tempLine;
            string[] lines = new string[Records.Count + 1];

            lines[0] = "Date,Year,Month,Weekday,Hour,Primary Type,Latitude,Longitude,Location,State,Cluster,DistanceToCluster";

            for (int i = 0; i < Records.Count; i++)
            {
                tempLine = Records[i].Date + "," + Records[i].Year + "," + Records[i].Month + "," + Records[i].Weekday + "," + Records[i].Hour + "," + Records[i].CrimeDesc + "," + Records[i].Latitude + "," + Records[i].Longitude + "," + Records[i].Location + "," + Records[i].State + "," + Records[i].ClusterID + "," + Records[i].DistToCluster;
                lines[i + 1] = tempLine;
            }

            File.WriteAllLines(@".\Results.csv", lines);
        }

        /// <summary>
        /// Loads the records from the CSV into memory
        /// </summary>
        public void LoadRecords()
        {
            string[] lines;
            lines = File.ReadAllLines(@".\Records.csv");
            string[] tempLine;

            Record newRecord;

            foreach(var line in lines)
            {
                if (line.Contains("Year"))
                {
                    continue;
                }

                tempLine = line.Split(',');
                newRecord = new Record();

                newRecord.Date = tempLine[0];
                newRecord.Year = tempLine[1];
                newRecord.Month = tempLine[2];
                newRecord.Weekday = tempLine[3];
                newRecord.Hour = tempLine[4];
                newRecord.CrimeDesc = tempLine[5];
                newRecord.Latitude = Convert.ToDouble(tempLine[6]);
                newRecord.Longitude = Convert.ToDouble(tempLine[7]);
                newRecord.Location = tempLine[8] + tempLine[9];
                newRecord.State = tempLine[10];

                Records.Add(newRecord);
            }
        }

        /// <summary>
        /// Loads the cluster centers from the CSV into memory
        /// </summary>
        public void LoadCenters()
        {
            string[] lines;


            lines = File.ReadAllLines(@".\Clusters.csv");

            string[] tempLine;
            double[] tempDubs;

            foreach (var line in lines)
            {
                Point newPoint = new Point();
                tempDubs = new double[3];
                tempLine = line.Split(',');

                newPoint.Latitude = Convert.ToDouble(tempLine[0]);
                newPoint.Longitude = Convert.ToDouble(tempLine[1]);
                newPoint.ClusterID = Convert.ToInt32(tempLine[2]);

                ClusterCenters.Add(newPoint);
            }
        }

        /// <summary>
        /// Assigns the correct cluster to the given Record
        /// </summary>
        /// <param name="record"></param>
        public void ChooseCluster(Record record)
        {
            double SmallestDistance = int.MaxValue;
            int bestCluster = 1;
            double tempDist;
            foreach(Point cluster in ClusterCenters)
            {
                tempDist = CalcDistance(record.Latitude, record.Longitude, cluster.Latitude, cluster.Longitude);
                if(tempDist < SmallestDistance)
                {
                    SmallestDistance = tempDist;
                    bestCluster = cluster.ClusterID;
                }
            }

            record.ClusterID = bestCluster;
            record.DistToCluster = SmallestDistance;

            return;
        }

        /// <summary>
        /// Takes 2 geographic coordinates and returns the distance between them in meters
        /// part of this function was taken from : https://stackoverflow.com/questions/639695/how-to-convert-latitude-or-longitude-to-meters
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="long1"></param>
        /// <param name="lat2"></param>
        /// <param name="long2"></param>
        /// <returns></returns>
        public double CalcDistance(double lat1, double long1, double lat2, double long2)
        {
            //For calcs
            double EARTHRADIUS = 6378.137; //in km
            double LatDistance;
            double LongDistance;
            double tempA;
            double tempB;
            double tempC;

            LatDistance = ((lat2 * Math.PI / 180) - (lat1 * Math.PI / 180));
            LongDistance = ((long2 * Math.PI / 180) - (long1 * Math.PI / 180));

            tempA = Math.Pow(Math.Sin(LatDistance / 2), 2) + (Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) * Math.Pow(Math.Sin(LongDistance / 2), 2));
            tempB = 2 * Math.Atan2(Math.Sqrt(tempA), Math.Sqrt(1 - tempA));
            tempC = EARTHRADIUS * tempB;
            return (tempC * 1000);

        }
    }
}
