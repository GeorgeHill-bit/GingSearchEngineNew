using System;

namespace Ging.Biz
{
    public class SponsoredLink : Link
    {
        public string Sponsor { get; }

        public decimal CostPerImpression { get; }

        public decimal Balance => CostPerImpression * Impressions;

        public SponsoredLink(string url, string description, string sponsor, decimal cost)
        {
            Url = url;
            Description = description;
            Impressions = 0;
            Sponsor = sponsor;
            CostPerImpression = cost;
        }
    }
}
