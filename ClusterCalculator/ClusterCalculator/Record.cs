using System;
using System.Collections.Generic;
using System.Text;

namespace ClusterCalculator
{
    public class Record
    {
        public string Date { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Weekday { get; set; }
        public string Hour { get; set; }
        public string CrimeDesc { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Location { get; set; }
        public string State { get; set; }
        public int ClusterID { get; set; }
        public double DistToCluster { get; set; }
    }
}
