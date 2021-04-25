using System;

namespace Sbran.CQS.Read.Results
{
    /// <summary>
    /// Данные по иностранцу
    /// </summary>
    public sealed class AlienResult
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Данные по контакту
        /// </summary>
        public ContactResult? Contact { get; set; }

        /// <summary>
        /// Данные по паспорту
        /// </summary>
        public PassportResult? Passport { get; set; }

        /// <summary>
        /// Данные по организации
        /// </summary>
        public OrganizationResult? Organization { get; set; }

        /// <summary>
        /// Данные по государственной регистрации
        /// </summary>
        public StateRegistrationResult? StateRegistration { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string? Position { get; set; }

        /// <summary>
        /// Место работы
        /// </summary>
        public string? WorkPlace { get; set; }

        /// <summary>
        /// Адрес работы
        /// </summary>
        public string? WorkAddress { get; set; }

        /// <summary>
        /// Адрес пребывания
        /// </summary>
        public string? StayAddress { get; set; }
    }
}