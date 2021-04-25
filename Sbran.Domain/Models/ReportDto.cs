using Sbran.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.Domain.Models
{
    public sealed class ReportDto
    {
        /// <summary>
        /// Основная часть
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Основная часть
        /// </summary>
        public Guid? AppendixId { get; set; }

        /// <summary>
        /// Основная часть
        /// </summary>
        public string? MainPart { get; set; }


        /// <summary>
        /// Выводы
        /// </summary>
        public string? Findings { get; set; }

        /// <summary>
        /// Предложения
        /// </summary>
        public string? Suggestion { get; set; }

        /// <summary>
        /// Заинтересованность иностранной стороны
        /// </summary>
        public string? ForeignInterest { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public byte[]? FileBinary { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// ТипОтчета
        /// </summary>
        public ReportType ReportType { get; set; }

        /// <summary>
        /// ТипОтчета
        /// </summary>
        public Guid? ParentId { get; set; }
    }
}
