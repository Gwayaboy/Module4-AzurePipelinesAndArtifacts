using BingSearchTestFramework.Configuration;
using BingSearchTestFramework.Pages;
using BoDi;
using OpenQA.Selenium;
using System;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace BingWebSearchWithSpecFlow.Steps
{
    public class Steps 
    {
        protected IObjectContainer Container { get; private set; }


        private readonly Func<BrowserHost> _browserHostFactory;

        public Steps(IObjectContainer container, Func<BrowserHost> webDriverFactory)
        {
            Container = container;
            _browserHostFactory = webDriverFactory;
        }


        [BeforeScenario]
        public void RegisterDefaultBrowserFactory()
        {
            if (!Container.IsRegistered<BrowserHost>())
            {
                Container.RegisterFactoryAs(BrowserFactory);
            }
        }

        [AfterScenario]
        public void DisposeBrowser()
        {
            if (Container.IsRegistered<BrowserHost>())
            {
                Container.Resolve<BrowserHost>().Dispose();
            }
        }

        protected TPage NavigateToInitial<TPage>(string startUpUrl) where TPage : Page, new()
        {
            var browserHost = Container.Resolve<BrowserHost>();
            return Register(browserHost.NavigateToInitial<TPage>(startUpUrl));
        }

        protected TPage Register<TPage>(TPage page) where TPage : Page, new()
        {
            Container.RegisterInstanceAs(page);
            return page;
        }

        protected TPage Retrieve<TPage>() where TPage : Page, new()
        {
            return Container.Resolve<TPage>();
        }

        protected void NotImplemented()
        {
            Container.Resolve<ScenarioContext>().Pending();
        }

        private BrowserHost BrowserFactory(IObjectContainer container)
        {
            return _browserHostFactory();
        }

        [StepArgumentTransformation]
        public int ToCardinal(string ordinal)
        {
            var value = new Regex(@"\d+").Match(ordinal).Value;

            if (string.IsNullOrWhiteSpace(value))
            {
                return 1;
            }

            return int.Parse(value);
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {

            if (!disposedValue)
            {
                if (disposing)
                {

                    if (Container.IsRegistered<IWebDriver>())
                    {
                        var webDriver = Container.Resolve<IWebDriver>();
                        webDriver.Quit();
                        webDriver.Dispose();
                    }
                }
                disposedValue = true;
            }
        }


        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion


    }
}
