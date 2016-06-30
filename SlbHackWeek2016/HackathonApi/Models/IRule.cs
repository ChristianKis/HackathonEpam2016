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
        double Execute(string author, ChangeList cl);
    }
}
