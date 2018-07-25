using System;
using Ging.Biz;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ging.UnitTests
{
    [TestClass]
    public class SponsoredLinkTests
    {
        [TestMethod]
        public void SponsoredLinkConstructorShouldPass()
        {
            //Arrange
            var link = new SponsoredLink("foo.com", "something", "some guy", 0.25M);

            //Assert
            Assert.AreEqual("foo.com", link.Url);
            Assert.AreEqual("something", link.Description);
            Assert.AreEqual("some guy", link.Sponsor);
            Assert.AreEqual(0.25M, link.CostPerImpression);
            Assert.AreEqual(0M, link.Balance);
        }
    }
}