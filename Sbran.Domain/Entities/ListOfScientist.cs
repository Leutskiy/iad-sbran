using System;

namespace Sbran.Domain.Entities
{
	/// <summary>
	/// Список ученых
	/// </summary>
	public sealed class ListOfScientist
    {
        public ListOfScientist()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Место работы
        /// </summary>
        public string WorkPlace { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }
        
        /// <summary>
        /// Ученая степень
        /// </summary>
        public string AcademicDegree { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public bool Type { get; set; }

        /// <summary>
        /// почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PhoneNumber { get; set; }

        public Guid ReportId { get; set; }
        public Report Report { get; set; }
    }
}
