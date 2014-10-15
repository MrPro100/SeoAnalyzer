using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeoAnalyzer.Models
{
    public class StopWords
    {
     [Required(ErrorMessage = "Please enter Stop Words separated by commas!")]
     public string StopWordsArray { get; set; }
    }
}