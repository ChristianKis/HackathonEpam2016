using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HackathonAPI.Models;

namespace GuessChangeListAuthor.Models
{
    public class TeamBuilderForSure : IRule
    {
        public void GenerateData(List<ChangeList> allChangeLists)
        {
            throw new NotImplementedException();
        }

        public int Execute(string author, ChangeList cl)
        {
            if (author == "teamcitybuilder" && cl.Description == "pbmgr:")
            {
                return 999;
            }

            return 0;
        }
    }
}