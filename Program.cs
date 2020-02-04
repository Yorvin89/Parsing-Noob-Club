

using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace NoobParser
{
    class Program
    {
        public const string WEB_RESOURCE = "https://www.noob-club.ru";

        static void Main(string[] args)
        {
            List<Reference> parserReferences = new List<Reference>();

            HtmlWeb webLoader = new HtmlWeb();
            HtmlDocument htmlDoc = webLoader.Load(WEB_RESOURCE);

            HtmlNodeCollection tableParentNode =
                htmlDoc.GetElementbyId("middle-wrapper")
                    .SelectNodes("//div[@class=\"entry first\"]");

            foreach (HtmlNode node in tableParentNode)
            {
                HtmlNodeCollection nodeChildCollection = node.ChildNodes;
                HtmlNode aTag = node.SelectSingleNode(".//h1/a");
                parserReferences.Add(PrepareReferenceFromNode(aTag));
            }

            Console.WriteLine($"На странице было найдено {parserReferences.Count} значений:\n");
            foreach (Reference parserReference in parserReferences)
            {
                Console.WriteLine($"Название статьи: {parserReference.Description}\nСсылка на статью:{parserReference.Refference}\n________\n");
            }

            Console.ReadLine();
        }

        public static Reference PrepareReferenceFromNode(HtmlNode node)
        {
            string desc = node.InnerText;
            string reference = node.Attributes["href"].Value;

            return new Reference()
            {
                Refference = WEB_RESOURCE + reference,
                Description = desc
            };

        }


    }

    class Reference
    {
        public string Refference { get; set; }
        public string Description { get; set; }
    }
}
