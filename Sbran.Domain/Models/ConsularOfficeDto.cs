using System;

namespace Sbran.Domain.Models
{
	public sealed class ConsularOfficeDto
    {
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
        /// город местонахождения
        /// </summary>
        public string? TextOfAgreement { get; set; }

        public Guid EmployeeId { get; set; }
    }
}
