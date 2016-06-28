using GuessChangeListAuthor.Models;
using HackathonAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace HackathonAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var jsonFile = @"k:\Hackathon\SlbHackWeek2016\changelists_Trainingset.json";
            var jsonString = File.ReadAllText(jsonFile);

            var allChangeLists = JsonConvert.DeserializeObject<List<ChangeList>>(jsonString);
            AddAuthorsToRuleManager(allChangeLists);
            AddDateRageRule(allChangeLists);
            AddTeamBuilderRule(); // 353 hits, 100% accuracy


            //beolvasás

            //adat staticba

            // rule hozzáadása
            // adatbépítés
            // kiértékelés

            //

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        private static void AddTeamBuilderRule()
        {
            var teambuilderForSureRule = new TeamBuilderForSure();
            RuleManager.Add(teambuilderForSureRule);
        }

        private static void AddDateRageRule(List<ChangeList> allChangeLists)
        {
            var dateRule = new DateRangeRule();

            dateRule.GenerateData(allChangeLists);

            RuleManager.Add(dateRule);
        }

        private static void AddAuthorsToRuleManager(List<ChangeList> allChangeLists)
        {
            var authors = from item in allChangeLists
                          group item by item.Author into grp
                          select grp.Key;

            foreach (var author in authors)
            {
                RuleManager.Add(author);
            }
        }
    }
}
