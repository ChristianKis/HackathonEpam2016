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

            RuleManager.Execute(changeList)

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

            myGuess.Author = "John Doo";

            return Ok(myGuess);
        }
    }

    public class MyBestGuess
    {
        public string Author { get; set; }
        public int Id { get; set; }
    }

}
