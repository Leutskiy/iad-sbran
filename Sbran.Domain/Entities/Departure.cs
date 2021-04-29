using Sbran.Domain.Enums;
using System;

/***
- страна командирования
- город командирования
- источник финансирования
- основание выезда(плановый, внеплановый, работа по гранту, и др.)
- принимающая организация
- место проживания(выбор осуществлялся самостоятельно или принимающей стороной)
- цель командирования(название мероприятия, тема научной работы)
- обоснование командирования(краткое обоснование целей и задач выезда)
- срок командировки (с __ по __)
***/

namespace Sbran.Domain.Entities
{
    // TODO: Не забыть про init only
    /// <summary>
    /// Выезд
    /// </summary>
    public sealed class Departure
    {
        /// <summary>
        /// Используем только инициализацию свойств
        /// </summary>
		public Departure()
		{
			Id = Guid.NewGuid();
		}

		/// <summary>
		/// Идентификатор
		/// </summary>
		public Guid Id { get; set; }

        /// <summary>
        /// Страна командирования
        /// </summary>
        public string? SendingCountry { get; set; }

        /// <summary>
        /// Город командирования
        /// </summary>
        public string? CityOfBusiness { get; set; }

        /// <summary>
        /// Источник финансирования
        /// </summary>
        public string? SourceOfFinancing { get; set; }

        /// <summary>
        /// Основание выезда
        /// </summary>
        public string? BasicOfDeparture { get; set; }

        /// <summary>
        /// Принимающая организация
        /// </summary>
        public string? HostOrganization { get; set; }

        /// <summary>
        /// Место проживания
        /// </summary>
        public string? PlaceOfResidence { get; set; }

        /// <summary>
        /// Цель командирования
        /// </summary>
        public string? PurposeOfTheTrip { get; set; }

        /// <summary>
        /// Обоснование командировки
        /// </summary>
        public string? JustificationOfTheBusiness { get; set; }

        // TODO: почему с ?
        /// <summary>
        /// Статус
        /// </summary>
        public DepartureStatus? DepartureStatus { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime? DateStart { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime? DateEnd { get; set; }

        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public Guid EmployeeId { get; set; } = default!;

        /// <summary>
        /// Сотрудник, совершивший выезд
        /// </summary>
        public Employee Employee { get; set; } = default!;

        /// <summary>
        /// Идентификатор отчета о выезде
        /// </summary>
        public Guid? ReportId { get; set; }

        /// <summary>
        /// Отчет о выезде
        /// </summary>
        public Report? Report { get; set; }
    }
}
