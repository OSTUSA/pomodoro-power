using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain.Model;
using User = Infrastructure.ListRepositories.Proxy.Users.User;

namespace Infrastructure.ListRepositories
{
    /// <summary>
    /// A UserRepository backed by a List of type User
    /// </summary>
    public class ListUserRepository : IRepository<User>
    {
        protected static List<User> Users { get; set; }

        private static long _id;

        private readonly object _listLock = new object();

        static ListUserRepository()
        {
            Users = new List<User>();
            _id = 0;
        }

        public User Get(object id)
        {
            return Users.SingleOrDefault(x => x.Id == (long) id);
        }

        public List<User> GetAll()
        {
            return Users;
        }

        public List<User> FindBy(Func<User, bool> predicate)
        {
            return Users.Where(predicate).ToList();
        }

        public User FindOneBy(Func<User, bool> pred)
        {
            return Users.SingleOrDefault(pred);
        }

        public void Store(User user)
        {
            var fetched = Get(user.Id);
            if (fetched == null)
                Insert(user);
        }

        public void Delete(User entity)
        {
            Users.RemoveAll(e => e.Id == entity.Id);
        }

        public void Clear()
        {
            lock (_listLock)
            {
                Users = new List<User>();
                _id = 0;
            }
        }

        protected void Insert(Core.Domain.Model.Users.User user)
        {
            lock (_listLock)
            {
                var cast = user as User;
                cast.SetId(++_id);
                Users.Add(cast);
            }
        }
    }
}
