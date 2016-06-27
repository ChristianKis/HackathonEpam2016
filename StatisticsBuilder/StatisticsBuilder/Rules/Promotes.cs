using StatisticsBuilder.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsBuilder.Rules
{
    internal static class Promotes
    {
        internal static void WriteStats(List<ChangeList> allChangelists)
        {
            // author, list<description>
            var result = new Dictionary<string, List<string>>();

            foreach (var changeList in allChangelists)
            {
                if ((changeList.ParsedDescription.Id != null && changeList.ParsedDescription.Id.Equals("promote", StringComparison.InvariantCultureIgnoreCase))
                    || changeList.ParsedDescription.Comment.IndexOf("promote", StringComparison.InvariantCultureIgnoreCase) > 0)
                {

                    if (!result.ContainsKey(changeList.Author))
                    {
                        result.Add(changeList.Author, new List<string>());
                    }

                    result[changeList.Author].Add(changeList.Description);
                }
            }
            WriteResults(result);
        }

        internal static void CheckRule(List<ChangeList> allChangeLists)
        {
            int ruleMatched = 0;
            int promote = 0;
            foreach (var changeList in allChangeLists)
            {
                var comment = changeList.ParsedDescription.Comment;
                if ((changeList.ParsedDescription.Id != null && changeList.ParsedDescription.Id.Equals("promote", StringComparison.InvariantCultureIgnoreCase))
                    || changeList.ParsedDescription.Comment.IndexOf("promote", StringComparison.InvariantCultureIgnoreCase) > 0)
                {
                    promote++;

                    if (comment.IndexOf("salmon", StringComparison.InvariantCultureIgnoreCase) > 0)
                    {
                        if (changeList.Author == "pkvamme")
                        {
                            ruleMatched++;
                        }
                    }
                    else if (comment.IndexOf("sailfish", StringComparison.InvariantCultureIgnoreCase) > 0)
                    {
                        if (changeList.Author == "thrams1")
                        {
                            ruleMatched++;
                        }
                    }
                    else if (comment.IndexOf("sailfish", StringComparison.InvariantCultureIgnoreCase) > 0)
                    {
                        if (changeList.Author == "sroret")
                        {
                            ruleMatched++;
                        }
                    }
                    else if (comment.IndexOf("Geophysics Extensions", StringComparison.InvariantCultureIgnoreCase) > 0)
                    {
                        if (changeList.Author == "espenp") 
                        {
                            ruleMatched++;
                        }
                    }
                    

                    //TODO: add date range!!!! espenp vs qfu3
                    //TODO: add date range!!!! pkvamme vs inorum

                    //else if (comment.IndexOf("Geophysics Extensions", StringComparison.InvariantCultureIgnoreCase) > 0)
                    //{
                    //    if (changeList.Author == "qfu3")
                    //    {
                    //        ruleMatched++;
                    //    }
                    //}
                    else if (comment.IndexOf("Geophysics Extensions", StringComparison.InvariantCultureIgnoreCase) > 0)
                    {
                        if (changeList.Author == "espenp")
                        {
                            ruleMatched++;
                        }
                    }
                }
            }
        }

        static async void WriteResults(Dictionary<string, List<string>> results)
        {
            using (var writer = File.CreateText("promotes.txt"))
            {
                foreach (var result in results.Keys)
                {
                    await writer.WriteLineAsync(result);
                    foreach (var promotes in results[result])
                    {
                        await writer.WriteLineAsync("\t" + promotes);
                    }
                }
            }
        }
    }
}
