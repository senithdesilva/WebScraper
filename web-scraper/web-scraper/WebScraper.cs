using HtmlAgilityPack;
using System;
using System.IO;
using System.Linq;

namespace web_scraper
{
    public class WebScraper
    {
        public static void Scraper(string urlAddress, string filePath, string fileName)
        {
            // Output details to a text file using StreamWriter class
            StreamWriter streamWriter = new StreamWriter(string.Format(@"{0}\{1}.txt", filePath, fileName), true);

            //WebScraper webScraper = new WebScraper();
            HtmlDocument htmlDocument = new HtmlDocument();

            string urlResponse = WebRequestGet.URLRequest(urlAddress);

            //Convert the Raw HTML into an HTML Object
            htmlDocument.LoadHtml(urlResponse);

            //Find all title tags in the document
            /*
            <head>
                <title>Page Title</title>
            </head>
             */
            var titleNodes = htmlDocument.DocumentNode.SelectNodes("//title");

            if(titleNodes != null)
            {
                streamWriter.WriteLine("The title of the page is:");
                streamWriter.WriteLine(titleNodes.FirstOrDefault().InnerText);
            }
            streamWriter.WriteLine("====================================================================================");
            //Find all keywords tags in the document
            //<meta name="keywords" content="HTML, CSS, XML, JavaScript">
            //- [translate] converts upper case letters to lower in cases where the author used Keywords, KEYWORDS, keywords or other
            var keywwordNodes = htmlDocument.DocumentNode.SelectNodes("//meta[translate(@name, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz') = 'keywords']");

            if (keywwordNodes != null)
            {
                foreach (var keywordNode in keywwordNodes)
                {
                    string content = keywordNode.GetAttributeValue("content", "");
                    if (!string.IsNullOrEmpty(content))
                    {
                        string[] keywords = content.Split(new string[] { "," }, StringSplitOptions.None);
                        streamWriter.WriteLine("Here are the keywords:");
                        streamWriter.WriteLine(string.Format("{0}", content));

                        foreach (var keyword in keywords)
                        {
                            streamWriter.WriteLine(string.Format("\t- {0}", keyword));
                        }
                    }

                }
            }
            streamWriter.WriteLine("====================================================================================");
            //Find all A tags in the document
            var anchorNodes = htmlDocument.DocumentNode.SelectNodes("//a");

            if (anchorNodes != null)
            {
                streamWriter.WriteLine(string.Format("We found {0} anchor tags on this page. Here is the text from those tags:", anchorNodes.Count));

                foreach (var anchorNode in anchorNodes)
                {
                    streamWriter.WriteLine(string.Format("{0} - {1}", anchorNode.InnerText, anchorNode.GetAttributeValue("href", "")));
                }
            }
        }
    }
}