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
            if (!Directory.Exists("assets\\minecraft\\sounds\\music"))
                return;
            if (!Directory.Exists("assets\\minecraft\\sounds\\death"))
                return;
            if (!Directory.Exists("assets\\minecraft\\sounds\\voice"))
                return;

            Dictionary<string, Category> soundsFile = new Dictionary<string, Category>();
            DirectoryInfo deathDir = new DirectoryInfo("assets\\minecraft\\sounds\\death");
            Category death = new Category();
            soundsFile["fortress.death"] = death;
            foreach (FileInfo file in deathDir.EnumerateFiles())
            {
                string songName = file.Name.Substring(0, file.Name.Length - 4);
                death.addSound(songName, true);
            }


            DirectoryInfo musicDir = new DirectoryInfo("assets\\minecraft\\sounds\\music");
            foreach (DirectoryInfo directory in musicDir.GetDirectories())
            {
                foreach (FileInfo file in directory.EnumerateFiles())
                {
                    Category category = new Category();
                    string songName = file.Name.Substring(0, file.Name.Length - 4);
                    soundsFile["music." + directory.Name + "." + songName] = category;
                    category.addSound("music/" + directory.Name + "/" + songName, true);
                }
            }

            string json = JsonConvert.SerializeObject(soundsFile, Formatting.Indented);
            Console.WriteLine(json);
            Console.ReadLine();
        }
    }

    class Category
    {
        //public DataSet DataSet { get; set; }
        public DataTable sounds { get; set; }

        public Category()
        {
            //DataSet = new DataSet(name);
            //DataSet.Namespace = "NetFrameWork";

            sounds = new DataTable("sounds");
            DataColumn nameColumn = new DataColumn("name");
            DataColumn stream = new DataColumn("stream", typeof(bool));
            sounds.Columns.Add(nameColumn);
            sounds.Columns.Add(stream);
            //DataSet.Tables.Add(sounds);
        }

        public void addSound(string name, bool stream)
        {
            DataRow row = sounds.NewRow();
            row["name"] = name;
            row["stream"] = stream;
            sounds.Rows.Add(row);
            //DataSet.AcceptChanges();
        }
    }
}
