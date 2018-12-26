using System;
using System.Collections.Generic;
using System.IO;
using HtmlAgilityPack;
using MatchUp.Models.DBModels;
using PeopleParser.DAL;

namespace PeopleParser
{
    public class Parser
    {
        private RepositoryStars Stars = new RepositoryStars();

        private ParserSelen selen = new ParserSelen();

        public List<Star> ParseFirstPage()
        {
            List<Star> stars = new List<Star>();

            var web = new HtmlWeb();
            //var doc = web.Load(TheFamousPeople);
            var doc = new HtmlDocument();
            doc.Load(Path.GetFullPath(@"F:\C#\Compatibility in FB API\PeopleParser\HtmlFiles\12.html"));

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//*[@id=\"relatedDiv\"]/div/div/a[1]");
            HtmlNodeCollection spans = doc.DocumentNode.SelectNodes("//*[@id=\"relatedDiv\"]/div/div/span");
            HtmlNodeCollection dob = doc.DocumentNode.SelectNodes("//*[@id=\"relatedDiv\"]/div/div/p[1]");

            for (int i = 0; i < nodes.Count; i++)
            {
                //Console.WriteLine(nodes[i].FirstChild.GetAttributeValue("src", "null"));
                Console.WriteLine(nodes[i].FirstChild.GetAttributeValue("alt", "Name"));
                Console.WriteLine($"{i}");
                //Console.WriteLine(spans[i].InnerText.Replace("(", "").Replace(")", ""));
                //Console.WriteLine(dob[i].InnerText);
                //Console.WriteLine();

                Star star = new Star
                {
                    Name = nodes[i].FirstChild.GetAttributeValue("alt", "Name"),
                    PhotoUrl = nodes[i].FirstChild.GetAttributeValue("src", ""),
                    Details = spans[i].InnerText.Replace("(", "").Replace(")", ""),
                    Birthday = DateTime.Parse(dob[i].InnerText)
                };

                if (star.Birthday < new DateTime(1850, 1, 1))
                {
                    continue;
                }

                stars.Add(star);
                Stars.Create(star);
            }

            return stars;
        }

        public void ParseForbes()
        {
            List<Star> stars = new List<Star>();

            var web = new HtmlWeb();

            var doc = new HtmlDocument();
            doc.Load(Path.GetFullPath(@"F:\C#\Compatibility in FB API\PeopleParser\HtmlFiles\Forbes.html"));

            for (int i = 53; i <= 100; i++)
            {
                Console.WriteLine($"{i}");

                if (i == 11 || i == 22 || i == 33 || i == 44 ||  i  == 55 || i == 66 || i == 77 || i == 88)
                {
                    continue;
                }

                HtmlNodeCollection img = doc.DocumentNode.SelectNodes($"//*[@id=\"list-table-body\"]/tr[{i}]/td[1]/a/img");
                HtmlNodeCollection name = doc.DocumentNode.SelectNodes($"//*[@id=\"list-table-body\"]/tr[{i}]/td[3]/a");
                HtmlNodeCollection details = doc.DocumentNode.SelectNodes($"//*[@id=\"list-table-body\"]/tr[{i}]/td[6]");

                string imgStr = img[0].GetAttributeValue("src", "");
                string nameStr = name[0].InnerText;
                string detailsStr = details[0].InnerText;
                detailsStr = detailsStr.Substring(0, 1).ToUpper() + (detailsStr.Length > 1 ? detailsStr.Substring(1) : "");

                DateTime dob;

                try
                {
                    dob = selen.FindDob(nameStr);
                }
                catch (Exception)
                {
                    continue;
                }

                Console.WriteLine($"{imgStr}");
                Console.WriteLine($"{nameStr}");
                Console.WriteLine($"{detailsStr}");
                Console.WriteLine($"{dob.ToString("d")}");
                
                Console.WriteLine("--------------------------");

                Star star = new Star
                {
                    Name = nameStr,
                    Details = detailsStr,
                    PhotoUrl = imgStr,
                    Birthday = dob
                };

                Stars.Create(star);
            }
           
        }

        public void ParseAthlets()
        {
            List<Star> stars = new List<Star>();

            var web = new HtmlWeb();

            var doc = new HtmlDocument();
            doc.Load(Path.GetFullPath(@"F:\C#\Compatibility in FB API\PeopleParser\HtmlFiles\Top100Athlets.html"));

            HtmlNodeCollection allElements = doc.DocumentNode.SelectNodes("//*[@id=\"card-container\"]")[0].ChildNodes;

            foreach (HtmlNode el in allElements)
            {
                //Console.WriteLine(el.Id);

                if (el.Id == "")
                {
                    continue;
                }
                else if (el.Id == "ad-block")
                {
                    break;
                }

                HtmlNodeCollection imgElem =
                    doc.DocumentNode.SelectNodes(
                        $"//*[@id=\"{el.Id}\"]/div/div[1]/div[1]/div/div[2]/div/img[1]");

                HtmlNodeCollection name =
                    doc.DocumentNode.SelectNodes(
                        $"//*[@id=\"{el.Id}\"]/div/div[1]/div[2]/div[2]/div[1]");

                HtmlNodeCollection details =
                    doc.DocumentNode.SelectNodes(
                        $"//*[@id=\"{el.Id}\"]/div/div[1]/div[2]/div[2]/div[2]/span[2]");

                DateTime dob;

                try
                {
                    dob = selen.FindDob(name[0].InnerText);
                }
                catch (Exception)
                {
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Green; // устанавливаем цвет
                Console.WriteLine(name[0].InnerText);
                
                Console.WriteLine(dob.ToString("d"));
                Console.ResetColor(); // сбрасываем в стандартный

                Console.WriteLine(imgElem[0].GetAttributeValue("src", ""));
                Console.WriteLine(details[0].InnerText.Substring(0, details[0].InnerText.Length - 5));
                Console.WriteLine();

                Star star = new Star
                {
                    Name = name[0].InnerText,
                    Details = details[0].InnerText.Substring(0, details[0].InnerText.Length - 5),
                    PhotoUrl = imgElem[0].GetAttributeValue("src", ""),
                    Birthday = dob
                };

                Stars.Create(star);
            }
            
        }
    }
}