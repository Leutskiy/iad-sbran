using System;

namespace Sbran.Domain.Entities
{
    /// <summary>
    /// Консульские учреждения
    /// </summary>
    public sealed class ConsularOffice
    {
        public ConsularOffice()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// наименование консульского учреждения
        /// </summary>
        public string? NameOfTheConsularPost { get; set; }

        /// <summary>
        ///  страна местонахождения
        /// </summary>
        public string? CountryOfLocation { get; set; }

        /// <summary>
        /// город местонахождения
        /// </summary>
        public string? CityOfLocation { get; set; }

        /// <summary>
        /// Текст соглашения
        /// </summary>
        public string? TextOfAgreement { get; set; }

        public Guid EmployeeId { get; set; }

        public Employee Employee { get; set; }

    }
}
