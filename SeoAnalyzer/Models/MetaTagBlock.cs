using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SeoAnalyzer.Models
{
    public class MetaTagBlock
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public int ContentTextCount { get; set; }

        public string ContentWithoutStopWords { get; set; }

        public int ContentWithoutStopWordsCount { get; set; }

        public virtual ICollection<AnalyzedWordForTable> AnalyzedWordsTable { get; set; }
    }
}