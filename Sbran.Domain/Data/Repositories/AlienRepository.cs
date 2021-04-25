using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Shared.Contracts;

namespace Sbran.Domain.Data.Repositories
{
	/// <summary>
	/// Репозиторий иностранцев
	/// </summary>
	public sealed class AlienRepository : IAlienRepository
    {
        private readonly IContactRepository _contactRepository;
        private readonly IPassportRepository _passportRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IStateRegistrationRepository _stateRegistrationRepository;
        private readonly DomainContext _domainContext;

		public AlienRepository(
			IContactRepository contactRepository,
			IPassportRepository passportRepository,
			IOrganizationRepository organizationRepository,
			IStateRegistrationRepository stateRegistrationRepository,
            DomainContext domainContext)
		{
			_contactRepository = contactRepository;
			_passportRepository = passportRepository;
			_organizationRepository = organizationRepository;
			_stateRegistrationRepository = stateRegistrationRepository;
			_domainContext = domainContext;
		}

        /// <summary>
        /// Получить всех иностранцев
        /// </summary>
        /// <returns>Иностранцы</returns>
        public Task<List<Alien>> GetAllAsync()
        {
            return _domainContext.Aliens.ToListAsync();
        }

        /// <summary>
        /// Получить иностранца
        /// </summary>
        /// <param name="id">Идентификатор иностранца</param>
        /// <returns>Иностранец</returns>
        public async Task<Alien> GetAsync(Guid id)
        {
            var alien = await _domainContext.Aliens.FindAsync(id);
            if (alien == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }

            return alien;
        }

        /// <summary>
        /// Создать иностранца
        /// </summary>
        /// <param name="contactId">Контакт</param>
        /// <param name="passportId">Паспорт</param>
        /// <param name="organizationId">Организация</param>
        /// <param name="stateRegistrationId">Государственная регистрация</param>
        /// <param name="position">Должность</param>
        /// <param name="workPlace">Место работы</param>
        /// <param name="workAddress">Рабочий адрес</param>
        /// <param name="stayAddress">Адрес пребывания</param>
        /// <returns>Идентификатор иностранца</returns>
        public Alien Create()
        {
            var createdAlien = new Alien();

            _domainContext.Aliens.Add(createdAlien);

            return createdAlien;
        }

        /// <summary>
        /// Добавить иностранца
        /// </summary>
        /// <param name="addedAlien">DTO добавляемого иностранца</param>
        public Alien Add(InviteeDto addedAlien)
        {
            var newAlien = Create();

            if (addedAlien.AlienContact != null)
            {
                var contact = _contactRepository.Add(addedAlien.AlienContact);
                newAlien.SetContact(contact: contact);
            }

            if (addedAlien.AlienPassport != null)
            {
                var passport = _passportRepository.Add(addedAlien.AlienPassport);
                newAlien.SetPassport(passport: passport);
            }

            if (addedAlien.AlienOrganization != null)
            {
                var organization = _organizationRepository.Add(addedAlien.AlienOrganization);
                newAlien.SetOrganization(organization: organization);
            }

            if (addedAlien.AlienStateRegistration != null)
            {
                var stateRegistration = _stateRegistrationRepository.Add(addedAlien.AlienStateRegistration);
                newAlien.SetStateRegistration(stateRegistration: stateRegistration);
            }

            if (addedAlien.AlienJob != null)
            {
                newAlien.SetWorkPlace(addedAlien.AlienJob.WorkPlace);
                newAlien.SetPosition(addedAlien.AlienJob.Position);
            }

            return newAlien;
        }

        /// <summary>
        /// Удалить иностранца
        /// </summary>
        /// <param name="id">Идентификатор иностранца</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            Contract.Argument.IsNotEmptyGuid(id, nameof(id));

            var deletedAlien = await GetAsync(id);

            _domainContext.Aliens.Remove(deletedAlien);
        }
    }
}