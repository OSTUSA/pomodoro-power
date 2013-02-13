using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Core.Domain.Model.Users;
using Moq;
using NUnit.Framework;
using Presentation.Web.Controllers;
using Presentation.Web.Models.Input;

namespace Test.Unit.Presentation.Web.Controllers
{
    [TestFixture]
    public class UserControllerTest
    {
        protected UserController Controller { get; set; }

        protected Mock<IUserRepository> Repo { get; set; }

        protected Mock<HttpContextBase> Context { get; set; }

        protected Mock<HttpResponseBase> Response { get; set; }

        [SetUp]
        public void Init()
        {
            Repo = new Mock<IUserRepository>();
            Controller = new UserController(Repo.Object);
            Context = new Mock<HttpContextBase>(MockBehavior.Strict);
            Response = new Mock<HttpResponseBase>();
            Response.SetupGet(x => x.Cookies).Returns(new HttpCookieCollection());
            Context.SetupGet(x => x.Response).Returns(Response.Object);
            Controller.ControllerContext = new ControllerContext(Context.Object, new RouteData(), Controller);
        }

        [Test]
        public void Register_should_return_a_ViewResult()
        {
            var result = Controller.Register();
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Register_should_return_ViewResult_if_model_has_error()
        {
            Controller.ModelState.AddModelError("Email", "Email is required");
            var result = Controller.Register(new RegisterInput());
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Register_should_return_RedirectToRouteResult_if_input_is_valid()
        {
            var result =
                Controller.Register(new RegisterInput()
                    {
                        Email = "scaturrob@gmail.com",
                        Name = "Brian",
                        Password = "password"
                    });
            Assert.IsInstanceOf<RedirectToRouteResult>(result);
        }

        [Test]
        public void Register_with_valid_model_should_store_new_user()
        {
            var input = new RegisterInput() {Email = "m@e.com", Password = "p", Name = "n"};
            Controller.Register(input);
            Repo.Verify(x => x.Store(It.IsAny<User>()));
        }
    }
}
