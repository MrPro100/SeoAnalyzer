using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeoAnalyzer.Models
{
    public class AnalysisResult
    {
        public PageInfo PageGeneralInformation { get; set; }
        public PageContentBlock Title { get; set; }
        public MetaTagBlock DescriptionMetaTag { get; set; }
        public MetaTagBlock KeywordsMetaTag { get; set; }
        public PageContentBlock WebPageText { get; set; }
        public virtual ICollection<PageLink> PageLinks { get; set; }
    }
}