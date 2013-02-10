namespace Core.Domain.Model.Users
{
    /// <summary>
    /// Persistence access to User entities
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get a User by id
        /// </summary>
        /// <param name="id">The unique id of a User object</param>
        /// <returns>A User object</returns>
        User Get(long id);

        /// <summary>
        /// Persist a User. If an ID is present it will update,
        /// if not it will insert
        /// </summary>
        /// <param name="user">The User object to persist</param>
        void Store(User user);
    }
}
