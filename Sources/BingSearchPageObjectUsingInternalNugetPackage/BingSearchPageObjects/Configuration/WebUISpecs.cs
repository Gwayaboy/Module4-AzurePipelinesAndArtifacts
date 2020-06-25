using System;
using BingSearchTestFramework.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BingSearchPageObjectsLab.Configuration
{
    public class WebUISpecs 
    {
        private Func<BrowserHost> _browserHostFactory;

        public BrowserHost Browser { get; private set; }
        public WebUISpecs() : this(BrowserHost.Chrome) { }

        protected WebUISpecs(Func<BrowserHost> browerHostFactory)
        {
            _browserHostFactory = browerHostFactory;
        }

        [TestInitialize]
        public void StartBrowser()
        {
            Browser = _browserHostFactory();
        }

        [TestCleanup]
        public void CloseAndDisposeBrowser()
        {
            Browser.Dispose();
            Browser = null;
        }


    }
}
