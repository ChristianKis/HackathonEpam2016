using HackathonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessChangeListAuthor.Models
{
    public interface IRule
    {
        void GenerateData(List<ChangeList> allChangeLists);
        Dictionary<string, int> Execute(ChangeList cl);
    }
}
