using Microsoft.VisualStudio.TestTools.UnitTesting;
using HackathonAPI.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using HackathonAPI.Models;
using Newtonsoft.Json;

namespace HackathonAPI.Controllers.Tests
{
    [TestClass()]
    public class ChangeListAuthorsControllerTests
    {
        [TestMethod()]
        public void GuessTest()
        {
            var jsonFile = "changelists_Trainingset.json";
            var jsonString = File.ReadAllText(jsonFile);

            var allChangeLists = JsonConvert.DeserializeObject<List<ChangeList>>(jsonString);

            WebApiApplication.InitializeRuleManager(jsonString);

            var controller = new ChangeListAuthorsController();

            double successCount = 0;

            foreach (var changeList in allChangeLists)
            {
                var result = controller.Guess(changeList) as OkNegotiatedContentResult<MyBestGuess>;
                var author = result.Content.Author;
                if (changeList.Author == author)
                {
                    successCount++;
                }
            }

            Assert.Fail(String.Format("SuccessCount: {0} - SuccessRate: {1}%", successCount, ((successCount / allChangeLists.Count) * 100).ToString()));
        }

        [TestMethod()]
        public void Disjunct()
        {
            var jsonFile = "changelists_Trainingset.json";
            //var test1 = "changelists_Day1.json";
            var jsonString = File.ReadAllText(jsonFile);
            //var jsonString1 = File.ReadAllText(test1);

            var allChangeLists = JsonConvert.DeserializeObject<List<ChangeList>>(jsonString);

            //var tests = JsonConvert.DeserializeObject<List<ChangeList>>(jsonString1);

            //var a = allChangeLists.Skip(10000).Take(1000);
            var a = allChangeLists.Take(2000);

            WebApiApplication.InitializeRuleManager(jsonString, true);

            var controller = new ChangeListAuthorsController();

            double successCount = 0;

            var failedTexts = new List<Tuple<string, string, string>>();

            foreach (var changeList in a)
            {
                var result = controller.Guess(changeList) as OkNegotiatedContentResult<MyBestGuess>;
                var author = result.Content.Author;
                if (changeList.Author == author)
                {
                    successCount++;
                }
                else
                    failedTexts.Add(new Tuple<string, string, string>("Expected: " + changeList.Author, "Got: " + author, changeList.Description));
            }



            Assert.Fail(String.Format("SuccessCount: {0} - SuccessRate: {1}%", successCount, ((successCount / a.ToList().Count) * 100).ToString()));
        }

        [TestMethod()]
        public void Day1Test()
        {
            var jsonFile = "changelists_Trainingset.json";
            var jsonString = File.ReadAllText(jsonFile);

            WebApiApplication.InitializeRuleManager(jsonString);

            var day1JsonFile = "changelists_Day1.json";
            var day1JsonString = File.ReadAllText(day1JsonFile);
            var allChangeLists = JsonConvert.DeserializeObject<List<ChangeList>>(day1JsonString);

            var controller = new ChangeListAuthorsController();

            double successCount = 0;

            using (var stream = File.Create("Day1-ErrorLog.txt"))
            using (var writer = new StreamWriter(stream))
            {
                foreach (var changeList in allChangeLists)
                {
                    var result = controller.Guess(changeList) as OkNegotiatedContentResult<MyBestGuess>;
                    var author = result.Content.Author;
                    if (changeList.Author == author)
                    {
                        successCount++;
                    }
                    else
                    {
                        writer.WriteLine(String.Format("Expected: {0} - Actual: {1}", changeList.Author, author));
                        writer.WriteLine("Changelist description: " + changeList.Description);
                        writer.WriteLine("Changelist date: " + changeList.Date);
                        writer.WriteLine();
                    }
                }
            }

            Assert.Fail(String.Format("SuccessCount: {0} - SuccessRate: {1}%", successCount, ((successCount / allChangeLists.Count) * 100).ToString()));
        }

        [TestMethod()]
        public void Day2Test()
        {
            var jsonFile = "changelists_Trainingset.json";
            var jsonString = File.ReadAllText(jsonFile);

            WebApiApplication.InitializeRuleManager(jsonString);

            var day2JsonFile = "changelists_Day2.json";
            var day2JsonString = File.ReadAllText(day2JsonFile);
            var allChangeLists = JsonConvert.DeserializeObject<List<ChangeList>>(day2JsonString);

            var controller = new ChangeListAuthorsController();

            double successCount = 0;

            using (var stream = File.Create("Day2-ErrorLog.txt"))
            using (var writer = new StreamWriter(stream))
            {
                foreach (var changeList in allChangeLists)
                {
                    var result = controller.Guess(changeList) as OkNegotiatedContentResult<MyBestGuess>;
                    var author = result.Content.Author;
                    if (changeList.Author == author)
                    {
                        successCount++;
                    }
                    else
                    {
                        writer.WriteLine(String.Format("Expected: {0} - Actual: {1}", changeList.Author, author));
                        writer.WriteLine("Changelist description: " + changeList.Description);
                        writer.WriteLine("Changelist date: " + changeList.Date);
                        writer.WriteLine();
                    }
                }
            }

            Assert.Fail(String.Format("SuccessCount: {0} - SuccessRate: {1}%", successCount, ((successCount / allChangeLists.Count) * 100).ToString()));
        }
    }
}