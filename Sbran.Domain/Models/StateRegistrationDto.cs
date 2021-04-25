using Newtonsoft.Json;

namespace Sbran.Domain.Models
{
    /// <summary>
    /// DTO государственной регистрации
    /// </summary>
    public sealed class StateRegistrationDto
    {
        /// <summary>
        /// ИНН
        /// </summary>
        [JsonProperty("inn")]
        public string? Inn { get; set; }

        /// <summary>
        /// ОГРНИП
        /// </summary>
        [JsonProperty("ogrnip")]
        public string? Ogrnip { get; set; }
    }
}