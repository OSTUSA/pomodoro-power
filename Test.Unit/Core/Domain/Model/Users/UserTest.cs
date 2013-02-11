using Core.Domain.Model.Users;
using DevOne.Security.Cryptography.BCrypt;
using NUnit.Framework;

namespace Test.Unit.Core.Domain.Model.Users
{
    [TestFixture]
    public class UserTest
    {
        [Test]
        public void HashPassword_should_hash_unhashed_password()
        {
            var user = new User() {Password = "password"};
            user.HashPassword();
            Assert.IsTrue(BCryptHelper.CheckPassword("password", user.Password));
        }
    }
}
