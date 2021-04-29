using System;

namespace Sbran.Domain.Models
{
	public sealed class PublicationDto
    {/// <summary>
     /// научный руководитель, соавторы
     /// </summary>
        public string? ScientificAdvisor { get; set; }

        /// <summary>
        /// название статьи
        /// </summary>
        public string? TitleOfTheArticle { get; set; }

        /// <summary>
        /// аннотация 
        /// </summary>
        public string? Abstract { get; set; }

        /// <summary>
        /// ключевые слова
        /// </summary>
        public string? Keywords { get; set; }

        /// <summary>
        /// основной текст статьи
        /// </summary>
        public string? MainTextOfTheArticle { get; set; }

        /// <summary>
        /// Источники
        /// </summary>
        public string? Literature { get; set; }

        public Guid EmployeeId { get; set; }
    }
}
