using System.Collections.Generic;
using HackathonAPI.Models;

namespace GuessChangeListAuthor.Models
{
    public class TimeRangeRule : IRule
    {
        public class CommitDTO
        {
            public int All;
            public int[] Hourly;
        }

        private class AuthorDTO
        {
            public string Author;
            public Dictionary<int, CommitDTO> Commits;
        }

        private readonly Dictionary<string, AuthorDTO> data = new Dictionary<string, AuthorDTO>();

        public void GenerateData(List<ChangeList> allChangeLists)
        {
            foreach (var changeList in allChangeLists)
            {
                AuthorDTO authorDto;
                var author = changeList.Author;
                if (!data.TryGetValue(author, out authorDto))
                {
                    authorDto = new AuthorDTO {Author = author, Commits = new Dictionary<int, CommitDTO>()};
                    data.Add(author, authorDto);
                }
                var commits = authorDto.Commits;
                CommitDTO values;
                var year = changeList.Date.Year;
                if (!commits.TryGetValue(year, out values))
                {
                    values = new CommitDTO() {All = 0, Hourly = new int[24]};
                    commits.Add(year, values);
                }
                var hour = changeList.Date.Hour;
                values.Hourly[hour]++;
                values.All++;
            }
        }

        public int Execute(string author, ChangeList cl)
        {
            AuthorDTO authorDto;
            if (!data.TryGetValue(author, out authorDto))
                return 0;

            var year = cl.Date.Year;
            CommitDTO values;
            if (!authorDto.Commits.TryGetValue(year, out values))
                return 0;

            var hour = cl.Date.Hour;
            var chance = 100 * values.Hourly[hour] / values.All;
            return chance;
        }
    }
}