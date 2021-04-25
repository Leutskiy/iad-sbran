using System;

namespace Sbran.Domain.Entities
{
    /// <summary>
    /// Контакт
    /// </summary>
    public sealed class Contact
    {
		public Contact()
		{
            Id = Guid.NewGuid();
		}

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string? Email { get; private set; }

        /// <summary>
        /// Индекс
        /// </summary>
        public string? Postcode { get; private set; }

        /// <summary>
        /// Домашний номер телефона
        /// </summary>
        public string? HomePhoneNumber { get; private set; }

        /// <summary>
        /// Рабочий номер телефона
        /// </summary>
        public string? WorkPhoneNumber { get; private set; }

        /// <summary>
        /// Мобильный номер телефона
        /// </summary>
        public string? MobilePhoneNumber { get; private set; }

        /// <summary>
        /// Задать электронную почту
        /// </summary>
        /// <param name="email">Электронная почта</param>
        public void SetEmail(string email)
        {
            if (Email == email)
            {
                return;
            }

            Email = email;
        }

        /// <summary>
        /// Задать почтовый индекс
        /// </summary>
        /// <param name="postcode">Почтовый индекс</param>
        public void SetPostcode(string postcode)
        {
            if (Postcode == postcode)
            {
                return;
            }

            Postcode = postcode;
        }

        /// <summary>
        /// Задать рабочий номер телефона
        /// </summary>
        /// <param name="workPhoneNumber">Рабочий номер телефона</param>
        public void SetWorkPhoneNumber(string workPhoneNumber)
        {
            if (WorkPhoneNumber == workPhoneNumber)
            {
                return;
            }

            WorkPhoneNumber = workPhoneNumber;
        }

        /// <summary>
        /// Задать домашний телефонный номер
        /// </summary>
        /// <param name="homePhoneNumber">Домашний телефонный номер</param>
        public void SetHomePhoneNumber(string homePhoneNumber)
        {
            if (HomePhoneNumber == homePhoneNumber)
            {
                return;
            }

            HomePhoneNumber = homePhoneNumber;
        }

        /// <summary>
        /// Задать мобильный номер телефона
        /// </summary>
        /// <param name="mobilePhoneNumber">Мобильный номер телефона</param>
        public void SetMobilePhoneNumber(string mobilePhoneNumber)
        {
            if (MobilePhoneNumber == mobilePhoneNumber)
            {
                return;
            }

            MobilePhoneNumber = mobilePhoneNumber;
        }
    }
}