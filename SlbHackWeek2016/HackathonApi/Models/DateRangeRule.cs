using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HackathonAPI.Models;

namespace GuessChangeListAuthor.Models
{
    public class DateRangeRule : IRule
    {
        private class DTO
        {
            public string Author { get; set; }
            public DateTime minDate { get; set; }
            public DateTime maxDate { get; set; }
        }

        private List<DTO> datas;

        public void GenerateData(List<ChangeList> allChangeLists)
        {
            datas = new List<DTO>();

            var alma = from item in allChangeLists
                       orderby item.Date
                       group item by item.Author into grp
                       select grp;

            foreach (var item in alma)
            {
                var author = item.Key;
                var min = item.Min(_ => _.Date);
                var max = item.Max(_ => _.Date);

                datas.Add(new DTO { Author = author, minDate = min, maxDate = max });
            }
        }

        public int Execute(string author, ChangeList cl)
        {
            var dto = datas.FirstOrDefault(_ => _.Author == author);

            if (dto == null)
                return 0;

            if (cl.Date >= dto.minDate && cl.Date <= dto.maxDate)
                return 1;
            

            return 0;
        }
    }
}