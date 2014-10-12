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

            Uri uri = new Uri(url);

            HtmlParser.HtmlParser.GetWebPageInfo(url, out encoding, out pageSize);

            return new PageInfo
            {
                Host = uri.Host,
                Encoding = encoding,
                Size = pageSize
            };

        }
        public PageContentBlock BuildWebPageTextBlock(string url)
        {

            var pageText = HtmlParser.HtmlParser.GetWebPageContent(url).ClearParcerResult();

            return new PageContentBlock
            {

                OriginalText = pageText,
                OriginalTextCount = pageText.WordCount(),
                TextWithoutStopWords = pageText.RemoveStopWords(GetStopWords()),
                TextWithoutStopWordsCount = pageText.RemoveStopWords(GetStopWords()).WordCount()

            };

        }

        public MetaTagBlock BuildMetaTagBlock(string url, string metaTagName)
        {

            var pageText = HtmlParser.HtmlParser.GetWebPageMetaTagContent(url, metaTagName).ClearParcerResult();

            return new MetaTagBlock
            {

                Name = metaTagName,
                Content = pageText,
                ContentTextCount = pageText.WordCount(),
                ContentWithoutStopWords = pageText.RemoveStopWords(GetStopWords()),
                ContentWithoutStopWordsCount = pageText.RemoveStopWords(GetStopWords()).WordCount()

            };

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
                    stopWords.Add(word);
                }

            }
            return stopWords.ToArray();
        }

      

    }
}