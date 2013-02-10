using System.Collections.Generic;
using Core.Domain.Model.Users;
using Infrastructure.ListRepositories;
using NUnit.Framework;

namespace Test.Unit.Infrastructure.ListRepositories
{
    [TestFixture]
    public class ListUserRepositoryTest
    {
        protected List<User> Users { get; set; }

        protected IUserRepository Repo { get; set; }

        [SetUp]
        public void init()
        {
            Users = new List<User>()
                {
                    new User() {Id = 1, Email = "scaturrob@gmail.com", Name = "Brian Scaturro", Password = "password"},
                    new User() {Id = 2, Email = "bscaturro@gmail.com", Name = "Scaturro Brian", Password = "password"},
                    new User() {Id = 3, Email = "myotheremail@gmail.com", Name = "Bill User", Password = "password"}
                };
            Repo = new ListUserRepository(Users);
        }

        [Test]
        public void Get_should_return_element_containing_id()
        {
            var user = Repo.Get(1);
            Assert.AreSame(Users[0], user);
        }

        [Test]
        public void Get_should_return_null_if_id_not_present()
        {
            var user = Repo.Get(4);
            Assert.IsNull(user);
        }

        [Test]
        public void Store_should_add_new_User_to_internal_list()
        {
            var user = new User() {Id = 4, Email = "myemail@email.com", Name = "Sam User", Password = "mypassword"};
            Repo.Store(user);
            Assert.AreSame(user, Users[3]);
        }

        [Test]
        public void Store_should_not_add_existing_user()
        {
            var user = Repo.Get(1);
            user.Name = "An Updated Name";
            Repo.Store(user);
            Assert.AreEqual(3, Users.Count);
            Assert.AreEqual("An Updated Name", Users[0].Name);
        }
    }
}
