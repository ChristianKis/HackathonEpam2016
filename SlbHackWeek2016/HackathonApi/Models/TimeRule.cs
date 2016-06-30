using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HackathonAPI.Models;

namespace GuessChangeListAuthor.Models
{
    public class TimeRule : IRule
    {
        public Dictionary<string, List<TimeSpan>> DatesForAuthors = new Dictionary<string, List<TimeSpan>>();
        public Dictionary<string, List<Tuple<TimeSpan, TimeSpan>>> ranges = new Dictionary<string, List<Tuple<TimeSpan, TimeSpan>>>();
        public double Execute(string author, ChangeList cl)
        {
            //var minDate = DatesForAuthors[author].Min();
            //var maxDate = DatesForAuthors[author].Max();

            //if (minDate <= cl.Date.TimeOfDay && maxDate >= cl.Date.TimeOfDay)
            //{
            //    return 100;
            //}

            ////if(minDate <= cl.Date.AddDays(2) && maxDate.AddDays(2) >= cl.Date)
            ////{
            ////    return 20;
            ////}


            //return 0;

            foreach (var dateRange in ranges[author])
            {
                var minDate = dateRange.Item1;
                var maxDate = dateRange.Item2;

                if (minDate <= cl.Date.TimeOfDay && maxDate >= cl.Date.TimeOfDay)
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
                    DatesForAuthors.Add(changeList.Author, new List<TimeSpan>());
                }

                DatesForAuthors[changeList.Author].Add(changeList.Date.TimeOfDay);
            }

            var authors = from pairs in DatesForAuthors
                          select pairs.Key;

            foreach (var author in authors)
            {
                ranges.Add(author, new List<Tuple<TimeSpan, TimeSpan>>());
                DatesForAuthors[author].Sort();

                TimeSpan? min = null;
                TimeSpan? max = null;
                TimeSpan? prev = null;

                foreach (var date in DatesForAuthors[author])
                {
                    if (min == null)
                    {
                        min = date;
                        max = date;
                        prev = date;
                        continue;
                    }

                    if (date < ((TimeSpan)prev).Add(new TimeSpan(0,30,0)))
                    {
                        max = date;
                    }
                    else
                    {
                        ranges[author].Add(new Tuple<TimeSpan, TimeSpan>((TimeSpan)min, (TimeSpan)max));
                        min = date;
                    }

                    prev = date;
                }

                ranges[author].Add(new Tuple<TimeSpan, TimeSpan>((TimeSpan)min, (TimeSpan)max));
            }
        }
    }
}