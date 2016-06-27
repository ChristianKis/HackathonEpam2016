﻿using GuessChangeListAuthor.Models;
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

            var authors = from item in allChangeLists                       
                        group item by item.Author into grp
                       select grp.Key;

            foreach (var author in authors)
            {
                RuleManager.Add(author);
            }

            var dateRule = new DateRangeRule();

            dateRule.GenerateData(allChangeLists);
            
            RuleManager.Add(dateRule);


            //beolvasás

            //adat staticba

            // rule hozzáadása
            // adatbépítés
            // kiértékelés

            //

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
