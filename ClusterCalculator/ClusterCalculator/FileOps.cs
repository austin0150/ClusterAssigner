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
                Console.WriteLine("ERROR ACCESSING INPUT FILE");
                return null;
            }

            StreamReader reader = new StreamReader(fileName);

            string line = reader.ReadLine();

            string[] headers = line.Split(',');

            return headers;
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
    }
}
