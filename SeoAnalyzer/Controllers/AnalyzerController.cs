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

        public ActionResult StopWordsModal()
        {
            StopWords model = new StopWords
            {
                StopWordsArray = String.Join(", ", _analyzeService.GetStopWords())
            };
            return PartialView("~/Views/Analyzer/Partial/_StopWordsModal.cshtml", model);
        }


        public ActionResult Analyzer(Analyzer_Settings settings)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AnalysisResult model = new AnalysisResult
                    {
                        PageGeneralInformation = _analyzeService.BuildWebPageInfoBlock(settings.Url),
                        Title = settings.ParcePageTitle ? _analyzeService.BuildWebPageTitleBlock(settings.Url) : null,
                        WebPageText = settings.ParcePageContent ? _analyzeService.BuildWebPageTextBlock(settings.Url) : null,
                        DescriptionMetaTag = settings.ParcePageDescription ? _analyzeService.BuildMetaTagBlock(settings.Url, "description") : null,
                        KeywordsMetaTag = settings.ParcePageKeywords ? _analyzeService.BuildMetaTagBlock(settings.Url, "keywords") : null,
                        PageLinks = settings.ParcePageLinks ? _analyzeService.GetPageLinks(settings.Url) : null,

                    };

                    return PartialView("~/Views/Analyzer/Partial/_AnalysisResult.cshtml", model);
                }

                catch (Exception ex)
                {
                    return PartialView("~/Views/Shared/Error.cshtml", new Error { ErrorException = ex });
                }
            }
            else
            {
                return View(settings);
            }
        }

        [HttpPost]
        public ActionResult SaveStopWordsList(StopWords text)
        {
            if (ModelState.IsValid)
            {
                _analyzeService.SaveStopWords(text.StopWordsArray);

                return RedirectToAction("Index");
            }
            else
            {
                return View(text);
            }

        }
    }
}