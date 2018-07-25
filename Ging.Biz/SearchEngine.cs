using System;
using System.Collections.Generic;

namespace CloudAcademy.CA151.Lab.Ging.Biz
{
    public class SearchEngine
    {
        private List<SponsoredLink> SponsoredLinks { get; set; }
        private List<NonSponsoredLink> NonSponsoredLinks { get; set; }

        public SearchEngine()
        {
            SponsoredLinks = new List<SponsoredLink>();
            NonSponsoredLinks = new List<NonSponsoredLink>();
        }

        public void AddSponsoredLink(SponsoredLink link)
        {
            SponsoredLinks.Add(link);
        }

        public void AddNonSponsoredLink(NonSponsoredLink link)
        {
            NonSponsoredLinks.Add(link);
        }

        public List<string> GetAllLinks()
        {
            var links = new List<string>();

            foreach (var link in SponsoredLinks)
            {
                links.Add("Type: Sponsored Link\n" +
                                  $"Url: {link.Url}\n" +
                                  $"Description: {link.Description}\n" +
                                  $"Impressions: {link.Impressions}");
            }

            foreach (var link in NonSponsoredLinks)
            {
                links.Add("Type: Non-Sponsored Link\n" +
                                  $"Url: {link.Url}\n" +
                                  $"Description: {link.Description}\n" +
                                  $"Impressions: {link.Impressions}");
            }

            return links;
        }

        public List<string> Search(string searchCondition)
        {
            var searchLinks = new List<string>();

            foreach (var link in SponsoredLinks)
            {
                if (link.Description.ToLower().Contains(searchCondition.ToLower()))
                {
                    searchLinks.Add("SPONSORED\n" +
                                    $"{link.Url}");
                    link.Impressions++;
                }
            }

            foreach (var link in NonSponsoredLinks)
            {
                if (link.Description.Contains(searchCondition))
                {
                    searchLinks.Add($"{link.Url}\n" +
                                    $"Last Crawled: {link.LastCrawl:MM/dd/yyyy}");

                    link.Impressions++;
                    link.LastCrawl = DateTime.Today;
                }
            }

            return searchLinks;
        }

        public List<string> GetFinancials()
        {
            var financials = new List<string>();

            foreach (var link in SponsoredLinks)
            {
                if (link.Balance > 0)
                {
                    financials.Add($"Url: {link.Url}\n" +
                                   $"Sponsor: {link.Sponsor}\n" +
                                   $"Balance: {link.Balance}");
                }

                if (financials.Count == 5)
                {
                    break;
                }
            }

            return financials;
        }
    }
}
