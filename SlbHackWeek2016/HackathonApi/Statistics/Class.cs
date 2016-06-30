using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessChangeListAuthor.Statistics
{
    public class Class
    {
        public string Author;

        public Dictionary<string, int> Words = new Dictionary<string, int>();
        public List<DateTime> Dates = new List<DateTime>();
    }
}