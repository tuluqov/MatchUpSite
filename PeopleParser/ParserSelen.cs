using System;
using System.Collections.Generic;
using System.Threading;
using MatchUp.Models.DBModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PeopleParser.DAL;

namespace PeopleParser
{
    public class ParserSelen
    {
        private static List<string> months = new List<string>
        {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"
        };

        private IWebDriver Browser;
        private RepositoryDescriptions descriptions = new RepositoryDescriptions();

        public void ParseSite(string siteUrl)
        {
            Browser = new ChromeDriver();

            Browser.Manage().Window.Maximize();

            Browser.Navigate().GoToUrl(siteUrl);

            //((IJavaScriptExecutor)Browser).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 5000)");


            List<IWebElement> peopleNames = new List<IWebElement>(Browser.FindElements(By.ClassName("name")));
            List<IWebElement> peopleDetails = new List<IWebElement>(Browser.FindElements(By.XPath("//*[@id=\"relatedDiv\"]/div")));
            //List<IWebElement> peopleDob = new List<IWebElement>(Browser.FindElements(By.ClassName("hidden-sm")));

            //var peopleElements = Browser.FindElements(By.ClassName("btn-primary btn-sm btn-block"));

            for (int i = 0; i < peopleNames.Count; i++)
            {
                Console.WriteLine(peopleNames[i].Text + " - " + peopleDetails[i].Text);

                peopleNames[i].Click();

                Browser.Navigate().Back();

            }


        }

        public DateTime FindDob(string search)
        {
            Browser = new ChromeDriver();

            Browser.Manage().Window.Maximize();

            //Browser.Navigate().GoToUrl("https://google.gik-team.com/?q=" + search.Replace(" ", "+"));
            Browser.Navigate().GoToUrl("https://www.google.com/search?&rls=en&q=" + "date+of+birth+" + search.Replace(" ", "+"));

            var dobStr = Browser.FindElement(
                By.XPath("//*[@id=\"rso\"]/div[1]/div/div[1]/div/div[1]/div[2]/div[2]/div/div[1]/div/div/div[1]"))
                .Text;

            DateTime dob = DateTime.Parse(dobStr);

            Browser.Close();

            return dob;
        }

        public void ParseNumLife()
        {
            Browser = new ChromeDriver();

            Browser.Manage().Window.Maximize();

            Browser.Navigate().GoToUrl("http://numerologylife.com/pythagorean_square");



            for (int i = 1900; i < 2018; i++)
            {
                var yearEl = Browser.FindElement(By.XPath("//*[@id=\"year\"]"));
                yearEl.SendKeys($"{i}");

                for (int j = 1; j < 12; j++)
                {
                    var monthEl = Browser.FindElement(By.XPath("//*[@id=\"mounth\"]"));
                    monthEl.SendKeys($"{j}");

                    for (int k = 1; k < 31; k++)
                    {
                        var dayEl = Browser.FindElement(By.XPath("//*[@id=\"day\"]"));
                        dayEl.SendKeys($"{k}");

                        var btn = Browser.FindElement(By.XPath("//*[@id=\"buttonBlockSmol\"]/button"));
                        btn.Click();

                        Browser.Navigate().Back();
                    }
                }
            }

        }

        public void CopyText(string url)
        {
            Browser = new ChromeDriver();

            Browser.Manage().Window.Maximize();
            Browser.Navigate().GoToUrl(url);

            for (int k = 76; k <= 106; k++)
            {
                var earElement = Browser.FindElement(By.Id("female_year"));
                earElement.Click();

                var newEar =
                    Browser.FindElement(By.XPath($"/html/body/div[4]/div[2]/div[1]/div[2]/div[3]/ul/li[{k}]"));
                newEar.Click();

                for (int j = 2; j <= 12; j++)
                {
                    var monthElement = Browser.FindElement(By.Id("female_month"));
                    monthElement.Click();

                    var newMonth =
                        Browser.FindElement(By.XPath($"/html/body/div[4]/div[2]/div[1]/div[2]/div[1]/ul/li[{j}]"));
                    newMonth.Click();

                    for (int i = 1; i <= 31; i++)
                    {
                        Console.WriteLine("------------------------------------------");
                        Console.WriteLine($"{i} {months[j - 1]} {1912 + k}");

                        var day = Browser.FindElement(By.Id("female_day"));
                        //var month = Browser.FindElement(By.Id("female_month"));
                        var year = Browser.FindElement(By.Id("female_year"));

                        var btn = Browser.FindElement(By.Id("go-pif"));
                        btn.Click();

                        Thread.Sleep(TimeSpan.FromSeconds(13));

                        //var comments = Browser.FindElements(By.ClassName("comment"));
                        var comments = Browser.FindElements(By.XPath("//*[@id=\"pifagor_primary\"]/div"));

                        foreach (var webElement in comments)
                        {
                            //Console.WriteLine();
                            //Console.WriteLine(webElement.FindElement(By.ClassName("comment-title")).Text.Split('—')[0]);
                            //Console.WriteLine(webElement.FindElement(By.ClassName("pink")).Text);
                            //Console.WriteLine(webElement.Text.Split('\n')[1]);
                            //Console.WriteLine();

                            Description description = new Description
                            {
                                Numbers = webElement.FindElement(By.ClassName("pink")).Text,
                                Name = webElement.FindElement(By.ClassName("comment-title")).Text.Split('—')[0],
                                Details = webElement.Text.Split('\n')[1]
                            };

                            descriptions.Create(description);
                        }

                        IJavaScriptExecutor js = (IJavaScriptExecutor)Browser;
                        js.ExecuteScript("$(document.elementFromPoint(100, 100)).click();");

                        day.Click();
                        var newDay = Browser.FindElement(By.XPath($"/html/body/div[4]/div[2]/div[1]/div[2]/div[2]/ul/li[{i}]"));
                        newDay.Click();
                    }
                }
            }
        }

    }
}