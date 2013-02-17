using Core.Domain.Model.Users;
using Infrastructure.NHibernate.Repositories;
using NHibernate;
using NHibernate.Proxy;
using NUnit.Framework;

namespace Test.Integration.Infrastructure.NHibernate.Repositories.UserRepositoryTest
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
            var user = Mother.GetUser();
            Repo.Store(user);
            var fetched = Repo.GetByEmail("b@s.com");
            Assert.True(fetched.Id > 0);
            Assert.AreEqual("b@s.com", fetched.Email);
            Assert.AreEqual("brian", fetched.Name);
            Assert.AreEqual(user.Password, fetched.Password);
        }

        [Test]
        public void Get_should_return_user_by_id()
        {
            var user = Mother.GetUser("s@b.com");
            Repo.Store(user);
            var fetched = Repo.Get(1);
            Assert.IsInstanceOf<User>(fetched);
        }
    }
}
