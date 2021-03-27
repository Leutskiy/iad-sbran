using System;

namespace Sbran.Domain.Entities
{
    /// <summary>
    /// Публикации
    /// </summary>
    public sealed class Publication
    {
        public Publication()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
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

        public Employee Employee { get; set; }

    }
}
