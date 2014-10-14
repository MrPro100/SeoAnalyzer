using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace SeoAnalyzer.Extensions
{
    public static class Extensions
    {
        public static string RemoveStopWords(this string initialText, string[] stopWords)
        {
            string pattern = " (" + string.Join("|", stopWords) + ") ";
            string cleaned = Regex.Replace(initialText, pattern, " ");
            return cleaned;
        }

        public static int WordCount(this string text)
        {
            int last = text.Length - 1;

            int count = 0;
            for (int i = 0; i <= last; i++)
            {
                if (char.IsLetterOrDigit(text[i]) &&
                     ((i == last) || char.IsWhiteSpace(text[i + 1]) || char.IsPunctuation(text[i + 1])))
                    count++;
            }
            return count;
        }

        public static string ClearParcerResult(this string resultText)
        {
            return Regex.Replace(resultText, @"<[^>]*(>|$)|&nbsp;|&zwnj;|&raquo;|&laquo;|[^\w\s\-\.\(\)]|[\-+]|[\.)(]|[\s+]", " ").ToLower().Trim();
        }

        public static Uri GetUri(this string url)
        {
            return new UriBuilder(url).Uri;
        }


        public static int CountWordOccurrences(this string word, string pageText)
        {

            return new Regex(word).Matches(pageText).Count;
        }

        public static decimal GetPercentWordCount(this int wordCount, int pageTextCount)
        {
            if (pageTextCount == 0) return 0;

            return Math.Round((wordCount / (pageTextCount / 100M)), 4);      
        }
    }
}