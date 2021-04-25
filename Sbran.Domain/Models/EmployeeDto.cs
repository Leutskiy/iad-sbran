using System;

namespace Sbran.Domain.Models
{
    /// <summary>
    /// DTO сотрудника
    /// </summary>
    public sealed class EmployeeDto
    {
        /// <summary>
        /// Идентификатор руководителя
        /// </summary>
        public Guid? ManagerId { get; set; }

        /// <summary>
        /// DTO контактных данных
        /// </summary>
        public ContactDto? Contact { get; set; }

        /// <summary>
        /// DTO паспортных данных
        /// </summary>
        public PassportDto? Passport { get; set; }

        /// <summary>
        /// DTO организации
        /// </summary>
        public OrganizationDto? Organization { get; set; }

        /// <summary>
        /// DTO государственной регистрации
        /// </summary>
        public StateRegistrationDto? StateRegistration { get; set; }

        /// <summary>
        /// Научное звание
        /// </summary>
        public string? AcademicRank { get; set; }

        /// <summary>
        /// Научная степень
        /// </summary>
        public string? AcademicDegree { get; set; }

        /// <summary>
        /// Образование
        /// </summary>
        public string? Education { get; set; }

        /// <summary>
        /// Место работы
        /// </summary>
        public string? WorkPlace { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string? Position { get; set; }
    }
}