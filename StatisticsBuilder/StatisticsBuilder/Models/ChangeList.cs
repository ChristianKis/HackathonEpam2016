using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsBuilder.Models
{
    public class ChangeList
    {
        [JsonProperty("Date")]
        /// <summary>
        /// Gets or sets the date when the changelist was submitted.
        /// </summary>
        public DateTime Date { get; set; }

        [JsonProperty("Description")]
        /// <summary>
        /// Gets or sets the description of the changelist.
        /// </summary>
        public string Description { get; set; }

        [JsonProperty("Id")]
        /// <summary>
        /// Gets or sets the id of the changelist.
        /// </summary>
        public int Id { get; set; }

        [JsonProperty("Author")]
        public string Author { get; set; }

        [JsonIgnore]
        public Description ParsedDescription { get; set; }
    }
}
