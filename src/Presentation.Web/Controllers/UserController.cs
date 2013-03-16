﻿using System.Web.Mvc;
using Core.Domain.Model.Users;
using Presentation.Web.Models.Input;
using Presentation.Web.Services;

namespace Presentation.Web.Controllers
{
    public class UserController : Controller
    {
        protected IUserRepository Users;
        protected IAuthenticationService Auth;

        public UserController(IUserRepository users, IAuthenticationService auth)
        {
            Users = users;
            Auth = auth;
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

            Auth.Authenticate(user, ControllerContext.HttpContext.Response);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogIn(LoginInput input)
        {
            if (!ModelState.IsValid) return View(input);

            var user = Users.GetByEmail(input.Email);

            Auth.Authenticate(user, HttpContext.Response);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public RedirectToRouteResult LogOut()
        {
            Auth.SignOut();
            return RedirectToAction("LogIn");
        }
    }
}