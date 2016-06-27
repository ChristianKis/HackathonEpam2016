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
            int foundReferredChangelist = 0;
            int sameAuthorForReferred = 0;

            foreach (var changeList in allChangelists)
            {
                var comment = changeList.ParsedDescription.Comment;
                if (comment.Contains("changset") || comment.Contains("changeset") || comment.IndexOf("tfs", StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    continue;
                }
                                
                var words = comment.Split(' ');

                bool skipChangelist = false;
                bool hasNumberInComment = false;
                bool foundChangelistForNumber = false;
                bool? sameAuthor = null;
                foreach (var word in words)
                {
                    if (skipChangelist)
                        break;

                    int number;
                    if ((word.Length == 5 || word.Length == 6) && int.TryParse(word, out number))
                    {
                        hasNumberInComment = true;

                        // 5 v 6 hosszu szamok -> find changelist
                        var referredChangelist = allChangelists.Where(c => c.Id == number).FirstOrDefault();
                        if (referredChangelist == null)
                        {
                            continue;
                        }
                        foundChangelistForNumber = true;

                        if (changeList.Author == referredChangelist.Author)
                        {
                            sameAuthor = true;
                            skipChangelist = true;
                        }
                    }                        
                }

                if (hasNumberInComment)
                {
                    numberInComment++;
                }

                if (foundChangelistForNumber)
                {
                    foundReferredChangelist++;
                }

                if (sameAuthor != null && sameAuthor == true)
                {
                    sameAuthorForReferred++;
                }

            }
            Console.WriteLine(String.Format("Number in comment: {0}", numberInComment));
            Console.WriteLine(String.Format("Found changelist for number: {0}", foundReferredChangelist));
            Console.WriteLine(String.Format("Same author for changelist: {0}", sameAuthorForReferred));
        }
    }
}
