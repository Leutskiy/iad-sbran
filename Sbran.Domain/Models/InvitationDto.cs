using Newtonsoft.Json;
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
        [JsonProperty("alien")]
        public InviteeDto Alien { get; init; } = default!;

        /// <summary>
        /// DTO деталей визита
        /// </summary>
        [JsonProperty("visitDetail")]
        public VisitDetailDto? VisitDetail { get; init; }

        /// <summary>
        /// Коллекция DTOs иностранного сопровождения сопровождения
        /// </summary>
        [JsonProperty("foreignParticipants")]
        public IEnumerable<ForeignParticipantDto>? ForeignParticipants { get; init; }
    }
}