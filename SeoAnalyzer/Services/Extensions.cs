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

        public static int WordCount(this string s)
        {
            int last = s.Length - 1;

            int count = 0;
            for (int i = 0; i <= last; i++)
            {
                if (char.IsLetterOrDigit(s[i]) &&
                     ((i == last) || char.IsWhiteSpace(s[i + 1]) || char.IsPunctuation(s[i + 1])))
                    count++;
            }
            return count;
        }
   
        public static string ClearParcerResult(this string resultText)
        {
            return Regex.Replace(resultText, @"<[^>]*(>|$)|&nbsp;|&zwnj;|&raquo;|&laquo;|[^\w\s\-\.\(\)]|[\-+]|[\.)(]|[\s+]", " ").ToLower().Trim();
        }
    }
}