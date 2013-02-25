using System;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using Test.UI.Web.Features.Pages;

namespace Test.UI.Web.Features.Contexts
{
    /// <summary>
    /// A base WebContext for use in all features
    /// Here are places to initialize pages, submit forms,
    /// and test for cookies
    /// </summary>
    [Binding]
    public class WebContext
    {
        protected IWebDriver Driver;
        protected string BaseUrl;
        protected IPage Page;

        [BeforeScenario()]
        public void InitScenario()
        {
            Driver = new FirefoxDriver();
            BaseUrl = "http://localhost:50522";
        }

        [AfterScenario()]
        public void TearDownScenario()
        {
            var authCookie = Driver.Manage().Cookies.GetCookieNamed(".ASPXAUTH");
            if(authCookie != null)
                Driver.Manage().Cookies.DeleteCookie(authCookie);
            Driver.Quit();
        }

        [Given(@"I am on page ""(.*)""")]
        public void GivenIAmOnPage(string relativeType)
        {
            var typeName = String.Format("Test.UI.Web.Features.Pages.{0}", relativeType);
            var type = Type.GetType(typeName);
            Object[] args = {Driver};
            Page = Activator.CreateInstance(type, args) as IPage;
            Page.NavigateTo();
        }

        [When(@"I submit the form using")]
        public void WhenISubmitTheFormUsing(Table table)
        {
            var page = Page as IFormPage;
            page.Submit((from row in table.Rows from kvp in row select kvp.Value).ToArray());
        }

        [Then(@"A cookie named ""(.*)"" should exist")]
        public void ThenACookieNamedShouldExist(string cookieName)
        {
            var cookie = Driver.Manage().Cookies.GetCookieNamed(cookieName);
            Assert.IsNotNull(cookie);
        }

        [Then(@"I should be redirected to ""(.*)""")]
        public void ThenIShouldBeRedirectedTo(string path)
        {
            Assert.AreEqual(BaseUrl + path, Driver.Url);
        }

        [Then(@"element ""(.*)"" should have text")]
        public void ThenElementShouldHaveText(string selector)
        {
            var elem = Driver.FindElement(By.CssSelector(selector));
            Assert.IsNotNull(elem);
            Assert.IsNotEmpty(elem.Text);
        }
    }
}
