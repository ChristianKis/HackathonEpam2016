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
            foreach (var changelist in allChangelists)
            {
                var description = changelist.ParsedDescription;
                if (changelist.Author == description.CommentForTestersTrimmed)
                    commentForTesterIsSameAuthor++;
                else if (allAuthors.Contains(description.CommentForTestersTrimmed))
                    commentForTesterIsDifferentAuthor++;
            }
            Console.WriteLine("Comment for tester is the same as author: {0}", commentForTesterIsSameAuthor);
            Console.WriteLine("Comment for tester is different author: {0}", commentForTesterIsDifferentAuthor);
        }
    }
}
