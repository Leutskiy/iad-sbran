using System;

namespace Sbran.Domain.Entities.Chat
{
	/// <summary>
	/// Комнаты для разговоров
	/// </summary>
	public sealed class ChatRoom
    {
        public ChatRoom()
        {
            Id = Guid.NewGuid();
        }

        public ChatRoom(Guid guid)
        {
            Id = guid;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; private set; }
    }
}
