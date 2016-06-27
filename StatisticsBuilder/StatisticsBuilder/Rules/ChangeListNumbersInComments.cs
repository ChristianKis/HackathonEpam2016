using StatisticsBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsBuilder.Rules
{
    internal static class ChangeListNumbersInComments
    {
        internal static void WriteStats(List<ChangeList> allChangelists)
        {
            int numberInComment = 0;
            int noReferredChangelist = 0;
            int sameAuthor = 0;
            int differentAuthor = 0;

            foreach (var changeList in allChangelists)
            {
                var comment = changeList.ParsedDescription.Comment;
                if (comment.Contains("changset") || comment.Contains("changeset") || comment.Contains("TFS"))
                {
                    continue;
                }

                var words = comment.Split(' ');
                foreach (var word in words)
                {
                    int number;
                    if ((word.Length == 5 || word.Length == 6) && int.TryParse(word, out number))
                    {
                        numberInComment++;

                        // 5 v 6 hosszu szamok -> find changelist
                        var referredChangelist = allChangelists.Where(c => c.Id == number).FirstOrDefault();
                        if (referredChangelist == null)
                        {
                            noReferredChangelist++;
                            continue;
                        }

                        if (changeList.Author == referredChangelist.Author)
                        {
                            sameAuthor++;
                        }
                        else
                        {
                            differentAuthor++;
                        }
                    }                        
                }
            }
            Console.WriteLine(String.Format("Number in comment: {0}", numberInComment));
            Console.WriteLine(String.Format("No changelist for number: {0}", noReferredChangelist));
            Console.WriteLine(String.Format("Same author for changelist: {0}", sameAuthor));
            Console.WriteLine(String.Format("Different author for changelist: {0}", differentAuthor));
        }
    }
}
