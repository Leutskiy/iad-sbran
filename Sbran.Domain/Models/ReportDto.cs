using Newtonsoft.Json;
using Sbran.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Sbran.Domain.Models
{
	public sealed class ReportDto
    {
        /// <summary>
        /// Основная часть
        /// </summary>
        [JsonProperty("id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// Основная часть
        /// </summary>
        [JsonProperty("mainPart")]
        public string? MainPart { get; set; }

        /// <summary>
        /// Выводы
        /// </summary>
        [JsonProperty("findings")]
        public string? Findings { get; set; }

        /// <summary>
        /// Предложения
        /// </summary>
        [JsonProperty("suggestion")]
        public string? Suggestion { get; set; }

        /// <summary>
        /// Заинтересованность иностранной стороны
        /// </summary>
        [JsonProperty("foreignInterest")]
        public string? ForeignInterest { get; set; }

        /// <summary>
        /// Статус отчета
        /// </summary>
        [JsonProperty("status")]
        public bool? Status { get; set; }

        /// <summary>
        /// ТипОтчета
        /// </summary>
        [JsonProperty("reportType")]
        public ReportType ReportType { get; set; }

        /// <summary>
        /// ТипОтчета
        /// </summary>
        [JsonProperty("parentId")]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Вложение
        /// </summary>
        [JsonProperty("appendix")]
        public List<AppendixDto>? Appendix { get; set; }

        /// <summary>
        /// Список ученых
        /// </summary>
        [JsonProperty("listOfScientists")]
        public List<ListOfScientistDto>? ListOfScientists { get; set; }
    }
}
