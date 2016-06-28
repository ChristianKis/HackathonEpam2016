using System;
using HackathonAPI.Models;
using System.Collections.Generic;

namespace GuessChangeListAuthor.Models
{
    public static class RuleManager
    {
        private static List<IRule> rules = new List<IRule>();
        private static List<string> authors = new List<string>();

        public static void AddRule(IRule rule)
        {
            rules.Add(rule);
        }

        public static void AddAuthor(string author)
        {
            authors.Add(author);
        }

        public static string Execute(ChangeList cl)
        {
            var sum = 0;
            var res = "";
            foreach (var author in authors)
            {
                //var one = 0;

                foreach (var rule in rules)
                {
                    var result = rule.Execute(author, cl);
                    if (result == 999)
                    {
                        return author;
                    }
                    //one += result;
                }
                return String.Empty;
                
                //if (one > sum)
                //{
                //    sum = one;
                //    res = author;
                //}
            }

            return res;
        }
    }
}