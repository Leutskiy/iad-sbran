using System;

namespace Sbran.Domain.Models
{
	public sealed class MessageDto
    {
        public string account { get; set; }
        public Guid chatRoomId { get; set; }
        public string? message { get; set; }
    }
}
