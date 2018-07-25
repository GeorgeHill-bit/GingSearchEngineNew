using System;
using Ging.Biz;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ging.UnitTests
{
    [TestClass]
    public class SearchEngineTests
    {
        [TestMethod]
        public void GetAllLinksShouldPass()
        {
            //Arrange
            var searchEngine = CreateEngine();

            //Act
            var results = searchEngine.GetAllLinks();

            //Assert
            Assert.AreEqual(
                "Type: Sponsored Link\n" + "Url: fooshop.com\n" + "Description: some online store\n" + "Impressions: 0",
                results[0]);
            Assert.AreEqual(
                "Type: Sponsored Link\n" + "Url: foostore.com\n" + "Description: probably losing to Amazon\n" + "Impressions: 0",
                results[1]);
            Assert.AreEqual(
                "Type: Sponsored Link\n" + "Url: foolife.com\n" + "Description: definitely losing to Amazon\n" + "Impressions: 0",
                results[2]);
            Assert.AreEqual(
                "Type: Non-Sponsored Link\n" + "Url: fooblog.com\n" + "Description: some blog\n" + "Impressions: 0",
                results[3]);
            Assert.AreEqual(
                "Type: Non-Sponsored Link\n" + "Url: fooforum.com\n" + "Description: some forum\n" + "Impressions: 0",
                results[4]);
            Assert.AreEqual(
                "Type: Non-Sponsored Link\n" + "Url: foobar.com\n" + "Description: something else\n" + "Impressions: 0",
                results[5]);
            Assert.AreEqual(6, results.Count);
        }

        [TestMethod]
        public void SearchForSomeShouldPass()
        {
            //Arrange
            var searchEngine = CreateEngine();

            //Act
            var results = searchEngine.Search("some");

            //Assert
            Assert.AreEqual(
                "SPONSORED\n" + "fooshop.com",
                results[0]);
            Assert.AreEqual("fooblog.com\n" + $"Last Crawled: {DateTime.Today:MM/dd/yyyy}",
                results[1]);
            Assert.AreEqual("fooforum.com\n" + $"Last Crawled: {DateTime.Today:MM/dd/yyyy}",
                results[2]);
            Assert.AreEqual("foobar.com\n" + $"Last Crawled: {DateTime.Today:MM/dd/yyyy}",
                results[3]);
            Assert.AreEqual(4, results.Count);
        }

        [TestMethod]
        public void LessThanFiveInFinancialsShouldPass()
        {
            //Arrange
            var searchEngine = CreateEngine();

            //Act
            searchEngine.Search("amazon");
            var results = searchEngine.GetFinancials();

            //Assert
            Assert.AreEqual(
                "Url: foostore.com\n" + "Sponsor: some dude\n" + "Balance: 0.3",
                results[0]);
            Assert.AreEqual(
                "Url: foolife.com\n" + "Sponsor: some hippie\n" + "Balance: 0.2",
                results[1]);
            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public void MoreThanFiveFinancialsShouldPass()
        {
            //Arrange
            var searchEngine = new SearchEngine();
            searchEngine.AddSponsoredLink(
                new SponsoredLink("fooshop.com", "something", "some guy", 0.25m));
            searchEngine.AddSponsoredLink(
                new SponsoredLink("foostore.com", "something", "some dude", 0.3m));
            searchEngine.AddSponsoredLink(
                new SponsoredLink("foolife.com", "something", "some hippie", 0.2m));
            searchEngine.AddSponsoredLink(
                new SponsoredLink("foobar.com", "something", "some bartender", 0.15m));
            searchEngine.AddSponsoredLink(
                new SponsoredLink("fooblog.com", "something", "some blogger", 0.25m));
            searchEngine.AddSponsoredLink(
                new SponsoredLink("fooforum.com", "something", "some forum admin", 0.4m));

            //Act
            searchEngine.Search("something");
            var results = searchEngine.GetFinancials();

            //Assert
            Assert.AreEqual(5, results.Count);
        }

        [TestMethod]
        public void NoResultsFinancialsShouldPass()
        {
            //Arrange
            var searchEngine = CreateEngine();

            //Act
            var results = searchEngine.GetFinancials();

            //Assert
            Assert.AreEqual(0, results.Count);
        }

        private SearchEngine CreateEngine()
        {
            var searchEngine = new SearchEngine();

            searchEngine.AddNonSponsoredLink(
                new NonSponsoredLink("fooblog.com", "some blog", DateTime.Today, LinkKind.Blog));
            searchEngine.AddNonSponsoredLink(
                new NonSponsoredLink("fooforum.com", "some forum", DateTime.Today, LinkKind.Forum));
            searchEngine.AddNonSponsoredLink(
                new NonSponsoredLink("foobar.com", "something else", DateTime.Today, LinkKind.Forum));
            searchEngine.AddSponsoredLink(
                new SponsoredLink("fooshop.com", "some online store", "some guy", 0.25m));
            searchEngine.AddSponsoredLink(
                new SponsoredLink("foostore.com", "probably losing to Amazon", "some dude", 0.3m));
            searchEngine.AddSponsoredLink(
                new SponsoredLink("foolife.com", "definitely losing to Amazon", "some hippie", 0.2m));

            return searchEngine;
        }
    }
}

