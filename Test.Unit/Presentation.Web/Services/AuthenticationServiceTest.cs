using System.Web;
using Core.Domain.Model.Users;
using Moq;
using NUnit.Framework;
using Presentation.Web.Services;

namespace Test.Unit.Presentation.Web.Services
{
    [TestFixture]
    public class AuthenticationServiceTest
    {
        protected User User { get; set; }

        protected AuthenticationService Service { get; set; }

        [SetUp]
        public void Init()
        {
            User = new User() { Email = "e@a.com", Name = "Brian", Password = "password" };

            Service = new AuthenticationService(User);
        }

        [Test]
        public void Constructor_should_set_user()
        {
            Assert.AreSame(User, Service.User);
        }

        [Test]
        public void Authenticate_should_set_cookie()
        {
            var response = new Mock<HttpResponseBase>();
            var collection = new HttpCookieCollection();
            response.SetupGet(x => x.Cookies).Returns(collection);
            Service.Authenticate(response.Object);
            Assert.AreEqual(1, collection.Count);
        }
    }
}
