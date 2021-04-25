using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.Domain.Models
{
    public sealed class AppendixDto
    {
        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public byte[] FileBinary { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public string FileName { get; set; }

        public Guid ReportId { get; set; }
    }
}
