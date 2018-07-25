using System;
using Ging.Biz;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ging.UnitTests
{
    [TestClass]
    public class NonSponsoredLinkTests
    {
        [TestMethod]
        public void NonSponsoredLinkConstructorShouldPass()
        {
            //Arrange
            var link = new NonSponsoredLink("foo.com", "something", DateTime.Today, LinkKind.Blog);

            //Assert
            Assert.AreEqual("foo.com", link.Url);
            Assert.AreEqual("something", link.Description);
            Assert.AreEqual(DateTime.Today, link.LastCrawl);
            Assert.AreEqual(LinkKind.Blog, link.Kind);
        }
    }
}