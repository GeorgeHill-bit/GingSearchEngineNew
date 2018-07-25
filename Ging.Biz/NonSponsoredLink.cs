using System;

namespace CloudAcademy.CA151.Lab.Ging.Biz
{
    public class NonSponsoredLink : Link
    {
        public LinkKind Kind { get; }

        public DateTime LastCrawl { get; set; }

        public NonSponsoredLink(string url, string description, DateTime lastCrawl, LinkKind kind)
        {
            Url = url;
            Description = description;
            Impressions = 0;
            LastCrawl = lastCrawl;
            Kind = kind;
        }
    }
}
