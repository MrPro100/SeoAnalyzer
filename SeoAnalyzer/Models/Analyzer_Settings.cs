using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeoAnalyzer.Models
{
    public class Analyzer_Settings
    {
        //[Url]
        [Required]
        public string Url { get; set; }

        [Display(Name = "Parce Title Meta Tag")]
        public bool ParceTitleMetaTag { get; set; }

        [Display(Name = "Parce Description Meta Tag")]
        public bool ParceDescriptionMetaTag { get; set; }
    }
}