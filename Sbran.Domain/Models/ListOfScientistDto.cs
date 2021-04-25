using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.Domain.Models
{
    public sealed class ListOfScientistDto
    {
        /// <summary>
        /// ФИО
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ФИО
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Место работы
        /// </summary>
        public string? WorkPlace { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string? Position { get; set; }

        /// <summary>
        /// почта
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Ученая степень
        /// </summary>
        public string? AcademicDegree { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public bool Type { get; set; }

        public Guid ReportId { get; set; }
    }
}
