using System;

namespace Sbran.CQS.Read.Results
{
    /// <summary>
    /// Данные по организации
    /// </summary>
    public sealed class OrganizationResult
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Данные по государственной регистрации
        /// </summary>
        public StateRegistrationResult? StateRegistration { get; set; }

        /// <summary>
        /// Полное наименование
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Краткое наименование
        /// </summary>
        public string? ShortName { get; set; }

        /// <summary>
        /// Юридический адрес
        /// </summary>
        public string? LegalAddress { get; set; }

        /// <summary>
        /// Направление научной деятельности
        /// </summary>
        public string? ScientificActivity { get; set; }
    }
}