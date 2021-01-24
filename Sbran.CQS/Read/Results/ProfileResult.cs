using System;

namespace Sbran.CQS.Read.Results
{
    /// <summary>
    /// Данные по профилю
    /// </summary>
    public sealed class ProfileResult
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Аватарка
        /// </summary>
        public byte[]? Avatar { get; set; }

        /// <summary>
        /// Web-страницы
        /// </summary>
        public string? WebPages { get; set; }
    }
}