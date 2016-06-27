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
                var description = ParseDescription(changelist.Description);
            }
        }

        private static Description ParseDescription(string description)
        {
            var firstPartIndex = description.IndexOf(";");

            // no ; in the string
            if (firstPartIndex < -1)
            {
                return new Description() { Comment = description };
            }

            var commentId = description.Substring(0, firstPartIndex);

            var secondPartIndex = description.LastIndexOf(";");

            // only 1 ; in the string
            if (firstPartIndex == secondPartIndex)
            {
                var secondPart = description.Substring(firstPartIndex, description.Length - firstPartIndex + 1);
                return new Description() { Id = commentId, Comment = secondPart };
            }
            
            var comment = description.Substring(firstPartIndex + 1, secondPartIndex - firstPartIndex - 1);
            var commentForTesters = description.Substring(secondPartIndex + 1);
            return new Description() { Id = commentId, Comment = comment, CommentForTesters = commentForTesters };
        }
    }
}
