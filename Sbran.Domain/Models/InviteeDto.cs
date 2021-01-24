using Newtonsoft.Json;

namespace Sbran.Domain.Models
{
	/// <summary>
	/// DTO приглашаемого
	/// </summary>
	public sealed class InviteeDto
    {
        /// <summary>
        /// DTO контактных данных
        /// </summary>
        [JsonProperty("alienContact")]
        public ContactDto? AlienContact { get; set; }

        /// <summary>
        /// DTO паспортных данных
        /// </summary>
        [JsonProperty("alienPassport")]
        public PassportDto? AlienPassport { get; set; }

        /// <summary>
        /// DTO организации
        /// </summary>
        [JsonProperty("alienOrganization")]
        public OrganizationDto? AlienOrganization { get; set; }

        /// <summary>
        /// DTO государственной регистрации
        /// </summary>
        [JsonProperty("alienStateRegistration")]
        public StateRegistrationDto? AlienStateRegistration { get; set; }

        /// <summary>
        /// TODO
        /// </summary>
        [JsonProperty("alienJob")]
        public JobDto? AlienJob { get; set; }

        /// <summary>
        /// DTO организации
        /// </summary>
        [JsonProperty("alienScientificInfo")]
        public ScientificInfoDto? AlienScientificInfoDto { get; set; }

        /// <summary>
        /// Адрес работы
        /// </summary>
        [JsonProperty("alienWorkAddress")]
        public string? AlienWorkAddress { get; set; }

        /// <summary>
        /// Адрес пребывания
        /// </summary>
        [JsonProperty("alienStayAddress")]
        public string? AlienStayAddress { get; set; }
    }
}