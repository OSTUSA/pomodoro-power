using Core.Domain.Model.Users;
using Infrastructure.NHibernate.Repositories;
using NHibernate;
using NUnit.Framework;

namespace Test.Integration.Infrastructure.NHibernate.Repositories
{
    [TestFixture]
    public class UserRepositoryTest : RepositoryTestBase
    {
        protected IUserRepository Repo;

        protected ISession Session;

        [SetUp]
        public void Init()
        {
            Session = Builder.GetFactory("testFactory", BuildTestFactory).OpenSession();
            Session.FlushMode = FlushMode.Commit;
            Repo = new UserRepository(Session);
        }

        [TearDown]
        public void Cleanup()
        {
            Session.Dispose();
        }

        [Test]
        public void Store_should_persist_new_User()
        {
            var user = new User() {Email = "b@s.com", Name = "brian", Password = "pass"};
            user.HashPassword();
            Repo.Store(user);
            var fetched = Repo.GetByEmail("b@s.com");
            Assert.True(fetched.Id > 0);
            Assert.AreEqual("b@s.com", fetched.Email);
            Assert.AreEqual("brian", fetched.Name);
            Assert.AreEqual(user.Password, fetched.Password);
        }
    }
}
