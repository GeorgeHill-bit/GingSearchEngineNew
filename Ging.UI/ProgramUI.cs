using System;
using System.Collections.Generic;
using System.Globalization;
using Ging.Biz;

namespace Ging.UI
{
    public class ProgramUI
    {
        private readonly IConsole _console;

        public SearchEngine SearchEngine { get; }

        public ProgramUI(IConsole consoleForAllReadsAndWrites)
        {
            _console = consoleForAllReadsAndWrites;
            SearchEngine = new SearchEngine();
        }

        public void Run()
        {
            var finished = false;
            do
            {
                _console.Write("Command (addlink, listlinks, search, or financials): ");
                var command = _console.ReadLine().ToLower();

                if (string.IsNullOrWhiteSpace(command))
                {
                    finished = true;
                }
                else if (command == "addlink")
                {
                    AddLink();
                    FinishIteration();
                }
                else if (command == "listlinks")
                {
                    ListLinks();
                    FinishIteration();
                }
                else if (command == "search")
                {
                    Search();
                    FinishIteration();
                }
                else if (command == "financials")
                {
                    Financials();
                    FinishIteration();
                }
            }
            while (!finished);
        }

        private void AddLink()
        {
            _console.Clear();

            var input = string.Empty;

            while (input != "y" && input != "n")
            {
                _console.Write("Is this a sponsored link? (y/n): ");
                input = _console.ReadLine()?.ToLower();

                if (input != "y" && input != "n")
                {
                    _console.WriteLine("Please enter a valid input.");
                }
            }

            var isSponsored = input == "y";

            _console.Write("Please enter the link's url: ");
            var url = _console.ReadLine();

            _console.Write("Please enter a description for the link: ");
            var description = _console.ReadLine();

            if (isSponsored)
            {
                AddSponsoredLink(url, description);
            }

            else
            {
                AddNonSponsoredLink(url, description);
            }
        }

        private void AddSponsoredLink(string url, string description)
        {
            var input = string.Empty;

            _console.Write("Please enter the name of the sponsor: ");
            var sponsor = _console.ReadLine();

            decimal cost;
            while (!decimal.TryParse(input, out cost))
            {
                _console.Write("Please enter the cost per impression: $");
                input = _console.ReadLine();

                if (!decimal.TryParse(input, out cost))
                {
                    _console.WriteLine("Please enter a valid cost.");
                }
            }

            SearchEngine.AddSponsoredLink(new SponsoredLink(url, description, sponsor, cost));
        }

        private void AddNonSponsoredLink(string url, string description)
        {
            var input = string.Empty;

            var lastCrawl = new DateTime();

            while(!DateTime.TryParseExact(input, "MM/dd/yyyy", new CultureInfo("en-us"), DateTimeStyles.None, out lastCrawl))
            {
                _console.Write("Please enter the last date the page was crawled (mm/dd/yyyy): ");
                input = _console.ReadLine();

                if (!DateTime.TryParseExact(input, "MM/dd/yyyy", new CultureInfo("en-us"), DateTimeStyles.None,
                    out lastCrawl))
                {
                    _console.WriteLine("Please enter a valid date.");
                }
            }

            _console.WriteLine("1 = forum\n" +
                              "2 = blog\n" +
                              "3 = other");
            while (input != "1" && input != "2" && input != "3")
            {
                _console.Write("Please select a site type: ");
                input = _console.ReadLine();

                if (input != "1" && input != "2" && input != "3")
                {
                    _console.WriteLine("Please enter a valid type.");
                }
            }

            var linkKind = (LinkKind)int.Parse(input);

            SearchEngine.AddNonSponsoredLink(new NonSponsoredLink(url, description, lastCrawl, linkKind));
        }

        private void ListLinks()
        {
            _console.Clear();
            PrintLinks(SearchEngine.GetAllLinks());
        }

        private void Search()
        {
            _console.Clear();

            _console.Write("Please enter a search term: ");
            var input = _console.ReadLine();

            var searchResults = SearchEngine.Search(input);

            PrintLinks(searchResults);
        }

        private void Financials()
        {
            _console.Clear();
            PrintLinks(SearchEngine.GetFinancials());
        }

        private void PrintLinks(List<string> toPrint)
        {
            foreach (var link in toPrint)
            {
                _console.WriteLine(link + "\n");
            }
        }

        private void FinishIteration()
        {
            _console.Write("PRESS ENTER TO RETURN TO MENU");
            _console.ReadLine();
            _console.Clear();
        }
    }
}
