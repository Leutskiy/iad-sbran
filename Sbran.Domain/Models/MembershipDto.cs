using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.Domain.Models
{
    public sealed class MembershipDto
    { /// <summary>
      /// наименование организации
      /// </summary>
        public string? NameOfCompany { get; set; }

        /// <summary>
        /// статус в организации
        /// </summary>
        public string? StatusInTheOrganization { get; set; }

        /// <summary>
        /// дата вступления
        /// </summary>
        public DateTime? DateOfEntry { get; set; }

        /// <summary>
        /// сайт организации
        /// </summary>
        public string? SiteOfTheOrganization { get; set; }

        public Guid EmployeeId { get; set; }
    }
}
