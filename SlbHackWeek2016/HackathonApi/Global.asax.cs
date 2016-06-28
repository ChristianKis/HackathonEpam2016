using GuessChangeListAuthor.Models;
using HackathonAPI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using GuessChangeListAuthor.Statistics;

namespace HackathonAPI
{
    public class WebApiApplication : HttpApplication
    {
        public static Dictionary<string, Class> data;
        protected void Application_Start()
        {
            var jsonFile = Server.MapPath(@"~/App_Data/changelists_Trainingset.json");
            var jsonString = File.ReadAllText(jsonFile);

            var allChangeLists = JsonConvert.DeserializeObject<List<ChangeList>>(jsonString);

            var wordsForAuthors = new Dictionary<string, Class>();

            foreach (var change in allChangeLists)
            {
                if (!wordsForAuthors.ContainsKey(change.Author))
                {
                    wordsForAuthors.Add(change.Author, new Class {Author = change.Author});
                }

                var wordSplit = change.Description.Split(';', ' ');

                foreach (var word in wordSplit)
                {
                    if (!wordsForAuthors[change.Author].Words.ContainsKey(word))
                    {
                        wordsForAuthors[change.Author].Words.Add(word, 1);
                    }
                    else
                    {
                        wordsForAuthors[change.Author].Words[word]++;
                    }
                }                
            }

            data = wordsForAuthors;

            InitializeRuleManager(jsonString);

            //AddAuthorsToRuleManager(allChangeLists);
            //AddDateRageRule(allChangeLists);
            //AddTeamBuilderRule(); // 353 hits, 100% accuracy 0,23%


            //beolvasásí

            //adat staticba

            // rule hozzáadása
            // adatbépítés
            // kiértékelés

            //

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        public static void InitializeRuleManager(string jsonString)
        {
            var allChangeLists = JsonConvert.DeserializeObject<List<ChangeList>>(jsonString);
            AddAuthorsToRuleManager(allChangeLists);
            AddRulesToRuleManager(allChangeLists);
        }

        private static IEnumerable<IRule> GetRules()
        {
            yield return new DateRangeRule();
            yield return new TimeRangeRule();
            yield return new TeamBuilderForSure(); // 353 hits, 100% accuracy
            yield return new ofwmgrForAlmostSure(); // 2434 hits, 2419 belong to ofwmgr, 15 to fsjursaether
        }

        private static void AddRulesToRuleManager(List<ChangeList> allChangeLists)
        {
            foreach (var rule in GetRules())
            {
                rule.GenerateData(allChangeLists);
                RuleManager.AddRule(rule);
            }
        }

        private static void AddAuthorsToRuleManager(List<ChangeList> allChangeLists)
        {
            var authors = from item in allChangeLists
                          group item by item.Author into grp
                          select grp.Key;

            foreach (var author in authors)
            {
                RuleManager.AddAuthor(author);
            }
        }
    }
}
