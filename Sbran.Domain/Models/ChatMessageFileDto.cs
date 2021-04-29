using System;

namespace Sbran.Domain.Models
{
	/// <summary>
	/// Сущность сообщение
	/// </summary>
	public sealed class ChatMessageFileDto
    {
        /// <summary>
        /// Файл
        /// </summary>
        public string? fileBinary { get; set; }
        public string? account { get; set; }
        public Guid profileId { get; set; }
        public Guid chatRoomId { get; set; }
        public string fileName { get; set; }
    }
}
