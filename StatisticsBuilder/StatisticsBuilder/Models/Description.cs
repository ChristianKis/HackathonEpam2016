﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsBuilder.Models
{
    public class Description
    {
        public string Id { get; set; }
        public string IdTrimmed { get; set; }
        public string[] IdSplitted { get; set; }

        public string Comment { get; set; }
        public string CommentTrimmed { get; set; }

        public string CommentForTesters { get; set; }
        public string CommentForTestersTrimmed { get; set; }
    }
}
