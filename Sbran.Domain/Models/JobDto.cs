using Newtonsoft.Json;

namespace Sbran.Domain.Models
{
	/// <summary>
	/// DTO работы сотрудника
	/// </summary>
	public sealed class JobDto
    {
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
    }
}