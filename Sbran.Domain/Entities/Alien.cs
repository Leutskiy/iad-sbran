using System;

namespace Sbran.Domain.Entities
{
    // TODO: сделать VO для данных работы и адреса места пребывания
	/// <summary>
	/// Иностранец
	/// </summary>
	public sealed class Alien
    {
        /// <summary>
        /// Инициализировать иностранца
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="invitationId">Идентификатор приглашения</param>
        /// <param name="contactId">Контакт</param>
        /// <param name="passportId">Паспорт</param>
        /// <param name="organizationId">Организация</param>
        /// <param name="stateRegistrationId">Государственная регистрация</param>
        /// <param name="position">Должность</param>
        /// <param name="workPlace">Место работы</param>
        /// <param name="workAddress">Адрес места работы</param>
        /// <param name="stayAddress">Адрес пребывания</param>
        public Alien()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; private set; }

		/// <summary>
		/// Идентификатор контактных данных
		/// </summary>
		public Guid? ContactId { get; private set; }

        /// <summary>
        /// Идентификатор паспортных данных
        /// </summary>
        public Guid? PassportId { get; private set; }

        /// <summary>
        /// Идентификатор организации
        /// </summary>
        public Guid? OrganizationId { get; private set; }

        /// <summary>
        /// Идентификатор государственной регистрации
        /// </summary>
        public Guid? StateRegistrationId { get; private set; }

		public Contact? Contact { get; private set; }

		public Passport? Passport { get; private set; }

		public Organization? Organization { get; private set; }

		public StateRegistration? StateRegistration { get; private set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string? Position { get; private set; }

        /// <summary>
        /// Место работы
        /// </summary>
        public string? WorkPlace { get; private set; }

        /// <summary>
        /// Адрес работы
        /// </summary>
        public string? WorkAddress { get; private set; }

        /// <summary>
        /// Адрес пребывания
        /// </summary>
        public string? StayAddress { get; private set; }

        /// <summary>
        /// Задать должность
        /// </summary>
        /// <param name="position">Должность</param>
        public void SetPosition(string? position)
        {
            if (Position == position)
            {
                return;
            }

            Position = position;
        }

        /// <summary>
        /// Задать место работы
        /// </summary>
        /// <param name="workPlace">Место работы</param>
        public void SetWorkPlace(string? workPlace)
        {
            if (WorkPlace == workPlace)
            {
                return;
            }

            WorkPlace = workPlace;
        }

        /// <summary>
        /// Адрес работы
        /// </summary>
        /// <param name="workAddress">Рабочий адрес</param>
        public void SetWorkAddress(string? workAddress)
        {
            if (WorkAddress == workAddress)
            {
                return;
            }

            WorkAddress = workAddress;
        }

        /// <summary>
        /// Задать адрес пребывания
        /// </summary>
        /// <param name="stayAddress">Адрес пребывания</param>
        public void SetStayAddress(string? stayAddress)
        {
            if (StayAddress == stayAddress)
            {
                return;
            }

            StayAddress = stayAddress;
        }

        /// <summary>
        /// Задать контактные данные
        /// </summary>
        /// <param name="contactId">Контактые данные</param>
        public void SetContact(Contact contact)
        {
            if (ContactId == contact.Id)
            {
                return;
            }

            ContactId = contact.Id;
            Contact = contact;
        }

        /// <summary>
        /// Задать паспортные данные
        /// </summary>
        /// <param name="passportId">Паспортные данные</param>
        public void SetPassport(Passport passport)
        {
            if (PassportId == passport.Id)
            {
                return;
            }

            PassportId = passport.Id;
            Passport = passport;
        }

        /// <summary>
        /// Задать организацию
        /// </summary>
        /// <param name="organizationId">Организация</param>
        public void SetOrganization(Organization organization)
        {
            if (OrganizationId == organization.Id)
            {
                return;
            }

            OrganizationId = organization.Id;
            Organization = organization;
        }

        /// <summary>
        /// Задать государственные регистрационные номера
        /// </summary>
        /// <param name="stateRegistrationId">Государственные регистрационные номера</param>
        public void SetStateRegistration(StateRegistration stateRegistration)
        {
            if (StateRegistrationId == stateRegistration.Id)
            {
                return;
            }

            StateRegistrationId = stateRegistration.Id;
            StateRegistration = stateRegistration;
        }
    }
}