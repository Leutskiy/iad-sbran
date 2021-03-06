﻿using Sbran.Domain.Enums;
using System;

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

        /// <summary>
        /// сайт организации
        /// </summary>
        public string? SiteOfTheJournal { get; set; }

        /// <summary>
        /// сайт организации
        /// </summary>
        public MembershipType MembershipType { get; set; }

        public Guid EmployeeId { get; set; }
    }
}
