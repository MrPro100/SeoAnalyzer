using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeoAnalyzer.Models;
using SeoAnalyzer.Services;

namespace SeoAnalyzer.Controllers
{
    public class AnalyzerController : Controller
    {

        AnalyzerService _analyzeService = new AnalyzerService();


        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Analyzer(Analyzer_Settings settings)
        {
            if (ModelState.IsValid)
            {
                AnalysisResult model = new AnalysisResult
                {
                    PageGeneralInformation = _analyzeService.BuildWebPageInfoBlock(settings.Url),
                    WebPageText = _analyzeService.BuildWebPageTextBlock(settings.Url),
                    DescriptionMetaTag = _analyzeService.BuildMetaTagBlock(settings.Url, "description")


                };

                return PartialView("~/Views/Analyzer/Partial/_AnalysisResult.cshtml", model);
            }
            else
            {
                return View(settings);
            }
        }
    }
}