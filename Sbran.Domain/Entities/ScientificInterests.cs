using System;

namespace Sbran.Domain.Entities
{
    /// <summary>
    /// Научные интересы
    /// </summary>
    public sealed class ScientificInterests
    {
        public ScientificInterests()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// наименование научных интересов 
        /// </summary>
        public string? NameOfScientificInterests { get; set; }

        public Guid EmployeeId { get; set; }

        public Employee Employee { get; set; }

    }
}
