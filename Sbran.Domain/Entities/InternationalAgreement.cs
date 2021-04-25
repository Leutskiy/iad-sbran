using System;

namespace Sbran.Domain.Entities
{
    /// <summary>
    /// Международные соглашения
    /// </summary>
    public sealed class InternationalAgreement
    {
        public InternationalAgreement()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// наименование соглашения
        /// </summary>
        public string? TheNameOfTheAgreement { get; set; }

        /// <summary>
        /// первая сторона соглашения
        /// </summary>
        public string? TheFirstPartyToTheAgreement { get; set; }

        /// <summary>
        /// вторая сторона соглашения
        /// </summary>
        public string? TheSecondPartyToTheAgreement { get; set; }

        /// <summary>
        /// место подписания
        /// </summary>
        public string? PlaceOfSigning { get; set; }

        /// <summary>
        /// дата вступления
        /// </summary>
        public DateTime? DateOfEntry { get; set; }

        /// <summary>
        /// текст соглашения
        /// </summary>
        public string? TextOfTheAgreement { get; set; }

        public Guid EmployeeId { get; set; }

        public Employee Employee { get; set; }

    }
}
