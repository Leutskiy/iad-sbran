namespace Sbran.Domain.Models
{
    /// <summary>
    /// DTO профиля
    /// </summary>
    public sealed class ProfileDto
    {
        /// [JsonConverter(typeof(Base64FileJsonConverter))]
        /// <summary>
        /// Аватар
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// Веб-страницы
        /// </summary>
        public string? WebPages { get; set; }
    }
}
