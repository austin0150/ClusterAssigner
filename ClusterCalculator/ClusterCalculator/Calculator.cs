using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClusterCalculator
{
    class Calculator
    {
        public List<Point> ClusterCenters;
        public List<Record> Records;

        public Calculator()
        {
            ClusterCenters = new List<Point>();
            Records = new List<Record>();
            LoadCenters();
            LoadRecords();
            ProcessData();
            OutputData();
        }

        public void ProcessData()
        {
            foreach(Record record in Records)
            {
                ChooseCluster(record);
            }
        }

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
