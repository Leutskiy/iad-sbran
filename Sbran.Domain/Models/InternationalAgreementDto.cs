using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.Domain.Models
{
    public sealed class InternationalAgreementDto
    {
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
    }
}
