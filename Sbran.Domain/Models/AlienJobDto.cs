namespace Sbran.Domain.Models
{
	/// <summary>
	/// DTO работы иностранца
	/// </summary>
	public sealed class AlienJobDto
    {
        /// <summary>
        /// Место работы
        /// </summary>
        public string? WorkPlace { get; set; }

        /// <summary>
        /// Рабочий адрес
        /// </summary>
        public string? WorkAddress { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string? Position { get; set; }
    }
}