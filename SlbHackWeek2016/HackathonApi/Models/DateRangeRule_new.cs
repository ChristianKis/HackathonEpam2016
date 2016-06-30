using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HackathonAPI.Models;

namespace GuessChangeListAuthor.Models
{
    public class DateRangeRule_new : IRule
    {
        public Dictionary<string, List<DateTime>> DatesForAuthors = new Dictionary<string, List<DateTime>>();
        public Dictionary<string, List<Tuple<DateTime, DateTime>>> ranges = new Dictionary<string, List<Tuple<DateTime, DateTime>>>();

        public double Execute(string author, ChangeList cl)
        {
            foreach (var dateRange in ranges[author])
            {
                var minDate = dateRange.Item1;
                var maxDate = dateRange.Item2;

                if (minDate <= cl.Date && maxDate >= cl.Date)
                {
                    return 100;
                }

                //if(minDate <= cl.Date.AddDays(2) && maxDate.AddDays(2) >= cl.Date)
                //{
                //    return 20;
                //}
            }

            return 0;
        }

        public void GenerateData(List<ChangeList> allChangeLists)
        {
            foreach (var changeList in allChangeLists)
            {
                if (!DatesForAuthors.ContainsKey(changeList.Author))
                {
                    DatesForAuthors.Add(changeList.Author, new List<DateTime>());
                }

                DatesForAuthors[changeList.Author].Add(changeList.Date);
            }

            var authors = from pairs in DatesForAuthors
                          select pairs.Key;

            foreach(var author in authors)
            {
                ranges.Add(author, new List<Tuple<DateTime, DateTime>>());
                DatesForAuthors[author].Sort();

                DateTime? min = null;
                DateTime? max = null;
                DateTime? prev = null;

                foreach (var date in DatesForAuthors[author])
                {
                    if(min == null)
                    {
                        min = date;
                        max = date;
                        prev = date;
                        continue;
                    }

                    if(date < ((DateTime)prev).AddDays(30))
                    {
                        max = date;
                    }
                    else
                    {
                        ranges[author].Add(new Tuple<DateTime, DateTime>((DateTime)min, (DateTime)max));
                        min = date;
                    }

                    prev = date;
                }

                ranges[author].Add(new Tuple<DateTime, DateTime>((DateTime)min, (DateTime)max));
            }
        }
    }
}