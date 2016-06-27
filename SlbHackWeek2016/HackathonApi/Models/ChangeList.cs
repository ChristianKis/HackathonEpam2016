using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonAPI.Models
{
    /// <summary>
    /// Represents a Perforce changelist.
    /// </summary>
    public class ChangeList
    {
        /// <summary>
        /// Gets or sets the date when the changelist was submitted.
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Gets or sets the description of the changelist.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the id of the changelist.
        /// </summary>
        public int Id { get; set; }
    }
}