using Sbran.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Sbran.CQS.Read.Results
{
    /// <summary>
    /// Данные по сотруднику
    /// </summary>
    public sealed class EmployeeResult
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор руководителя
        /// </summary>
        public Guid? ManagerId { get; set; }

        /// <summary>
        /// Данные по контакту
        /// </summary>
        public ContactResult? Contact { get; set; }

        /// <summary>
        /// Данные по паспорту
        /// </summary>
        public PassportResult? Passport { get; set; }

        /// <summary>
        /// Данные по организации
        /// </summary>
        public OrganizationResult? Organization { get; set; }

        /// <summary>
        /// Данные по государственной регистрации
        /// </summary>
        public StateRegistrationResult? StateRegistration { get; set; }

        /// <summary>
        /// Научная степень
        /// </summary>
        public string? AcademicDegree { get; set; }

        /// <summary>
        /// Научное звание
        /// </summary>
        public string? AcademicRank { get; set; }

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

        /// <summary>
        /// приглашения
        /// </summary>
        public List<Invitation> Invitations { get; set; }
        /// <summary>
        /// Выезды
        /// </summary>
        public List<Departure> Departures { get; set; }
        /// <summary>
        /// Выезды
        /// </summary>
        public List<Membership> Memberships { get; set; }
        /// <summary>
        /// Выезды
        /// </summary>
        public List<Publication> Publications { get; set; }
        /// <summary>
        /// Выезды
        /// </summary>
        public List<ScientificInterests> ScientificInterests { get; set; }
        /// <summary>
        /// Выезды
        /// </summary>
        public List<ConsularOffice> ConsularOffices { get; set; }

    }
}