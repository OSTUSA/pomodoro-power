﻿using DevOne.Security.Cryptography.BCrypt;

namespace Core.Domain.Model.Users
{
    /// <summary>
    /// Represents a Pomodoro user
    /// </summary>
    public class User : EntityBase<User>
    {
        public virtual string Email { get; set; }

        public virtual string Name { get; set; }

        public virtual string Password { get; set; }

        public virtual void HashPassword()
        {
            var salt = BCryptHelper.GenerateSalt(10);
            Password = BCryptHelper.HashPassword(Password, salt);
        }
    }
}
