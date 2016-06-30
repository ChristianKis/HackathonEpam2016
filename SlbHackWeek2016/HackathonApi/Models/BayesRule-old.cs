//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using BayesSharp;
//using HackathonAPI.Models;

//namespace GuessChangeListAuthor.Models
//{
//    public class BayesRuleOld : IRule
//    {
//        private BayesSimpleTextClassifier classifier;

//        public void GenerateData(List<ChangeList> allChangeLists)
//        {
//            if (classifier == null)
//            {
//                classifier = new BayesSimpleTextClassifier();
//            }

//            foreach (var changelist in allChangeLists)
//            {
//                classifier.Train(changelist.Author, changelist.Description);
//            }
//        }

//        public double Execute(string author, ChangeList cl)
//        {
//            var result = classifier.Classify(cl.Description);
//            if (!result.ContainsKey(author))
//            {
//                return 0;
//            }
//            return result[author] * 1000;
//        }
//    }
//}