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
    }
}
