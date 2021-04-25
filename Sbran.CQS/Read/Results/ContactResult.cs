using System;

namespace Sbran.CQS.Read.Results
{
    /// <summary>
    /// Данные по контакту
    /// </summary>
    public sealed class ContactResult
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Индекс
        /// </summary>
        public string? Postcode { get; set; }

        /// <summary>
        /// Домашний номер телефона
        /// </summary>
        public string? HomePhoneNumber { get; set; }

        /// <summary>
        /// Рабочий номер телефона
        /// </summary>
        public string? WorkPhoneNumber { get; set; }

        /// <summary>
        /// Мобильный номер телефона
        /// </summary>
        public string? MobilePhoneNumber { get; set; }
    }
}