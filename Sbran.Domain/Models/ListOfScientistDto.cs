using Newtonsoft.Json;
using System;

namespace Sbran.Domain.Models
{
	public sealed class ListOfScientistDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [JsonProperty("id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        [JsonProperty("fullName")]
        public string? FullName { get; set; }

        /// <summary>
        /// Место работы
        /// </summary>
        [JsonProperty("workPlace")]
        public string? WorkPlace { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        [JsonProperty("position")]
        public string? Position { get; set; }

        /// <summary>
        /// Почта
        /// </summary>
        [JsonProperty("email")]
        public string? Email { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [JsonProperty("phoneNumber")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Ученая степень
        /// </summary>
        [JsonProperty("academicDegree")]
        public string? AcademicDegree { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        [JsonProperty("type")]
        public bool Type { get; set; }

        /// <summary>
        /// Идентификатор отчета
        /// </summary>
        [JsonProperty("reportId")]
        public Guid ReportId { get; set; }
    }
}
