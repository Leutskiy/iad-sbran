﻿using System;

namespace Sbran.Domain.Entities.Chat
{
	/// <summary>
	/// Сущность сообщение
	/// </summary>
	public sealed class ChatMessageFile
    {
        public ChatMessageFile()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        public Guid ChatMessageId { get; set; }
        public ChatMessage ChatMessage { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public byte[] FileBinary { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public string fileName { get; set; }
    }
}
