using System;
using System.Collections.Generic;
using System.Text;

namespace ClusterCalculator
{
    public class Record
    {
        public string[] data;
        public double Latitude;
        public double Longitude;
        public int ClusterID;
        public double DistToCluster = -1;
    }
}
