using Sbran.Domain.Entities.System;
using System;

namespace Sbran.Domain.Entities.Chat
{
	/// <summary>
	/// Сущность сообщение
	/// </summary>
	public sealed class ChatMessage
    {
        public ChatMessage()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Идентификатор комнаты
        /// </summary>
        public Guid ChatRoomId { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public DateTimeOffset DateTime { get; set; }

        public ChatRoom? ChatRoom { get; set; }

       //public List<ChatMessageFile> ChatMessageFiles { get; set; }

        /// <summary>
        /// Задать пользователя
        /// </summary>
        /// <param name="userId">Пользователь</param>
        public void SetUser(User user)
        {
            if (UserId == user.Id)
            {
                return;
            }

            UserId = user.Id;
        }

        /// <summary>
        /// Задать пользователя
        /// </summary>
        /// <param name="userId">Пользователь</param>
        public void SetChatRoom(ChatRoom chatRoom)
        {
            if (ChatRoomId == chatRoom.Id)
            {
                return;
            }

            ChatRoomId = chatRoom.Id;
            ChatRoom = chatRoom;
        }
    }
}
