using Core.Domain.Model.Users;
using NUnit.Framework;

namespace Test.Integration.Infrastructure.NHibernate.Repositories.NHibernateRepositoryTest
{
    [TestFixture]
    public class NHibernateRepositoryTest : RepositoryTestBase<User>
    {
        [Test]
        public void Store_should_persist_new_User()
        {
            var user = Mother.GetUser();
            Repo.Store(user);
            var fetched = Repo.FindOneBy(u => u.Email == "b@s.com");
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
            var fetched = Repo.Get((long)1);
            Assert.IsInstanceOf<User>(fetched);
        }
    }
}
