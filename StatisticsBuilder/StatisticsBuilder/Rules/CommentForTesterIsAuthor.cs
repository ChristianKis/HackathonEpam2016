using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsBuilder.Models;

namespace StatisticsBuilder.Rules
{
    internal static class CommentForTesterIsAuthor
    {
        internal static void WriteStats(List<ChangeList> allChangelists)
        {
            var allAuthors = new HashSet<string>(allChangelists.Select(cl => cl.Author));
            var commentForTesterIsSameAuthor = 0;
            var commentForTesterIsDifferentAuthor = 0;
            var commentForTesterIsSameAuthorByAuthor = new Dictionary<string, int>();
            var commentForTesterIsDifferentAuthorByAuthor = new Dictionary<string, int>();
            foreach (var changelist in allChangelists)
            {
                var description = changelist.ParsedDescription;
                if (changelist.Author == description.CommentForTestersTrimmed)
                {
                    commentForTesterIsSameAuthor++;
                    if (commentForTesterIsSameAuthorByAuthor.ContainsKey(changelist.Author))
                        commentForTesterIsSameAuthorByAuthor[changelist.Author]++;
                    else
                        commentForTesterIsSameAuthorByAuthor[changelist.Author] = 0;
                }
                else if (allAuthors.Contains(description.CommentForTestersTrimmed))
                {
                    commentForTesterIsDifferentAuthor++;
                    if (commentForTesterIsDifferentAuthorByAuthor.ContainsKey(changelist.Author))
                        commentForTesterIsDifferentAuthorByAuthor[changelist.Author]++;
                    else
                        commentForTesterIsDifferentAuthorByAuthor[changelist.Author] = 0;
                }
            }
            Console.WriteLine("Comment for tester is the same as author: {0}, authors: {1}, top: {2}-{3}", commentForTesterIsSameAuthor, commentForTesterIsSameAuthorByAuthor.Count, commentForTesterIsSameAuthorByAuthor.OrderByDescending(a => a.Value).First().Key, commentForTesterIsSameAuthorByAuthor.OrderByDescending(a => a.Value).First().Value);
            Console.WriteLine("Comment for tester is different author: {0}, authors: {1}, top: {2}-{3}", commentForTesterIsDifferentAuthor, commentForTesterIsDifferentAuthorByAuthor.Count, commentForTesterIsDifferentAuthorByAuthor.OrderByDescending(a => a.Value).First().Key, commentForTesterIsDifferentAuthorByAuthor.OrderByDescending(a => a.Value).First().Value);
        }
    }
}
