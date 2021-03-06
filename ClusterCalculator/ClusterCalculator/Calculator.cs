﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ClusterCalculator
{
    public class Calculator
    {
        public List<Point> ClusterCenters;
        public List<Record> Records;
        public string InputFileName;
        public string OutputFileName;
        public string CenterFile;
        public bool UseClusterRadius;
        public double ClusterRadius;
        public int LatColumn;
        public int LongColumn;
        public int CenterLatColumn;
        public int CenterLongColumn;

        public Calculator()
        {
            ClusterCenters = new List<Point>();
            Records = new List<Record>();
        }

        

        public void ProcessData()
        {
            Records = FileOps.ReadRecords(InputFileName, LongColumn, LatColumn);
            ClusterCenters = FileOps.ReadCenters(CenterFile, CenterLongColumn, CenterLatColumn);

            for(int i = 0; i < Records.Count; i++)
            {
                Record record = Records[i];
                ChooseCluster(ref record);
            }

            FileOps.OutputData(this.OutputFileName, Records, (FileOps.GetHeader(this.InputFileName) + ",Cluster ID,Distance To Cluster"));
        }

 



        /// <summary>
        /// Assigns the correct cluster to the given Record
        /// </summary>
        /// <param name="record"></param>
        public void ChooseCluster(ref Record record)
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


            if(UseClusterRadius)
            {
                if(SmallestDistance<=ClusterRadius)
                {
                    record.ClusterID = bestCluster;
                    record.DistToCluster = SmallestDistance;
                }
                else
                {
                    record.ClusterID = -1;
                    record.DistToCluster = -1;
                }
            }
            else
            {
                record.ClusterID = bestCluster;
                record.DistToCluster = SmallestDistance;
            }

            FileOps.WriteToLog("Record processed - Lat:" + record.Latitude + ", Long:" + record.Longitude + ", ID:" + record.ClusterID + ", Distance to Cluster:" + record.DistToCluster);

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
