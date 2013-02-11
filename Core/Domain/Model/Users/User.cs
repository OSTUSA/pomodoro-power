using DevOne.Security.Cryptography.BCrypt;

namespace Core.Domain.Model.Users
{
    /// <summary>
    /// Represents a Pomodoro user
    /// </summary>
    public class User
    {
        /// <summary>
        /// The unique ID of a User
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The user's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The user's password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Hash the user's password using a Blowfish cypher
        /// </summary>
        public void HashPassword()
        {
            var salt = BCryptHelper.GenerateSalt(10);
            Password = BCryptHelper.HashPassword(Password, salt);
        }
    }
}
