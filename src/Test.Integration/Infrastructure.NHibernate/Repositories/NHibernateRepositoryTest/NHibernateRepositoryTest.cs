using System.Collections.Generic;
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
        public void Store_should_update_existing_User()
        {
            var user = Mother.GetUser();
            Repo.Store(user);
            Session.Clear();
            var fetched = Repo.Get(user.Id);
            fetched.Name = "Bryan";
            Repo.Store(fetched);
            Session.Clear();
            var updated = Repo.Get(fetched.Id);
            Assert.AreEqual("Bryan", updated.Name);
        }

        [Test]
        public void FindBy_should_return_list_of_matching_criteria()
        {
            var user = Mother.GetUser();
            var user2 = Mother.GetUser("s@b.com", "scaturro");
            Repo.Store(user);
            Repo.Store(user2);
            Session.Clear();
            var found = Repo.FindBy(x => x.Name == "brian");
            Assert.AreEqual(1, found.Count);
            Assert.AreEqual("brian", found[0].Name);
        }

        [Test]
        public void Get_should_return_user_by_id()
        {
            var user = Mother.GetUser("s@b.com");
            Repo.Store(user);
            Session.Clear();
            var fetched = Repo.Get(user.Id);
            Assert.IsInstanceOf<User>(fetched);
        }

        [Test]
        public void Get_should_return_null_if_user_not_found()
        {
            var fetched = Repo.Get((long)-1);
            Assert.IsNull(fetched);
        }

        [Test]
        public void FindBy_should_return_empty_list_if_no_items_found()
        {
            var fetched = Repo.FindBy(x => x.Email == "doesntexist@email.com");
            CollectionAssert.IsEmpty(fetched);
        }

        [Test]
        public void GetAll_should_return_empty_list_if_no_records()
        {
            var fetched = Repo.GetAll();
            CollectionAssert.IsEmpty(fetched);
        }

        [Test]
        public void GetAll_should_return_list_of_all_records()
        {
            var user = Mother.GetUser();
            var user2 = Mother.GetUser("j@s.com", "jennie");
            Repo.Store(user);
            Repo.Store(user2);
            Session.Clear();
            var all = Repo.GetAll();
            Assert.AreEqual(2, all.Count);
            Assert.AreEqual("brian", all[0].Name);
            Assert.AreEqual("jennie", all[1].Name);
        }

        [Test]
        public void Delete()
        {
            var user = Mother.GetUser();
            var user2 = Mother.GetUser("j@s.com");
            Repo.Store(user);
            Repo.Store(user2);
            Session.Clear();
            var brian = Repo.Get((long)1);
            Repo.Delete(brian);
            var all = Repo.GetAll();
            Assert.AreEqual(1, all.Count);
        }
    }
}
