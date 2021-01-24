using System;

namespace Sbran.Domain.Models
{
    /// <summary>
    /// DTO иностранного участника
    /// </summary>
    public sealed class ForeignParticipantDto
    {
        /// <summary>
        /// Идентификатор иностранца
        /// </summary>
        public Guid AlienId { get; set; }

        /// <summary>
        /// Идентификатор приглашения
        /// </summary>
        public Guid InvitationId { get; set; }

        /// <summary>
        /// DTO паспортных данных
        /// </summary>
        public PassportDto? Passport { get; set; }
    }
}