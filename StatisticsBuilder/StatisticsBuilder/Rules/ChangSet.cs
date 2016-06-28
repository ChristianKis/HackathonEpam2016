using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsBuilder.Models;

namespace StatisticsBuilder.Rules
{
    internal static class ChangSet
    {
        internal static void WriteStats(List<ChangeList> allChangelists)
        {
            var changsetAuthors = new Dictionary<string, int>();
            foreach (var changelist in allChangelists)
            {
                if (changelist.ParsedDescription.Comment.Contains("changset"))
                {
                    if (!changsetAuthors.ContainsKey(changelist.Author))
                    {
                        changsetAuthors.Add(changelist.Author, 1);
                        continue;
                    }
                    changsetAuthors[changelist.Author]++;
                }
            }

            foreach (var author in changsetAuthors.Keys)
            {
                Console.WriteLine("Author {0} used 'changset' {1}", author, changsetAuthors[author]);
            }
        }
    }
}
