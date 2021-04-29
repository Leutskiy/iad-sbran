using System;

namespace Sbran.CQS.Read.Results
{
	public sealed class ChatMessageFileResult
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        public Guid ChatMessageId { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public byte[] FileBinary { get; set; }
    }
}
