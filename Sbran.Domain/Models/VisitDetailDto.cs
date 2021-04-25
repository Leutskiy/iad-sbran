using Sbran.Domain.Entities;
using Newtonsoft.Json;
using System;

namespace Sbran.Domain.Models
{
    /// <summary>
    /// DTO деталей визита
    /// </summary>
    public sealed class VisitDetailDto
    {
        /// <summary>
        /// Цель визита
        /// </summary>
        [JsonProperty("goal")]
        public string? Goal { get; set; }

        /// <summary>
        /// Страна визита
        /// </summary>
        [JsonProperty("country")]
        public string? Country { get; set; }

        /// <summary>
        /// Пункты посещения
        /// </summary>
        [JsonProperty("visitingPoints")]
        public string? VisitingPoints { get; set; }

        /// <summary>
        /// Период пребывания
        /// </summary>
        [JsonProperty("periodInDays")]
        public long PeriodInDays { get; set; }

        /// <summary>
        /// Дата пребытия
        /// </summary>
        [JsonProperty("arrivalDate")]
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// Дата депортации
        /// </summary>
        [JsonProperty("departureDate")]
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// Вид визы
        /// </summary>
        [JsonProperty("visaType")]
        public string? VisaType { get; set; }

        /// <summary>
        /// Город получения визы
        /// </summary>
        [JsonProperty("visaCity")]
        public string? VisaCity { get; set; }

        /// <summary>
        /// Страна получения визы
        /// </summary>
        [JsonProperty("visaCountry")]
        public string? VisaCountry { get; set; }

        /// <summary>
        /// Кратность визы
        /// </summary>
        [JsonProperty("visaMultiplicity")]
        public VisaMultiplicity VisaMultiplicity { get; set; }
    }
}