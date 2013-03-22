using System.Collections.Generic;
using Infrastructure.ListRepositories;
using Infrastructure.ListRepositories.Proxy.Users;
using NUnit.Framework;

namespace Test.Unit.Infrastructure.ListRepositories
{
    [TestFixture]
    public class ListUserRepositoryTest
    {
        protected List<User> Users { get; set; }

        protected ListUserRepository Repo { get; set; }

        [SetUp]
        public void Init()
        {
            Users = new List<User>()
                {
                    new User() {Email = "scaturrob@gmail.com", Name = "Brian Scaturro", Password = "password"},
                    new User() {Email = "bscaturro@gmail.com", Name = "Scaturro Brian", Password = "password"},
                    new User() {Email = "myotheremail@gmail.com", Name = "Bill User", Password = "password"}
                };
            for (int i = 1; i < Users.Count + 1; i++ )
                Users[i - 1].SetId(i);
            Repo = new ListUserRepository();
            foreach(var user in Users)
                Repo.Store(user);
        }

        [TearDown]
        public void TearDown()
        {
            Repo.Clear();
        }

        [Test]
        public void Get_should_return_element_containing_id()
        {
            var user = Repo.Get((long) 1);
            Assert.AreSame(Users[0], user);
        }

        [Test]
        public void Get_should_return_null_if_id_not_present()
        {
            var user = Repo.Get((long)4);
            Assert.IsNull(user);
        }

        [Test]
        public void Store_should_add_new_User_to_internal_list()
        {
            var user = new User() {Email = "myemail@email.com", Name = "Sam User", Password = "mypassword"};
            user.SetId(4);
            Repo.Store(user);
            Assert.AreSame(user, Repo.Get((long)4));
        }

        [Test]
        public void Store_should_not_add_existing_user()
        {
            var user = Repo.Get((long)1);
            user.Name = "An Updated Name";
            Repo.Store(user);
            Assert.AreEqual("An Updated Name", Repo.Get((long)1).Name);
        }

        [Test]
        public void Store_should_increment_id_if_it_does_not_exist()
        {
            var user = new User() {Email = "sam@email.com", Name = "Sam Guy", Password = "mypass"};
            Repo.Store(user);
            var fetched = Repo.Get((long)4);
            Assert.AreEqual("sam@email.com", fetched.Email);
        }

        [Test]
        public void GetByEmail_should_return_a_single_user_by_email()
        {
            var user = Repo.FindOneBy(u => u.Email == "bscaturro@gmail.com");
            Assert.AreSame(Users[1], user);
        }

        [Test]
        public void GetByEmail_should_return_null_if_email_does_not_exist()
        {
            var user = Repo.FindOneBy(u => u.Email == "supergnarly@gmail.com");
            Assert.IsNull(user);
        }
    }
}
