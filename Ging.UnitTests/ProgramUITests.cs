using System;
using System.Diagnostics.CodeAnalysis;
using Ging.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ging.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ProgramUITest
    {
        [TestMethod]
        public void ProgramUIAddSponsoredLinkWhenUserFollowsInstructionsShouldPass()
        {
            //Arrange
            var mockConsole = new MockConsole(new string[] { "addlink", "y", "foo.com", "something", "some guy", "0.15" });
            var programUI = new ProgramUI(mockConsole);

            //Act
            programUI.Run();

            //Assert
            var searchEngine = programUI.SearchEngine;
            var outputText = mockConsole.Output;

            StringAssert.Contains(outputText, "a sponsored link?");
            StringAssert.Contains(outputText, "enter the link's url:");

            Assert.AreEqual(
                "Command (addlink, listlinks, search, or financials): " +
                "Called Clear method." +
                "Is this a sponsored link? (y/n): " +
                "Please enter the link's url: " +
                "Please enter a description for the link: " +
                "Please enter the name of the sponsor: " +
                "Please enter the cost per impression: $" +
                "PRESS ENTER TO RETURN TO MENU" +
                "Called Clear method." +
                "Command (addlink, listlinks, search, or financials): ",
                outputText);
        }

        [TestMethod]
        public void ProgramUIAddNonSponsoredLinkWhenUserFollowsInstructionsShouldPass()
        {
            //Arrange
            var mockConsole = new MockConsole(new string[] { "addlink", "n", "foo.com", "something", "06/18/2018", "1" });
            var programUI = new ProgramUI(mockConsole);

            //Act
            programUI.Run();

            //Assert
            var searchEngine = programUI.SearchEngine;
            var outputText = mockConsole.Output;
            Assert.AreEqual(
                "Command (addlink, listlinks, search, or financials): " +
                "Called Clear method." +
                "Is this a sponsored link? (y/n): " +
                "Please enter the link's url: " +
                "Please enter a description for the link: " +
                "Please enter the last date the page was crawled (mm/dd/yyyy): " +
                "1 = forum\n" + "2 = blog\n" + "3 = other\r\n" +
                "Please select a site type: " +
                "PRESS ENTER TO RETURN TO MENU" +
                "Called Clear method." +
                "Command (addlink, listlinks, search, or financials): ",
                outputText);
        }

        [TestMethod]
        public void ProgramUIListLinksWhenUserHasEnteredLinks()
        {
            //Arrange
            var mockConsole = new MockConsole(new string[]
            {
                "addlink", "y", "foo.com", "something", "some guy", "0.15", string.Empty,
                "addlink", "n", "foo.com", "something", "06/18/2018", "1", string.Empty,
                "listlinks"
            });
            var programUI = new ProgramUI(mockConsole);

            //Act
            programUI.Run();

            //Assert
            var searchEngine = programUI.SearchEngine;
            var outputText = mockConsole.Output;
            Assert.AreEqual(
                "Command (addlink, listlinks, search, or financials): " +
                "Called Clear method." +
                "Is this a sponsored link? (y/n): " +
                "Please enter the link's url: " +
                "Please enter a description for the link: " +
                "Please enter the name of the sponsor: " +
                "Please enter the cost per impression: $" +
                "PRESS ENTER TO RETURN TO MENU" +
                "Called Clear method." +
                "Command (addlink, listlinks, search, or financials): " +
                "Called Clear method." +
                "Is this a sponsored link? (y/n): " +
                "Please enter the link's url: " +
                "Please enter a description for the link: " +
                "Please enter the last date the page was crawled (mm/dd/yyyy): " +
                "1 = forum\n" + "2 = blog\n" + "3 = other\r\n" +
                "Please select a site type: " +
                "PRESS ENTER TO RETURN TO MENU" +
                "Called Clear method." +
                "Command (addlink, listlinks, search, or financials): " +
                "Called Clear method." +
                "Type: Sponsored Link\n" +
                "Url: foo.com\n" +
                "Description: something\n" +
                "Impressions: 0\n\r\n" +
                "Type: Non-Sponsored Link\n" +
                "Url: foo.com\n" +
                "Description: something\n" +
                "Impressions: 0\n\r\n" +
                "PRESS ENTER TO RETURN TO MENU" +
                "Called Clear method." +
                "Command (addlink, listlinks, search, or financials): ",
                outputText);
        }

        [TestMethod]
        public void ProgramUISearchWhenUserHasAddedLinksShouldPass()
        {
            //Arrange
            var mockConsole = new MockConsole(new string[]
            {
                "addlink", "y", "foo.com", "something", "some guy", "0.15", string.Empty,
                "addlink", "n", "foo.com", "something", DateTime.Today.ToString("MM/dd/yyyy"), "1", string.Empty,
                "search", "something"
            });
            var programUI = new ProgramUI(mockConsole);

            //Act
            programUI.Run();

            //Assert
            var searchEngine = programUI.SearchEngine;
            var outputText = mockConsole.Output;

            Assert.AreEqual(
                "Command (addlink, listlinks, search, or financials): " +
                "Called Clear method." +
                "Is this a sponsored link? (y/n): " +
                "Please enter the link's url: " +
                "Please enter a description for the link: " +
                "Please enter the name of the sponsor: " +
                "Please enter the cost per impression: $" +
                "PRESS ENTER TO RETURN TO MENU" +
                "Called Clear method." +
                "Command (addlink, listlinks, search, or financials): " +
                "Called Clear method." +
                "Is this a sponsored link? (y/n): " +
                "Please enter the link's url: " +
                "Please enter a description for the link: " +
                "Please enter the last date the page was crawled (mm/dd/yyyy): " +
                "1 = forum\n" + "2 = blog\n" + "3 = other\r\n" +
                "Please select a site type: " +
                "PRESS ENTER TO RETURN TO MENU" +
                "Called Clear method." +
                "Command (addlink, listlinks, search, or financials): " +
                "Called Clear method." +
                "Please enter a search term: " +
                "SPONSORED\n" + "foo.com\n\r\n" +
                "foo.com\n" + $"Last Crawled: {DateTime.Today:MM/dd/yyyy}\n\r\n" +
                "PRESS ENTER TO RETURN TO MENU" +
                "Called Clear method." +
                "Command (addlink, listlinks, search, or financials): ",
                outputText);
        }

        [TestMethod]
        public void ProgramUIFinancialsWhenUserHasAddedLinksShouldPass()
        {
            //Arrange
            var mockConsole = new MockConsole(new string[]
            {
                "addlink", "y", "foo.com", "something", "some guy", "0.15", string.Empty,
                "search", "something", string.Empty,
                "financials"
            });
            var programUI = new ProgramUI(mockConsole);

            //Act
            programUI.Run();

            //Assert
            var searchEngine = programUI.SearchEngine;
            var outputText = mockConsole.Output;
            Assert.AreEqual(
                "Command (addlink, listlinks, search, or financials): " +
                "Called Clear method." +
                "Is this a sponsored link? (y/n): " +
                "Please enter the link's url: " +
                "Please enter a description for the link: " +
                "Please enter the name of the sponsor: " +
                "Please enter the cost per impression: $" +
                "PRESS ENTER TO RETURN TO MENU" +
                "Called Clear method." +
                "Command (addlink, listlinks, search, or financials): " +
                "Called Clear method." +
                "Please enter a search term: " +
                "SPONSORED\n" + "foo.com\n\r\n" +
                "PRESS ENTER TO RETURN TO MENU" +
                "Called Clear method." +
                "Command (addlink, listlinks, search, or financials): " +
                "Called Clear method." +
                "Url: foo.com\n" + "Sponsor: some guy\n" + "Balance: 0.15\n\r\n" +
                "PRESS ENTER TO RETURN TO MENU" +
                "Called Clear method." +
                "Command (addlink, listlinks, search, or financials): ",
                outputText);
        }
    }
}
