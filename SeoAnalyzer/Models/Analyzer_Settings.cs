using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeoAnalyzer.Models
{
    public class Analyzer_Settings
    {
        [Required]
        public string Url { get; set; }

        [Display(Name = "Title")]
        public bool ParcePageTitle { get; set; }

        [Display(Name = "Description")]
        public bool ParcePageDescription { get; set; }

        [Display(Name = "Keywords")]
        public bool ParcePageKeywords { get; set; }

        [Display(Name = "Content")]
        public bool ParcePageContent { get; set; }

        [Display(Name = "Links")]
        public bool ParcePageLinks { get; set; }
    }
}