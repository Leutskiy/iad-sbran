using JetBrains.Annotations;
using Sbran.Domain.Entities.System;
using System;
using System.Collections.Generic;

namespace Sbran.Domain.Entities
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public sealed class Employee
    {
        [UsedImplicitly]
        private Employee() => Id = Guid.NewGuid();

        /// <summary>
        /// Инициализировать сотрудника
        /// </summary>
        /// <param name="userId">Идентификатор пользователя системы</param>
        public Employee(Guid userId) : this()
        {
            UserId = userId;
            Invitations = new List<Invitation>();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Идентификатор пользователя системы
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Идентификатор руководителя-сотрудника
        /// </summary>
        public Guid? ManagerId { get; private set; }

        /// <summary>
        /// Идентификатор контактных данных
        /// </summary>
        public Guid? ContactId { get; private set; }

        /// <summary>
        /// Идентификатор паспорта
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

        /// <summary>
        /// Руководитель
        /// </summary>
        public Employee? Manager { get; private set; }

        /// <summary>
        /// Контакт
        /// </summary>
        public Contact? Contact { get; private set; }

        /// <summary>
        /// Паспорт
        /// </summary>
        public Passport? Passport { get; private set; }

        /// <summary>
        /// Организация
        /// </summary>
        public Organization? Organization { get; private set; }

        /// <summary>
        /// Государственная регистрация
        /// </summary>
        public StateRegistration? StateRegistration { get; private set; }

        /// TODO: сделать Lazy
        /// <summary>
        /// Все приглашения сотрудника
        /// </summary>
		public List<Invitation> Invitations { get; set; }

        /// TODO: сделать Lazy
        /// <summary>
        /// Все выезды сотрудника
        /// </summary>
        public List<Departure> Departures { get; set; }

        /// TODO: сделать Lazy
        /// <summary>
        /// Все выезды сотрудника
        /// </summary>
        public List<Membership> Memberships { get; set; }

        /// TODO: сделать Lazy
        /// <summary>
        /// Все выезды сотрудника
        /// </summary>
        public List<Publication> Publications { get; set; }
        public List<ConsularOffice> ConsularOffices { get; set; }
        public List<ScientificInterests> ScientificInterests { get; set; }

        /// <summary>
        /// Научная степень
        /// </summary>
        public string? AcademicDegree { get; private set; }

        /// <summary>
        /// Научное звание
        /// </summary>
        public string? AcademicRank { get; private set; }

        /// <summary>
        /// Образование
        /// </summary>
        public string? Education { get; private set; }

        /// <summary>
        /// Место работы
        /// </summary>
        public string? WorkPlace { get; private set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string? Position { get; private set; }

        /// <summary>
        /// Задать руководителя
        /// </summary>
        /// <param name="manager">Руководитель</param>
        public void SetManager(Employee manager)
        {
            if (Manager?.Id == manager.Id)
            {
                return;
            }

            ManagerId = manager.Id;
            Manager = manager;
        }

        /// <summary>
        /// Задать контакт
        /// </summary>
        /// <param name="contactId">Контракт</param>
        public void SetContact(Contact contact)
        {
            if (Contact?.Id == contact.Id)
            {
                return;
            }

            ContactId = contact.Id;
            Contact = contact;
        }

        /// <summary>
        /// Задать паспорт
        /// </summary>
        /// <param name="passportId">Паспорт</param>
        public void SetPassport(Passport passport)
        {
            if (Passport?.Id == passport.Id)
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
            if (Organization?.Id == organization.Id)
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
            if (StateRegistration?.Id == stateRegistration.Id)
            {
                return;
            }

            StateRegistrationId = stateRegistration.Id;
            StateRegistration = stateRegistration;
        }

        /// <summary>
        /// Задать научная степень
        /// </summary>
        /// <param name="academicDegree">Научная степень</param>
        public void SetAcademicDegree(string? academicDegree)
        {
            if (AcademicDegree == academicDegree)
            {
                return;
            }

            AcademicDegree = academicDegree;
        }

        /// <summary>
        /// Задать научное звание
        /// </summary>
        /// <param name="academicRank">Научное звание</param>
        public void SetAcademicRank(string? academicRank)
        {
            if (AcademicRank == academicRank)
            {
                return;
            }

            AcademicRank = academicRank;
        }

        /// <summary>
        /// Задать образование
        /// </summary>
        /// <param name="workPlace">Образование</param>
        public void SetEducation(string? education)
        {
            if (Education == education)
            {
                return;
            }

            Education = education;
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

    }
}