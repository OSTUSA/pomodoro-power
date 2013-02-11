using System.Collections.Generic;
using Core.Domain.Model.Users;
using System.Linq;

namespace Infrastructure.ListRepositories
{
    /// <summary>
    /// A UserRepository backed by a List of type User
    /// </summary>
    public class ListUserRepository : IUserRepository
    {
        /// <summary>
        /// An internal list backing the this repository
        /// </summary>
        protected static List<User> Users { get; set; }

        /// <summary>
        /// The internal Id tracker
        /// </summary>
        private static long _id;

        private readonly object _listLock = new object();

        /// <summary>
        /// Set the internal list to a new list
        /// and start the Id tracker at 0
        /// </summary>
        static ListUserRepository()
        {
            Users = new List<User>();
            _id = 0;
        }

        /// <summary>
        /// Get a User by id
        /// </summary>
        /// <param name="id">The unique id of a User object</param>
        /// <returns>A User object</returns>
        public User Get(long id)
        {
            return Users.SingleOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Fetch a single user by the unique email address
        /// </summary>
        /// <param name="email">A valid email address</param>
        /// <returns>A User object</returns>
        public User GetByEmail(string email)
        {
            return Users.SingleOrDefault(x => x.Email == email);
        }

        /// <summary>
        /// Persist a User. If an ID is present it will update,
        /// if not it will insert
        /// </summary>
        /// <param name="user">The User object to persist</param>
        public void Store(User user)
        {
            var fetched = Get(user.Id);
            if (fetched == null)
                Insert(user);
        }

        /// <summary>
        /// Reset the internal list
        /// </summary>
        public void Clear()
        {
            lock (_listLock)
            {
                Users = new List<User>();
                _id = 0;
            }
        }

        /// <summary>
        /// Insert a new user
        /// </summary>
        /// <param name="user">The user to insert</param>
        protected void Insert(User user)
        {
            lock (_listLock)
            {
                user.Id = ++_id;
                Users.Add(user);
            }
        }
    }
}
