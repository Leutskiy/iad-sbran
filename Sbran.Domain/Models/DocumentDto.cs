using Sbran.Domain.Enums;
using System;

namespace Sbran.Domain.Models
{
    /// <summary>
    /// DTO документа
    /// </summary>
    public sealed class DocumentDto
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Содержимое
        /// </summary>
        public byte[]? Content { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Тип документа
        /// </summary>
        public DocumentType DocumentType { get; set; }
    }
}