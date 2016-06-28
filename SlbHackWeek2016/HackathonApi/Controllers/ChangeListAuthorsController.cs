using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HackathonAPI.Models;
using GuessChangeListAuthor.Models;

namespace HackathonAPI.Controllers
{
    public class ChangeListAuthorsController : ApiController
    {
        /// <summary>
        /// Try guessing who submitted the changelist to Perforce.
        /// </summary>
        /// <param name="changeList"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Guess([FromBody]ChangeList changeList)
        {
            MyBestGuess myGuess = new MyBestGuess();
            myGuess.Id = changeList.Id;

            var words = changeList.Description.Split(' ', ';').Distinct();

            var data = WebApiApplication.data;

            var allWordsMentioned = new Dictionary<string, Dictionary<string, int>>();

            foreach (var word in words)
            {
                var authorsWhoMentionWord = new Dictionary<string, int>();

                foreach (var author in data.Keys)
                {
                    if (data[author].Words.ContainsKey(word))
                    {
                        authorsWhoMentionWord.Add(author, data[author].Words[word]);
                    }
                }

                allWordsMentioned.Add(word, authorsWhoMentionWord);
            }

            var authorsWithPoint = new Dictionary<string, decimal>();

            foreach (var word in allWordsMentioned)
            {
                foreach (var author in word.Value)
                {
                    if (!authorsWithPoint.ContainsKey(author.Key))
                    {
                        authorsWithPoint.Add(author.Key, 0);
                    }

                    authorsWithPoint[author.Key] += 10000 / word.Value.Count;
                }
            }

            var authorWithHighestPoint = authorsWithPoint.Aggregate((a, b) => (a.Value > b.Value ? a : b));            

            //string result = RuleManager.Execute(changeList);

            //XXX Start working here:
            //Use your algorithm to find out who was the author of the changelist.
            //Set the MyBestGuess.Author property accordingly.

            //Eg:
            //if (changeList.Date > new DateTime(2014, 11, 12))
            //{
            //    changeList.Author = "TSanta";
            //}

            // You can start testing this api at http://localhost:10000/swagger
            // glhf!

            myGuess.Author = authorWithHighestPoint.Key;

            return Ok(myGuess);
        }
    }

    public class MyBestGuess
    {
        public string Author { get; set; }
        public int Id { get; set; }
    }

}
