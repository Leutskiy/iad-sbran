using System;

namespace Sbran.Domain.Models
{
	public sealed class NewsDto
    {
        /// <summary>
        /// Идентификтор
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата и время
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}
