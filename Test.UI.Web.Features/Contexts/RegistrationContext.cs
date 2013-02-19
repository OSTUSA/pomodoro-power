using Infrastructure.NHibernate.Mapping.Users;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using Test.Integration;

namespace Test.UI.Web.Features.Contexts
{
    [Binding]
    public class RegistrationContext
    {
        protected IWebDriver Driver;
        protected string BaseUrl;

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

        [Given(@"I am on ""(.*)""")]
        public void GivenIAmOn(string page)
        {
            Driver.Navigate().GoToUrl(BaseUrl + page);
        }

        [When(@"I fill out the form")]
        public void WhenIFillOutTheForm()
        {
            var email = Driver.FindElement(By.Id("Email"));
            var name = Driver.FindElement(By.Id("Name"));
            var password = Driver.FindElement(By.Id("Password"));
            email.SendKeys("bscaturro@gmail.com");
            name.SendKeys("Brian Scaturro");
            password.SendKeys("password");
        }

        [When(@"I submit it")]
        public void WhenISubmitIt()
        {
            var form = Driver.FindElement(By.CssSelector("form[action='/user/register']"));
            form.Submit();
        }

        [Then(@"A cookie should be created")]
        public void ThenACookieShouldBeCreated()
        {
            var cookie = Driver.Manage().Cookies.GetCookieNamed(".ASPXAUTH");
            Assert.IsNotNull(cookie);
        }

        [Then(@"I should be redirected to the home screen")]
        public void ThenIShouldBeRedirectedToTheHomeScreen()
        {
            Assert.AreEqual(BaseUrl + '/', Driver.Url);
        }

    }
}
