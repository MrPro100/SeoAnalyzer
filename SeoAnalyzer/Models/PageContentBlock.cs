using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeoAnalyzer.Models
{
    public class PageContentBlock
    {
        public string Name { get; set; }
        public int OriginalTextCount { get; set; }
        public string OriginalText { get; set; }
        public int TextWithoutStopWordsCount { get; set; }
        public string TextWithoutStopWords { get; set; }
        public virtual ICollection<AnalyzedWordForTable> AnalyzedWordsTable { get; set; }
    }
}