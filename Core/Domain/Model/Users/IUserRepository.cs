namespace Core.Domain.Model.Users
{
    /// <summary>
    /// Persistence access to User entities
    /// </summary>
    public interface IUserRepository
    {
        User Get(long id);

        User GetByEmail(string email);

        void Store(User user);
    }
}
