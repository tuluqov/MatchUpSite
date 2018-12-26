using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace PeopleParser
{
    class Program
    {
        private static string TheFamousPeople = "https://www.thefamouspeople.com/famous-people-born-in-january.php";
        private static string Google = "https://www.google.by";
        private static string FamouseBD = "https://www.famousbirthdays.com/most-popular-people.html";
        private static string Square = "http://in-contri.com/numerology-calculator/";

        static void Main(string[] args)
        {
            //Parser parser = new Parser();
            //parser.ParseFirstPage();
            //parser.ParseAthlets();

            ParserSelen parserSelen = new ParserSelen();
            parserSelen.ParseNumLife();
            //parserSelen.CopyText(Square);
        }
    }
}
