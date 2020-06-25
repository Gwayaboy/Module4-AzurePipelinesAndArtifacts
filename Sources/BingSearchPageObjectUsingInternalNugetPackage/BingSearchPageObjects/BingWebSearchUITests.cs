using BingSearchPageObjectsLab.Configuration;
using BingSearchTestFramework.Assertions;
using BingSearchTestFramework.Pages;
using BingSearchTestFramework.ViewModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BingSearchPageObjectsLab
{
    [TestClass]
    [TestCategory("Selenium")]
    public class BingWebSearchUITests : WebUISpecs
    {
        [TestCategory("BingTextSearch")]
        [TestMethod]
        public void EmptySearchShouldNotTriggerAnySearch()
        {
            //Arrange
            var searchPage = Browser.NavigateToInitial<BingSearchPage>("https://bing.com");

            // Act
            var returnedPage = searchPage.Search();

            // Assertions
            returnedPage                
                .Should()
                .ContainsTitle("Bing")
                .And
                .NotContainInUrl("search?q=");
        }

        [TestCategory("BingLocationSearch")]
        [TestMethod]
        public void SelectingTheLocationPinShouldSearchAndReturnSeveralResultsRelatedToLocation()
        {
            //Arrange
            var searchPage = Browser.NavigateToInitial<BingSearchPage>("https://bing.com");
            var currentLocation = searchPage.CurrentPinLocation;

            // Act
            var returnedPage = searchPage.SelectCurrentPinLocation();

            // Assertions
            returnedPage.Should().HaveMoreThan(1).And.SearchMatches(currentLocation);
        }

        [TestCategory("BingTextSearch")]
        [TestMethod]
        public void HelloWordSeachFirstResultShouldBeHelloWordProgram()
        {
            // Arrange
            var searchPage = Browser.NavigateToInitial<BingSearchPage>("https://bing.com");
            var wikipediaResultItem = new ResultItem("\"Hello, World!\" program - Wikipedia", 
                                                     "https://en.wikipedia.org/wiki/%22Hello,_World!%22_program");

            // Act
            var returnedPage = searchPage.TypeSearchText("Hello World!").Search();

            // Assertions
            returnedPage.Should().HaveItemResult(1, wikipediaResultItem);
              
        }
    }
}
