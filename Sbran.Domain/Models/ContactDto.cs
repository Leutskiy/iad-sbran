using Newtonsoft.Json;

namespace Sbran.Domain.Models
{
    /// <summary>
    /// DTO контакта
    /// </summary>
    public sealed class ContactDto
    {
        /// <summary>
        /// Электронная почта
        /// </summary>
        [JsonProperty("email")]
        public string? Email { get; set; }

        /// <summary>
        /// Индекс
        /// </summary>
        [JsonProperty("postcode")]
        public string? Postcode { get; set; }

        /// <summary>
        /// Домашний номер телефона
        /// </summary>
        [JsonProperty("homePhoneNumber")]
        public string? HomePhoneNumber { get; set; }

        /// <summary>
        /// Рабочий номер телефона
        /// </summary>
        [JsonProperty("workPhoneNumber")]
        public string? WorkPhoneNumber { get; set; }

        /// <summary>
        /// Мобильный номер телефона
        /// </summary>
        [JsonProperty("mobilePhoneNumber")]
        public string? MobilePhoneNumber { get; set; }
    }
}