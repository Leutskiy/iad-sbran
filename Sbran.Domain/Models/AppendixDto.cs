using Newtonsoft.Json;
using System;

namespace Sbran.Domain.Models
{
	public sealed class AppendixDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [JsonProperty("id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// Содержимое файла
        /// </summary>
        [JsonProperty("fileBinary")]
        public byte[] FileBinary { get; set; }

        /// <summary>
        /// Наименование файла
        /// </summary>
        [JsonProperty("fileName")]
        public string FileName { get; set; }

        /// <summary>
        /// Идентификатор отчета
        /// </summary>
        [JsonProperty("reportId")]
        public Guid? ReportId { get; set; }
    }
}
