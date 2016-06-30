using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HackathonAPI.Models;

namespace GuessChangeListAuthor.Models
{
    public class AlmaRule : IRule
    {
        private class Dto
        {
            public string Author;

            public Dictionary<string, int> Words = new Dictionary<string, int>();
        }

        private Dictionary<string, Dto> data;
        public void GenerateData(List<ChangeList> allChangeLists)
        {
            var wordsForAuthors = new Dictionary<string, Dto>();

            foreach (var change in allChangeLists)
            {
                if (!wordsForAuthors.ContainsKey(change.Author))
                {
                    wordsForAuthors.Add(change.Author, new Dto
                    {
                        Author = change.Author
                    });
                }

                var wordSplit = change.Description.Split(';', ' ');

                foreach (var word in wordSplit)
                {
                    if (!wordsForAuthors[change.Author].Words.ContainsKey(word))
                    {
                        wordsForAuthors[change.Author].Words.Add(word, 1);
                    }
                    else
                    {
                        wordsForAuthors[change.Author].Words[word]++;
                    }
                }
            }

            data = wordsForAuthors;
        }

        public double Execute(string author, ChangeList cl)
        {
            var words = cl.Description.Split(' ', ';').Distinct();

            // dictionary<word, Dictionary<author, count>>
            var allWordsMentioned = new Dictionary<string, Dictionary<string, double>>();

            foreach (var word in words)
            {
                var authorsWhoMentionWord = new Dictionary<string, double>();

                foreach (var author2 in data.Keys)
                {
                    if (data[author2].Words.ContainsKey(word))
                    {
                        authorsWhoMentionWord.Add(author2, data[author2].Words[word]);
                    }
                }

                allWordsMentioned.Add(word, authorsWhoMentionWord);
            }

            double sum = 0;

            foreach (var word in allWordsMentioned)
            {                
                if(word.Value.ContainsKey(author))
                    sum += word.Value[author] / word.Value.Count;                
            }

            return sum;
        }
    }
}