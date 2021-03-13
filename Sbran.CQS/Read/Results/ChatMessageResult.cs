using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.CQS.Read.Results
{
    /// <summary>
    /// Данные об сообщении
    /// </summary>
    public sealed class ChatMessageResult
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public DateTimeOffset DateTime { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid profileId { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid profileTo { get; set; }
    }
}
