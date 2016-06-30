using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HackathonAPI.Models;

namespace GuessChangeListAuthor.Models
{
    /// <summary>
    /// 
    /// </summary>
    public static class BayesCalculator 
    {
        private static BayesClassifier classifier;
        public static void GenerateData(List<ChangeList> allChangeLists)
        {
            //Index
            var authors = from item in allChangeLists
                          group item by item.Author into grp
                          select grp.Key;


            classifier = new BayesClassifier(authors.ToArray());

            foreach (var changelist in allChangeLists)
            {
                classifier.Train(changelist.Author, changelist.Description);
            }

        }

        public static string Execute(ChangeList cl)
        {
            return classifier.Classify(cl.Description);
        }
    }
}