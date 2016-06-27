using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsBuilder.Models;

namespace StatisticsBuilder.Rules
{
    internal static class CommentForTesterIsNcOrEmpty
    {
        internal static void WriteStats(List<ChangeList> allChangelists)
        {
            //var allAuthors = new HashSet<string>(allChangelists.Select(cl => cl.Author));
            var commentForTesterIsNull = 0;
            var commentForTesterIsEmpty = 0;
            var commentForTesterIsNc = 0;
            var commentForTesterIsNullByAuthor = new Dictionary<string, int>();
            var commentForTesterIsEmptyByAuthor = new Dictionary<string, int>();
            var commentForTesterIsNcByAuthor = new Dictionary<string, int>();
            foreach (var changelist in allChangelists)
            {
                var description = changelist.ParsedDescription;
                if (description.CommentForTestersTrimmed == null)
                {
                    commentForTesterIsNull++;
                    if (commentForTesterIsNullByAuthor.ContainsKey(changelist.Author))
                        commentForTesterIsNullByAuthor[changelist.Author]++;
                    else
                        commentForTesterIsNullByAuthor[changelist.Author] = 0;
                }
                else if (description.CommentForTestersTrimmed == "")
                {
                    commentForTesterIsEmpty++;
                    if (commentForTesterIsEmptyByAuthor.ContainsKey(changelist.Author))
                        commentForTesterIsEmptyByAuthor[changelist.Author]++;
                    else
                        commentForTesterIsEmptyByAuthor[changelist.Author] = 0;
                }
                else if (string.Equals(description.CommentForTestersTrimmed, "nc", StringComparison.OrdinalIgnoreCase))
                {
                    commentForTesterIsNc++;
                    if (commentForTesterIsNcByAuthor.ContainsKey(changelist.Author))
                        commentForTesterIsNcByAuthor[changelist.Author]++;
                    else
                        commentForTesterIsNcByAuthor[changelist.Author] = 0;
                }
            }
            Console.WriteLine("Comment for tester is null: {0}, authors: {1}, top: {2}-{3}", commentForTesterIsNull, commentForTesterIsNullByAuthor.Count, commentForTesterIsNullByAuthor.OrderByDescending(a => a.Value).First().Key, commentForTesterIsNullByAuthor.OrderByDescending(a => a.Value).First().Value);
            Console.WriteLine("Comment for tester is empty: {0}, authors: {1}, top: {2}-{3}", commentForTesterIsEmpty, commentForTesterIsEmptyByAuthor.Count, commentForTesterIsEmptyByAuthor.OrderByDescending(a => a.Value).First().Key, commentForTesterIsEmptyByAuthor.OrderByDescending(a => a.Value).First().Value);
            Console.WriteLine("Comment for tester is nc: {0}, authors: {1}, top: {2}-{3}", commentForTesterIsNc, commentForTesterIsNcByAuthor.Count, commentForTesterIsNcByAuthor.OrderByDescending(a => a.Value).First().Key, commentForTesterIsNcByAuthor.OrderByDescending(a => a.Value).First().Value);
        }
    }
}
