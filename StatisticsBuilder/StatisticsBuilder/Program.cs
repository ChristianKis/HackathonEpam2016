using Newtonsoft.Json;
using StatisticsBuilder.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsonFile = "changelists_Trainingset.json";
            var jsonString = File.ReadAllText(jsonFile);

            var allChangeLists = JsonConvert.DeserializeObject<List<ChangeList>>(jsonString);
                        
            foreach (var changelist in allChangeLists)
            {

            }
        }
    }
}
