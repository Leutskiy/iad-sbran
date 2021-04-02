using Sbran.Domain.Enums;
using System;

namespace Sbran.Domain.Entities
{
    /// <summary>
    /// Членство
    /// </summary>
    public sealed class Membership
    {
        public Membership()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
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
        
        /// <summary>
        /// сайт организации
        /// </summary>
        public MembershipType MembershipType { get; set; }

        public Guid EmployeeId { get; set; }

        public Employee Employee { get; set; }

    }
}
