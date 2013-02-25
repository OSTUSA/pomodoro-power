using System;
using System.Linq;
using Infrastructure.NHibernate.Mapping.Users;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using Test.Integration;
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
            new DatabaseTestState("TestConnection", "pom-schema.sql").Configure<UserMap>();
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
        public void GivenIAmOnPage(string p0)
        {
            var typeName = String.Format("Test.UI.Web.Features.Pages.{0}", p0);
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
        public void ThenACookieNamedShouldExist(string p0)
        {
            var cookie = Driver.Manage().Cookies.GetCookieNamed(p0);
            Assert.IsNotNull(cookie);
        }

        [Then(@"I should be redirected to ""(.*)""")]
        public void ThenIShouldBeRedirectedTo(string p0)
        {
            Assert.AreEqual(BaseUrl + p0, Driver.Url);
        }

    }
}
