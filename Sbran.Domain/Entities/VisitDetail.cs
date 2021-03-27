using System;

namespace Sbran.Domain.Entities
{
    /// <summary>
    /// Детали визита
    /// </summary>
    public sealed class VisitDetail
    {
        public VisitDetail()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Цель визита
        /// </summary>
        public string? Goal { get; private set; }

        /// <summary>
        /// Страна визита
        /// </summary>
        public string? Country { get; private set; }

        /// <summary>
        /// Пункты посещения
        /// </summary>
        public string? VisitingPoints { get; private set; }

        /// <summary>
        /// Период в днях
        /// </summary>
        public long? PeriodDays { get; private set; }

        /// <summary>
        /// Период пребывания
        /// </summary>
        public TimeSpan? Period => PeriodDays.HasValue ? TimeSpan.FromDays(PeriodDays.Value) : default;

        /// <summary>
        /// Дата пребытия
        /// </summary>
        public DateTime? ArrivalDate  { get; private set; }

        /// <summary>
        /// Дата депортации
        /// </summary>
        public DateTime? DepartureDate { get; private set; }

        /// <summary>
        /// Вид визы
        /// </summary>
        public string? VisaType { get; private set; }

        /// <summary>
        /// Город получения визы
        /// </summary>
        public string? VisaCity { get; private set; }

        /// <summary>
        /// Страна получения визы
        /// </summary>
        public string? VisaCountry { get; private set; }

        /// <summary>
        /// Кратность визы
        /// </summary>
        public VisaMultiplicity? VisaMultiplicity { get; private set; }
        
        /// <summary>
        /// Тип приема
        /// </summary>
        public string? TypeReception { get; private set; }

        /// <summary>
        /// Финансовые условия
        /// </summary>
        public FinancialCondition? FinancialCondition { get; private set; }

		#region setters

		/// <summary>
		/// Задать цель визита
		/// </summary>
		/// <param name="goal">Цель визита</param>
		public void SetGoal(string? goal)
        {
            if (Goal == goal)
            {
                return;
            }

            Goal = goal;
        }

        /// <summary>
        /// Задать страну визита
        /// </summary>
        /// <param name="country">Страна визита</param>
        public void SetCountry(string? country)
        {
            if (Country == country)
            {
                return;
            }

            Country = country;
        }

        /// <summary>
        /// Задать пункты посещения
        /// </summary>
        /// <param name="visitingPoints">Пункты посещения</param>
        public void SetVisitingPoints(string? visitingPoints)
        {
            if (VisitingPoints == visitingPoints)
            {
                return;
            }

            VisitingPoints = visitingPoints;
        }

        /// <summary>
        /// Задать пункты посещения по отдельности
        /// </summary>
        /// <param name="visitingPoints">Перечисление пунктов посещения</param>
        public void SetVisitingPoints(params string[]? visitingPoints)
        {
            var concatedVisitingPoints = string.Join(", ", visitingPoints);

            if (VisitingPoints == concatedVisitingPoints)
            {
                return;
            }

            VisitingPoints = concatedVisitingPoints;
        }

        /// <summary>
        /// Задать период пребывания в днях
        /// </summary>
        /// <param name="period">Период пребывания в днях</param>
        public void SetPeriodDays(long? periodDays)
        {
            if (PeriodDays == periodDays)
            {
                return;
            }

            PeriodDays = periodDays;
        }

        /// <summary>
        /// Задать дату пребытия
        /// </summary>
        /// <param name="arrivalDate">Дата пребытия</param>
        public void SetArrivalDate(DateTime? arrivalDate)
        {
            if (ArrivalDate == arrivalDate)
            {
                return;
            }

            ArrivalDate = arrivalDate;
        }

        /// <summary>
        /// Задать дату депортации
        /// </summary>
        /// <param name="departureDate">Дата депортации</param>
        public void SetDepartureDate(DateTime? departureDate)
        {
            if (DepartureDate == departureDate)
            {
                return;
            }

            DepartureDate = departureDate;
        }

        /// <summary>
        /// Задать тип визы
        /// </summary>
        /// <param name="visaType">Тип визы</param>
        public void SetVisaType(string? visaType)
        {
            if (VisaType == visaType)
            {
                return;
            }

            VisaType = visaType;
        }

        /// <summary>
        /// Задать город получения визы
        /// </summary>
        /// <param name="visaCity">Город получения визы</param>
        public void SetVisaCity(string? visaCity)
        {
            if (VisaCity == visaCity)
            {
                return;
            }

            VisaCity = visaCity;
        }

        /// <summary>
        /// Задать страну получения визы
        /// </summary>
        /// <param name="visaCountry">Страна получения визы</param>
        public void SetVisaCountry(string? visaCountry)
        {
            if (VisaCountry == visaCountry)
            {
                return;
            }

            VisaCountry = visaCountry;
        }

        /// <summary>
        /// Задать кратность визы
        /// </summary>
        /// <param name="visaMultiplicity">Кратность визы</param>
        public void SetVisaMultiplicity(VisaMultiplicity? visaMultiplicity)
        {
            if (VisaMultiplicity == visaMultiplicity)
            {
                return;
            }

            VisaMultiplicity = visaMultiplicity;
        }

		#endregion
	}
}