using BingSearchTestFramework.Assertions;
using BingSearchTestFramework.Configuration;
using BingSearchTestFramework.Pages;
using BoDi;
using TechTalk.SpecFlow;

namespace BingWebSearchWithSpecFlow.Steps
{
    [Binding]
    public class CurrentLocationSearchSteps : Steps
    {
        private BingSearchPage SearchPage => Retrieve<BingSearchPage>();
        private string CurrentLocation { get; set; }
        private BingSearchResultPage ResultPage { get; set; }

        public CurrentLocationSearchSteps(IObjectContainer container) 
            : base(container, BrowserHost.EdgeChromium)
        { }

        [When(@"the user select the current pin location")]
        public void WhenTheUserSelectTheCurrentPinLocation()
        {
            CurrentLocation = SearchPage.CurrentPinLocation;
            ResultPage = SearchPage.SelectCurrentPinLocation();
        }

        [Then(@"the results related to the background's image location should be listed")]
        public void ThenTheResultsRelatedToTheBackgroundSImageLocationShouldBeListed()
        {
            ResultPage.Should().SearchMatches(CurrentLocation).And.HaveMoreThan(1);
        }

       

    }
}
