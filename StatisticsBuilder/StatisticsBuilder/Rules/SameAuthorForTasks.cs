using StatisticsBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsBuilder.Rules
{
    internal static class SameAuthorForTasks
    {
        internal static void WriteStats(List<ChangeList> allChangelists)
        {
            // ugyanaz a SWAT-szam, vagy TFS-szam, Authorok kigyujtese
            var tasks = new Dictionary<string, List<string>>();
            foreach (var changelist in allChangelists)
            {
                if (String.IsNullOrWhiteSpace(changelist.ParsedDescription.Id))
                {
                    continue;
                }

                var descriptionId = changelist.ParsedDescription.Id;

                // number only -> swat
                int swatNumber;
                if (int.TryParse(descriptionId, out swatNumber))
                {
                    var swatString = swatNumber.ToString();
                    if (!tasks.ContainsKey(swatString))
                    {
                        tasks.Add(swatString, new List<string>());
                    }

                    tasks[swatString].Add(changelist.Author);
                    continue;
                }

                // TFS-number -> task

                if (descriptionId.StartsWith("TFS-"))
                {
                    if (!tasks.ContainsKey(descriptionId))
                    {
                        tasks.Add(descriptionId, new List<string>());
                    }

                    tasks[descriptionId].Add(changelist.Author);
                    continue;
                }
            }

            var authorsCount = 0;
            var tasksWithDifferentAuthors = new Dictionary<string, List<string>>();
            var tasksWithMoreThanOneCommitFromTheSameUser = 0;
            foreach (var task in tasks)
            {
                var authorCount = task.Value.Distinct().Count();
                if (authorCount > 1)
                {
                    tasksWithDifferentAuthors.Add(task.Key, task.Value);
                    authorsCount += authorCount;
                    continue;
                }
                else if (authorCount == 1)
                {
                    if (task.Value.Count > 1)
                    {
                        tasksWithMoreThanOneCommitFromTheSameUser++;
                        continue;
                    }
                }
            }

            Console.WriteLine(String.Format("Total number of tasks: {0}", tasks.Count));
            Console.WriteLine(String.Format("Number of tasks where the same user committed more than once: {0}", tasksWithMoreThanOneCommitFromTheSameUser));
            Console.WriteLine(String.Format("Number of tasks with different authors: {0}", tasksWithDifferentAuthors.Count));
            Console.WriteLine(String.Format("Average number of authors for tasks with different authors: {0}", (authorsCount / tasksWithDifferentAuthors.Count)));
        }
    }
}
