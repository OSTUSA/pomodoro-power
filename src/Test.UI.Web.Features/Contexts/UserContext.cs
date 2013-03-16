using System;
using System.Linq;
using Core.Domain.Model.Users;
using Infrastructure.NHibernate.Mapping.Users;
using Infrastructure.NHibernate.Repositories;
using NHibernate;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using Test.Integration;
using Test.UI.Web.Features.Pages;

namespace Test.UI.Web.Features.Contexts
{
    /// <summary>
    /// A Context dealing with user creation and management
    /// </summary>
    [Binding]
    public class UserContext
    {
        protected static ISessionFactory SessionFactory;

        protected ISession Session;

        [BeforeFeature()]
        public static void InitFeature()
        {
            SessionFactory = new DatabaseTestState("TestConnection", "pom-schema.sql").Configure<UserMap>();
        }

        [Given(@"I create a user with the following")]
        public void GivenICreateAUserWithTheFollowing(Table table)
        {
            var row = table.Rows.First();
            using(var session = SessionFactory.OpenSession())
            {
                var user = new User() {Email = row["Email"], Name = row["Name"], Password = row["Password"]};
                user.HashPassword();
                var repo = new UserRepository(session);
                repo.Store(user);
            }
        }


    }
}
