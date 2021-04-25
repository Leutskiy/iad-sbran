using System;

namespace Sbran.Domain.Entities
{
    /// <summary>
    /// Примечание
    /// </summary>
    public sealed class Appendix
    {
        public Appendix()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public byte[]? FileBinary { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public string? FileName { get; set; }

        public Guid ReportId { get; set; }
        public Report Report { get; set; }

    }
}
