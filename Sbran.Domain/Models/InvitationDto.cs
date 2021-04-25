using System.Collections.Generic;

namespace Sbran.Domain.Models
{
    /// <summary>
    /// DTO приглашения
    /// </summary>
    public sealed class InvitationDto
    {
        /// <summary>
        /// DTO иностранца
        /// </summary>
        public InviteeDto Alien { get; init; } = default!;

        /// <summary>
        /// DTO деталей визита
        /// </summary>
        public VisitDetailDto? VisitDetail { get; init; }

        /// <summary>
        /// Коллекция DTOs иностранного сопровождения сопровождения
        /// </summary>
        public IEnumerable<ForeignParticipantDto>? ForeignParticipants { get; init; }
    }
}