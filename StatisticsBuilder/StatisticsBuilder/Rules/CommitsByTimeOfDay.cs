﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsBuilder.Models;

namespace StatisticsBuilder.Rules
{
    internal static class CommitsByTimeOfDay
    {
        internal static void WriteStats(List<ChangeList> allChangelists)
        {
            var authors = new Dictionary<string, int[]>();
            foreach (var changelist in allChangelists)
            {
                var author = changelist.Author;
                int[] commits;
                if (!authors.TryGetValue(author, out commits))
                {
                    commits = new int[24];
                    authors.Add(author, commits);
                }
                commits[changelist.Date.Hour]++;
            }
            Console.WriteLine("Percent of commits by hour");
            foreach (var author in authors)
            {
                var value = author.Value;
                var sum = value.Sum();
                for (int i = 0; i < value.Length; i++)
                {
                    value[i] = value[i] * 100 / sum;
                }
                Console.WriteLine("{0}: {1}", author.Key, string.Join(" ", author.Value));
            }
        }
    }
}
