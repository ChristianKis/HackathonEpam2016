using System.Collections.Generic;
using HackathonAPI.Models;

namespace GuessChangeListAuthor.Models
{
    public class DateRangeRule : IRule
    {
        private class DTO
        {
            public string Author;
            public Dictionary<int, int[]> Commits;
            public int AllCommits;
        }

        private readonly Dictionary<string, DTO> data = new Dictionary<string, DTO>();

        public void GenerateData(List<ChangeList> allChangeLists)
        {
            foreach (var changeList in allChangeLists)
            {
                DTO dto;
                var author = changeList.Author;
                if (!data.TryGetValue(author, out dto))
                {
                    dto = new DTO { Author = author, Commits = new Dictionary<int, int[]>() };
                    data.Add(author, dto);
                }
                var commits = dto.Commits;
                int[] values;
                var year = changeList.Date.Year;
                if (!commits.TryGetValue(year, out values))
                {
                    values = new int[12];
                    commits.Add(year, values);
                }
                var month = changeList.Date.Month - 1;
                commits[year][month]++;
                dto.AllCommits++;
            }
        }

        public int Execute(string author, ChangeList cl)
        {
            DTO dto;
            if (!data.TryGetValue(author, out dto))
                return 0;

            var year = cl.Date.Year;
            int[] values;
            if (!dto.Commits.TryGetValue(year, out values))
                return 0;

            var month = cl.Date.Month - 1;
            var chance = 100 * values[month] / dto.AllCommits;
            return chance;
        }
    }
}