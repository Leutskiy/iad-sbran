using System;
using Sbran.Domain.Enums;

namespace Sbran.CQS.Read.Results
{
	/// <summary>
	/// Данные по документу
	/// </summary>
	public sealed class DocumentResult
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Содержимое
        /// </summary>
        public byte[] Content { get; set; }

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