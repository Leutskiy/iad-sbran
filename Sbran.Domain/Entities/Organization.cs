using System;

namespace Sbran.Domain.Entities
{
    /// <summary>
    /// Организация
    /// </summary>
    public sealed class Organization
    {
        /// <summary>
        /// Инициализировать организацию
        /// </summary>
        public Organization()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Идентификатор государственной регистрации
        /// </summary>
        public Guid? StateRegistrationId { get; private set; }

		public StateRegistration? StateRegistration { get; private set; }

		/// <summary>
		/// Полное наименование
		/// </summary>
		public string? Name { get; private set; }

        /// <summary>
        /// Краткое наименование
        /// </summary>
        public string? ShortName { get; private set; }

        /// <summary>
        /// Юридический адрес
        /// </summary>
        public string? LegalAddress { get; private set; }

        /// <summary>
        /// Направление научной деятельности
        /// </summary>
        public string? ScientificActivity { get; private set; }

        /// <summary>
        /// Задать наименование
        /// </summary>
        /// <param name="name">Наименование</param>
        public void SetName(string? name)
        {
            if (Name == name)
            {
                return;
            }

            Name = name;
        }

        /// <summary>
        /// Задать короткое наименование
        /// </summary>
        /// <param name="shortName">Короткое наименование</param>
        public void SetShortName(string? shortName)
        {
            if (ShortName == shortName)
            {
                return;
            }

            ShortName = shortName;
        }

        /// <summary>
        /// Задать юридический адрес
        /// </summary>
        /// <param name="legalAddress">Юридический адрес</param>
        public void SetLegalAddress(string? legalAddress)
        {
            if (LegalAddress == legalAddress)
            {
                return;
            }

            LegalAddress = legalAddress;
        }

        /// <summary>
        /// Задать научную направленность
        /// </summary>
        /// <param name="scientificActivity">Научное направление</param>
        public void SetScientificActivity(string? scientificActivity)
        {
            if (ScientificActivity == scientificActivity)
            {
                return;
            }

            ScientificActivity = scientificActivity;
        }

        /// <summary>
        /// Задать государственную регистрацию
        /// </summary>
        /// <param name="stateRegistrationId">Государственная регистрация</param>
        public void SetStateRegistration(StateRegistration stateRegistration)
        {
            if (StateRegistration?.Id == stateRegistration.Id)
            {
                return;
            }

            StateRegistrationId = stateRegistration.Id;
            StateRegistration = stateRegistration;
        }

        /// <summary>
        /// Обновить все данные по организации
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="shortName">Короткое наименование</param>
        /// <param name="scientificActivity">Научная деятельность</param>
        /// <param name="legalAddress">Юридический адрес</param>
        public void Update(
            string? name,
            string? shortName,
            string? legalAddress,
            string? scientificActivity)
        {
            SetName(name);
            SetShortName(shortName);
            SetLegalAddress(legalAddress);
            SetScientificActivity(scientificActivity);
        }
    }
}