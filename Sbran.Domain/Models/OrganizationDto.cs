using Newtonsoft.Json;

namespace Sbran.Domain.Models
{
    /// <summary>
    /// DTO организации
    /// </summary>
    public sealed class OrganizationDto
    {
        /// <summary>
        /// DTO государственной регистрации
        /// </summary>
        [JsonProperty("stateRegistration")]
        public StateRegistrationDto? StateRegistration { get; set; }

        /// <summary>
        /// Полное наименование
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Краткое наименование
        /// </summary>
        [JsonProperty("shortName")]
        public string? ShortName { get; set; }

        /// <summary>
        /// Юридический адрес
        /// </summary>
        [JsonProperty("legalAddress")]
        public string? LegalAddress { get; set; }

        /// <summary>
        /// Направление научной деятельности
        /// </summary>
        [JsonProperty("scientificActivity")]
        public string? ScientificActivity { get; set; }
    }
}