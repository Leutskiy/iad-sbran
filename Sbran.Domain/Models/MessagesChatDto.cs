namespace Sbran.Domain.Models
{
	public sealed class MessagesChatDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public bool? IsValid { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string? DateTime { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string? profileId { get; set; }
        
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string? profileTo { get; set; }
    }
}
