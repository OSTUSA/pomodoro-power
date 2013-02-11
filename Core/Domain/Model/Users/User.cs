using DevOne.Security.Cryptography.BCrypt;

namespace Core.Domain.Model.Users
{
    /// <summary>
    /// Represents a Pomodoro user
    /// </summary>
    public class User
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public void HashPassword()
        {
            var salt = BCryptHelper.GenerateSalt(10);
            Password = BCryptHelper.HashPassword(Password, salt);
        }
    }
}
