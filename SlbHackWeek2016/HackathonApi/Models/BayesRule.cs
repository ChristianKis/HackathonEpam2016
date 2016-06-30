using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HackathonAPI.Models;
using System.Text.RegularExpressions;

namespace GuessChangeListAuthor.Models
{
    public class BayesRule : IRule
    {
        private BayesClassifier classifier;

        public void GenerateData(List<ChangeList> allChangeLists)
        {
            if (classifier == null)
            {
                var authors = from item in allChangeLists
                              group item by item.Author into grp
                              select grp.Key;


                classifier = new BayesClassifier(authors.ToArray());
            }

            foreach (var changelist in allChangeLists)
            {
                var desc = Almazor(changelist);
                classifier.Train(changelist.Author, desc);
            }
        }
        private static string Almazor(ChangeList changelist)
        {
            var desc = changelist.Description;
            desc = desc.ToUpper();
            desc = desc.Replace(";", " ; ");
            desc = desc.Replace(",", " ");
            desc = desc.Replace(".", " ");
            desc = desc.Replace("/", " / ");
            desc = desc.Replace("@", " @ ");
            desc = desc.Replace(":", " : ");
            desc = desc.Replace("'", " ' ");
            desc = desc.Replace("\\", " \\ ");

            RegexOptions options = RegexOptions.None;
            var reg2 = new Regex("CL(?<clnumber>[0-9]+)", options);
            MatchEvaluator evaluator = new MatchEvaluator(match =>
            {
                return "cl " + match.Groups["clnumber"];
            });

            desc = reg2.Replace(desc, evaluator);
            Regex regex = new Regex("[ ]{2,}", options);
            desc = regex.Replace(desc, " ");
            return desc;
        }
        public double Execute(string author, ChangeList cl)
        {
            var desc = Almazor(cl);
            var result = classifier.Classifications(desc);
            if (!result.ContainsKey(author))
            {
                return 0;
            }

            var orderedResult = result.OrderByDescending(r => r.Value);
            var lowest = orderedResult.First().Value; // -> 0%
            var highest = orderedResult.Last().Value; // -> 100%
            var percent = 100 / (highest - lowest); // -> 1%
            var returnResult = percent * (highest - result[author]);
            return returnResult;
        }
    }
}