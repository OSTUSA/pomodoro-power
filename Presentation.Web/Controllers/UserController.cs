using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Core.Domain.Model.Users;
using Presentation.Web.Models.Input;
using Presentation.Web.Services;

namespace Presentation.Web.Controllers
{
    public class UserController : Controller
    {
        protected IUserRepository Users { get; set; }

        public UserController(IUserRepository users)
        {
            Users = users;
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterInput input)
        {
            if (!ModelState.IsValid) return View(input);

            var user = new User() {Email = input.Email, Name = input.Name, Password = input.Password};
            user.HashPassword();

            Users.Store(user);

            new Services.AuthenticationService(user).Authenticate(ControllerContext.HttpContext.Response);

            return RedirectToAction("Index", "Home");
        }
    }
}
