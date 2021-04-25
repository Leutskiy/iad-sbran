using Sbran.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Sbran.CQS.Read.Results
{
    /// <summary>
    /// Данные по приглашению
    /// </summary>
    public sealed class InvitationResult
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Данные по иностранцу
        /// </summary>
        public AlienResult? Alien { get; set; }
        
        /// <summary>
        /// Данные по иностранцу
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public InvitationStatus? InvitationStatus { get; set; }

        /// <summary>
        /// Индикатор отчета
        /// </summary>
        public Guid? ReportId { get; set; }

        /// <summary>
        /// Данные по сотруднику
        /// </summary>
        public EmployeeResult? Employee { get; set; }

        /// <summary>
        /// Данные по деталям визита
        /// </summary>
        public VisitDetailResult? VisitDetail { get; set; }

        /// <summary>
        /// Данные по иностранному сопровождению
        /// </summary>
        public IEnumerable<ForeignParticipantResult>? ForeignParticipants { get; set; }
    }
}