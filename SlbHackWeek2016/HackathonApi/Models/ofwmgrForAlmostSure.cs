using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HackathonAPI.Models;

namespace GuessChangeListAuthor.Models
{
    public class ofwmgrForAlmostSure : IRule
    {
        public void GenerateData(List<ChangeList> allChangeLists)
        {
        }

        public int Execute(string author, ChangeList cl)
        {
            if (author == "ofwmgr" && cl.Description.Contains("changset"))
            {
                return 999;
            }

            return 0;
        }
    }
}