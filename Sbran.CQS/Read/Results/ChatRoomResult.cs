﻿using System;

namespace Sbran.CQS.Read.Results
{
	/// <summary>
	/// Данные об комнате
	/// </summary>
	public sealed class ChatRoomResult
    {
        /// <summary>
        /// идентификатор пользователя
        /// </summary>
        public Guid userid { get; set; }

        /// <summary>
        /// email пользователя
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// путь к картинке
        /// </summary>
        public byte[]? image { get; set; }

        /// <summary>
        /// идентификатор комнаты
        /// </summary>
        public Guid? chatRoomId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset? lastmessagedate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? lastmessage { get; set; }
    }
}
