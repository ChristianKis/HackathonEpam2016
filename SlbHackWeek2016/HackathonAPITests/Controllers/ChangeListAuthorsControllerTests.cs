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

            Assert.Fail(String.Format("SuccessRate: {0}%", ((successCount/allChangeLists.Count) * 100).ToString()));
        }

        [TestMethod()]
        public void Disjunct()
        {
            var jsonFile = "changelists_Trainingset.json";
            var jsonString = File.ReadAllText(jsonFile);

            var allChangeLists = JsonConvert.DeserializeObject<List<ChangeList>>(jsonString);

            var filtered = allChangeLists.Skip(15000);

            WebApiApplication.InitializeRuleManager(jsonString);

            var controller = new ChangeListAuthorsController();

            double successCount = 0;

            foreach (var changeList in filtered)
            {
                var result = controller.Guess(changeList) as OkNegotiatedContentResult<MyBestGuess>;
                var author = result.Content.Author;
                if (changeList.Author == author)
                {
                    successCount++;
                }
            }

            Assert.Fail(String.Format("SuccessRate: {0}%", ((successCount / filtered.Count()) * 100).ToString()));
        }
    }
}