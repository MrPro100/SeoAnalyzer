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
            var hw = new HtmlWeb();

            HtmlDocument doc = hw.Load(url);

            StringBuilder sb = new StringBuilder();
            IEnumerable<HtmlNode> nodes = doc.DocumentNode.Descendants().Where(n =>
                n.NodeType == HtmlNodeType.Text &&
                n.ParentNode.Name != "script" &&
                n.ParentNode.Name != "head" &&
                n.ParentNode.Name != "title" &&
                n.ParentNode.Name != "style");
            foreach (HtmlNode node in nodes)
            {
                sb.Append(node.InnerText);
            }

            return sb.ToString();
        }

        public static string GetWebPageMetaTagContent(string url, string name)
        {
            var Webget = new HtmlWeb();
            var doc = Webget.Load(url);
            HtmlNode metaNode = doc.DocumentNode.SelectSingleNode(string.Format("//meta[@name='{0}']", name));
            if (metaNode != null)
            {
                return metaNode.GetAttributeValue("content", "");
            }
            else
            {
                return string.Empty;
            }
        }

        public static void GetWebPageInfo(string url, out string encoding, out int pageSize)
        {
            var hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(url);

            encoding = doc.Encoding.WebName;

            pageSize = Encoding.Default.GetByteCount(doc.DocumentNode.InnerHtml) / 1024;
        }

        public static string GetWebPageTitle(string url)
        {
            var Webget = new HtmlWeb();
            var doc = Webget.Load(url);
            HtmlNode titleNode = doc.DocumentNode.SelectSingleNode("//title");
            if (titleNode != null)
            {
                return titleNode.InnerText;
            }
            else
            {
                return string.Empty;
            }
        }

        public static List<dynamic> GetWebPageLinks(string url)
        {
            var webGet = new HtmlWeb();
            var document = webGet.Load(url);
            var linksOnPage = from lnks in document.DocumentNode.Descendants()
                              where lnks.Name == "a" &&
                                   lnks.Attributes["href"] != null &&
                                   lnks.InnerText.Trim().Length > 0 &&
                                   lnks.ParentNode.Name != "script" &&
                                   lnks.ParentNode.Name != "head" &&
                                   lnks.ParentNode.Name != "style" &&
                                   lnks.InnerText.Trim().Length > 22

                              select new 
                              {
                                  Url = lnks.Attributes["href"].Value,
                                  Text = lnks.InnerText
                              };

            return linksOnPage.ToList<dynamic>();
        }
    }
}

