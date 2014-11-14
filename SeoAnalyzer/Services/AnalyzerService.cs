using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SeoAnalyzer.Models;
using SeoAnalyzer.HtmlParser;
using SeoAnalyzer.Extensions;
using System.Text;
using System.IO;

namespace SeoAnalyzer.Services
{
    public class AnalyzerService
    {
        public PageInfo BuildWebPageInfoBlock(string url)
        {

            int pageSize;

            string encoding;

            Uri uri = url.GetUri();

            HtmlParser.HtmlParser.GetWebPageInfo(uri.ToString(), out encoding, out pageSize);

            return new PageInfo
            {
                Host = uri.Host,
                Encoding = encoding,
                Size = pageSize
            };

        }
        public PageContentBlock BuildWebPageTextBlock(string url)
        {

            var pageText = HtmlParser.HtmlParser.GetWebPageContent(url.GetUri().ToString()).ClearParcerResult().TrimFull();

            var pageTextWithoutStopWords = pageText.RemoveStopWords(GetStopWords());

            return new PageContentBlock
            {
                Name = "Content",
                OriginalText = pageText,
                OriginalTextCount = pageText.WordCount(),
                TextWithoutStopWords = pageTextWithoutStopWords,
                TextWithoutStopWordsCount = pageTextWithoutStopWords.WordCount(),
                AnalyzedWordsTable = GenerateWordsTable(pageTextWithoutStopWords)

            };
        }

        public PageContentBlock BuildWebPageTitleBlock(string url)
        {

            var titleText = HtmlParser.HtmlParser.GetWebPageTitle(url.GetUri().ToString()).ClearParcerResult().TrimFull();

            var titleTexttWithoutStopWords = titleText.RemoveStopWords(GetStopWords());

            return new PageContentBlock
            {
                Name = "Title",
                OriginalText = titleText,
                OriginalTextCount = titleText.WordCount(),
                TextWithoutStopWords = titleTexttWithoutStopWords,
                TextWithoutStopWordsCount = titleTexttWithoutStopWords.WordCount(),
                AnalyzedWordsTable = GenerateWordsTable(titleTexttWithoutStopWords)

            };
        }

        public MetaTagBlock BuildMetaTagBlock(string url, string metaTagName)
        {

            var pageText = HtmlParser.HtmlParser.GetWebPageMetaTagContent(url.GetUri().ToString(), metaTagName).ClearParcerResult().TrimFull();

            var contentWithoutStopWords = pageText.RemoveStopWords(GetStopWords());

            return new MetaTagBlock
            {
                Name = metaTagName,
                Content = pageText,
                ContentTextCount = pageText.WordCount(),
                ContentWithoutStopWords = contentWithoutStopWords,
                ContentWithoutStopWordsCount = contentWithoutStopWords.WordCount(),
                AnalyzedWordsTable = GenerateWordsTable(contentWithoutStopWords)

            };

        }

        public List<PageLink> GetPageLinks(string url)
        {
            var pageLinks = HtmlParser.HtmlParser.GetWebPageLinks(url.GetUri().ToString());

            return pageLinks.Select(x => new PageLink { Url = x.Url, Text = x.Text }).ToList();
        }

        public string[] GetStopWords()
        {
            List<string> stopWords = new List<string>();

            var fileWidthStopWords = HttpContext.Current.Server.MapPath("/") + "StopWords.txt";

            foreach (string line in File.ReadAllLines(fileWidthStopWords))
            {
                string[] fromTxt = line.Split(',');
                foreach (string word in fromTxt)
                {
                    stopWords.Add(word.Trim());
                }

            }
            return stopWords.ToArray();
        }


        public void  SaveStopWords(string text)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(baseDirectory, "StopWords.txt");

            using (var writer = new StreamWriter(filePath,false))
            {
                writer.Write(text);
            }
        }


        public List<AnalyzedWordForTable> GenerateWordsTable(string textWithoutStopWords)
        {
            List<AnalyzedWordForTable> result = new List<AnalyzedWordForTable>();

            var textWithoutDuplicates = textWithoutStopWords.ClearParcerResult().TrimFull()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Where(x => x.Length >= 3)
            .Distinct(StringComparer.CurrentCultureIgnoreCase)
            .OrderBy(x => x);

            foreach (string s in textWithoutDuplicates)
            {
                var wordOccurrencesCount = s.CountWordOccurrences(textWithoutStopWords);

                if (wordOccurrencesCount > 0)
                    result.Add(new AnalyzedWordForTable()
                    {
                        Word = s,
                        WordOccurrencesCount = wordOccurrencesCount,
                        PercentWordCount = wordOccurrencesCount.GetPercentWordCount(textWithoutStopWords.WordCount())
                    });
            }
            return result;
        }
    }
}