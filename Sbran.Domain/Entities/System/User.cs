using System;

namespace Sbran.Domain.Entities.System
{
    /// <summary>
    /// Пользователь системы
    /// </summary>
    public sealed class User
    {
		private User()
		{
            Id = Guid.NewGuid();
        }

        public User(Profile profile) : this()
        {
            ProfileId = profile.Id;
            Profile = profile;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Идентификатор профиля
        /// </summary>
        public Guid ProfileId { get; private set; }

        /// <summary>
        /// Информация о профиле
        /// Профиль может быть не задан, если служебная учетка (например, админ)
        /// </summary>
        public Profile Profile { get; private set; }

        /// <summary>
        /// Имя аккаунта
        /// </summary>
        public string? Account { get; private set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string? Password { get; private set; }

        public void SetAccount(string account)
        {
            if (Account == account)
            {
                return;
            }

            Account = account;
        }

        public void SetPassword(string password)
        {
            if (Password == password)
            {
                return;
            }

            Password = password;
        }
    }
}