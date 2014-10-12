using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace SeoAnalyzer.HtmlParser
{
    public class HtmlParser
    {
        public static string GetWebPageContent(string url)
        {
            if (!url.ToUpper().StartsWith("HTTP://"))
            {
                url = "http://" + url;
            }

            var hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(url);


           var body = doc.DocumentNode.SelectSingleNode("//body");



           body.InnerHtml.Replace("<!--", "").Replace("-->", "");

 
            StringBuilder sb = new StringBuilder();
            IEnumerable<HtmlNode> nodes = body.Descendants().Where(n =>
                n.NodeType == HtmlNodeType.Text &&
                n.ParentNode.Name != "script" &&
                n.ParentNode.Name != "head" &&
                n.ParentNode.Name != "title" &&
                    //n.ParentNode.Name != "a" &&
                n.ParentNode.Name != "style");
            foreach (HtmlNode node in nodes)
            {

                //sb.Append(Regex.Replace(node.InnerText, @"<[^>]*(>|$)|&nbsp;|&zwnj;|&raquo;|&laquo;|[^\w\s\-\.\(\)]|[\-+]|[\.)(]", " "));
                sb.Append(node.InnerText);
            }

            return sb.ToString();

        }

        public static string GetWebPageMetaTagContent(string url, string name)
        {
            if (!url.ToUpper().StartsWith("HTTP://"))
            {
                url = "http://" + url;
            }

            //string.Format
            var Webget = new HtmlWeb();
            var doc = Webget.Load(url);
            HtmlNode ourNode = doc.DocumentNode.SelectSingleNode(string.Format("//meta[@name='{0}']", name));
            if (ourNode != null)
            {
                //return Regex.Replace(ourNode.GetAttributeValue("content", ""), @"<[^>]*(>|$)|&nbsp;|&zwnj;|&raquo;|&laquo;|[^\w\s\-\.\(\)]|[\-+]|[\.)(]", " ");

                return ourNode.GetAttributeValue("content", "");
            }
            else
            {
                return "not fount";
            }

        }

        public static void GetWebPageInfo (string url, out string encoding, out int pageSize)
        {
                     if (!url.ToUpper().StartsWith("HTTP://"))
            {
                url = "http://" + url;
            }

            var hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(url);


            encoding = doc.Encoding.WebName;

            pageSize = Encoding.Default.GetByteCount(doc.DocumentNode.InnerHtml) / 1024;

        }
    }
}

