using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace soundtojson
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!Directory.Exists("death"))
                return;
            if (!Directory.Exists("music"))
                return;

            DataSet dataSet = new DataSet();
            dataSet.Namespace = "NetFrameWork";
            DirectoryInfo deathDir = new DirectoryInfo("death");
            DataTable deathSounds = new DataTable("sounds");
            DataColumn name = new DataColumn("name");
            DataColumn stream = new DataColumn("stream", typeof(bool));
            deathSounds.Columns.Add(name);
            deathSounds.Columns.Add(stream);
            dataSet.Tables.Add(deathSounds);
            foreach (FileInfo file in deathDir.EnumerateFiles())
            {
                string songName = file.Name.Substring(0, file.Name.Length - 4);
                DataRow row = deathSounds.NewRow();
                row["name"] = songName;
                row["stream"] = true;
                deathSounds.Rows.Add(row);
            }


            //DirectoryInfo musicDir = new DirectoryInfo("music");
            //foreach (DirectoryInfo directory in musicDir.GetDirectories())
            //{
            //    foreach (FileInfo file in directory.EnumerateFiles())
            //    {
            //        string songName = file.Name.Substring(0, file.Name.Length - 3);
            //        DataTable table = new DataTable();
            //        table.TableName = "music." + directory.Name + "." + songName;
            //        DataColumn name = new DataColumn("name");
            //        DataR
            //        DataColumn stream = new DataColumn("stream", typeof(bool));
            //    }
            //}

            dataSet.AcceptChanges();
            string json = JsonConvert.SerializeObject(dataSet, Formatting.Indented);
            Console.WriteLine(json);
            Console.ReadLine();
        }
    }

    class Song
    {
        public string Name { get; set; }
        public bool Stream { get; set; }

        public Song(string name)
        {
            this.Name = name;
            Stream = true;
        }
    }

    class Category
    {
        public Song Song { get; set; }
    }
}
