using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeoAnalyzer.Models
{
    public class AnalyzedWordForTable
    {
        public string Word { get; set; }

        public long WordOccurrencesCount { get; set; }

        public decimal PercentWordCount { get; set; }
    }
}