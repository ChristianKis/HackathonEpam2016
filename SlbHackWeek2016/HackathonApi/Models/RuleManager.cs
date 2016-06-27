using HackathonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessChangeListAuthor.Models
{
    public static class RuleManager
    {
        private static List<IRule> rules = new List<IRule>();
        public static void Add(IRule rule)
        {
            rules.Add(rule);
        }

        public static int Execute(ChangeList cl)
        {
            var sum = 0;
            foreach (var rule in rules)
                sum += rule.Execute(cl);
            return sum;
        }
    }
}