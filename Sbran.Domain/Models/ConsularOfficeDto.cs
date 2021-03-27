using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Guid EmployeeId { get; set; }
    }
}
