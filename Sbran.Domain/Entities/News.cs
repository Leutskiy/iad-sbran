﻿using System;

namespace Sbran.Domain.Entities
{
	public sealed class News
    {
        public News()
        {
            Id = Guid.NewGuid();
        }

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
        public DateTimeOffset DateTime { get; set; }
    }
}
