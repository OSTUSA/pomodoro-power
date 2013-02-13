using System;
using System.Web;
using System.Web.Security;
using Core.Domain.Model.Users;

namespace Presentation.Web.Services
{
    public class AuthenticationService
    {
        public User User { get; set; }

        public AuthenticationService(User user)
        {
            User = user;
        }

        public void Authenticate(HttpResponseBase response)
        {
            var ticket = new FormsAuthenticationTicket(1, User.Email, DateTime.Now, DateTime.Now.AddDays(4), false, string.Empty);
            string encrypted = FormsAuthentication.Encrypt(ticket);
            response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encrypted));
        }
    }
}