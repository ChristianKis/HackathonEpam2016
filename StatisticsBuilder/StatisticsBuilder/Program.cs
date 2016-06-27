using Newtonsoft.Json;
using StatisticsBuilder.Models;
using StatisticsBuilder.Rules;
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
            allChangeLists.ForEach(a => a.ParsedDescription = ParseDescription(a.Description));
            GenerateFilesForEachAuthor(allChangeLists);

            SameAuthorForTasks.WriteStats(allChangeLists);
            Console.WriteLine();
            ChangeListNumbersInComments.WriteStats(allChangeLists);
            Console.WriteLine();
            CommentForTesterIsAuthor.WriteStats(allChangeLists);
            Console.WriteLine();
            CommentForTesterIsNcOrEmpty.WriteStats(allChangeLists);
            //Console.WriteLine();
            //Promotes.WriteStats(allChangeLists);

            Console.WriteLine();
            Console.WriteLine("Done.");
            Console.ReadKey();
        }

        private static void GenerateFilesForEachAuthor(List<ChangeList> allChangeLists)
        {
            var alma = from item in allChangeLists
                       orderby item.Date
                       group item by item.Author into grp
                       select grp;

            foreach (var item in alma)
            {
                var path = string.Format("file_{0}.json", item.Key);

                using (var writer = new StreamWriter(path))
                {
                    foreach (var one in item)
                    {
                        writer.WriteLine(JsonConvert.SerializeObject(one));
                    }
                }
            }
        }

        private static Description ParseDescription(string description)
        {
            var firstPartIndex = description.IndexOf(";");

            // no ; in the string
            if (firstPartIndex < 0)
            {
                return new Description() { Comment = description, CommentTrimmed = description.Trim() };
            }

            var commentId = description.Substring(0, firstPartIndex);
            var secondPartIndex = description.LastIndexOf(";");

            // only 1 ; in the string
            if (firstPartIndex == secondPartIndex)
            {
                var secondPart = description.Substring(firstPartIndex + 1);
                return new Description() { Id = commentId, Comment = secondPart, IdTrimmed = commentId.Trim(), CommentTrimmed = secondPart.Trim(), IdSplitted = commentId.Split(',').Select(s => s.Trim()).ToArray() };
            }

            var comment = description.Substring(firstPartIndex + 1, secondPartIndex - firstPartIndex - 1);
            var commentForTesters = description.Substring(secondPartIndex + 1);
            return new Description() { Id = commentId, Comment = comment, CommentForTesters = commentForTesters, IdTrimmed = commentId.Trim(), CommentTrimmed = comment.Trim(), CommentForTestersTrimmed = commentForTesters.Trim(), IdSplitted = commentId.Split(',').Select(s => s.Trim()).ToArray() };
        }
    }
}
