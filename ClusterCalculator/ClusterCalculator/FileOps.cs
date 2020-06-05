using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ClusterCalculator
{
    public static class FileOps
    {
        public static string LogFileName;

        public static string[] GetHeaders(string fileName)
        {
            if(!File.Exists(fileName))
            {
                FileOps.WriteToLog("ERROR ACCESSING INPUT FILE");
                Console.WriteLine("ERROR ACCESSING INPUT FILE");
                return null;
            }

            StreamReader reader = new StreamReader(fileName);

            string line = reader.ReadLine();

            string[] headers = line.Split(',');

            return headers;
        }

        public static string GetHeader(string fileName)
        {
            if (!File.Exists(fileName))
            {
                FileOps.WriteToLog("ERROR ACCESSING INPUT FILE");
                Console.WriteLine("ERROR ACCESSING INPUT FILE");
                return null;
            }

            StreamReader reader = new StreamReader(fileName);

            string line = reader.ReadLine();

            return line;
        }

        public static void InitLogger()
        {
            DateTime time = DateTime.Now;

            LogFileName = ".//LogFile_" + time.Month + "_" + time.Day + "_" + time.Year + ".txt";
        }

        public static void WriteToLog(string line)
        {
            DateTime time = DateTime.Now;

            line = "(" + time.Hour + ":" + time.Minute + ":" + time.Second + ") " + line;

            try
            {
                if(!File.Exists(LogFileName))
                {
                    StreamWriter stream = File.CreateText(LogFileName);
                    stream.WriteLine(line);
                    stream.Close();
                }
                else
                {
                    StreamWriter stream = File.AppendText(LogFileName);
                    stream.WriteLine(line);
                    stream.Close();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error Writing to Log file at: " + LogFileName);
                Console.WriteLine(e.Message);
            }
        }

        public static string SelectFile()
        {
            string fileToMod = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileToMod = openFileDialog.FileName;
            }

            return fileToMod;
        }

        public static string SelectFolder()
        {
            string folderPath = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.Description = "Select where to create the new file.";
            fbd.ShowNewFolderButton = true;

            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK)
            {
                folderPath = fbd.SelectedPath;
            }

            return folderPath;
        }

        public static List<Point> ReadCenters(string fileName, int longCol, int latCol)
        {
            string[] lines;
            lines = File.ReadAllLines(fileName);
            string[] tempLine;
            List<Point> pointList = new List<Point>();

            Point newPoint;

            foreach (var line in lines)
            {
                if (lines[0] == line)
                {
                    continue;
                }

                tempLine = line.Split(',');
                newPoint = new Point();

                newPoint.Latitude = Convert.ToDouble(tempLine[latCol]);
                newPoint.Longitude = Convert.ToDouble(tempLine[longCol]);
                newPoint.ClusterID = pointList.Count;

                pointList.Add(newPoint);
                FileOps.WriteToLog("Read Cluster Center: Longitude-" + newPoint.Longitude + ", Latitude-" + newPoint.Latitude + ", Cluster ID-" + newPoint.ClusterID);
            }

            return pointList;
        }

        public static List<Record> ReadRecords(string fileName, int longCol, int latCol)
        {
            List<Record> records = new List<Record>();
            string[] lines;
            lines = File.ReadAllLines(fileName);
            string[] tempLine;

            Record newRecord;

            foreach (var line in lines)
            {
                if (line == lines[0])
                {
                    continue;
                }

                tempLine = line.Split(',');
                newRecord = new Record();

                newRecord.data = tempLine;
                newRecord.Latitude = Convert.ToDouble(tempLine[latCol]);
                newRecord.Longitude = Convert.ToDouble(tempLine[longCol]);
                records.Add(newRecord);
            }

            return records;
        }

        public static int OutputData(string fileName, List<Record> records, string header)
        {
            StreamWriter writer = File.AppendText(fileName);

            writer.Write(header + "\n");

            foreach (var Record in records)
            {
                for(int i =0; i < Record.data.Length;i++)
                {
                    writer.Write(Record.data[i]);
                    writer.Write(",");
                }
                writer.Write(Record.ClusterID + "," + Record.DistToCluster);
                writer.Write("\n");
            }

            return 1;
        }
    }
}
