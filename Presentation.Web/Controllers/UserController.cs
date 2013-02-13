using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Domain.Model.Users;
using Presentation.Web.Models.Input;

namespace Presentation.Web.Controllers
{
    public class UserController : Controller
    {
        protected IUserRepository Users { get; set; }

        public UserController(IUserRepository users)
        {
            Users = users;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterInput input)
        {
            if (!ModelState.IsValid) return View(input);

            var user = new User() {Email = input.Email, Name = input.Name, Password = input.Password};
            user.HashPassword();

            Users.Store(user);

            return RedirectToAction("Index", "Home");
        }
    }
}
