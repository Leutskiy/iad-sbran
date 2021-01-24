using System;
using System.Threading.Tasks;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Models;
using Sbran.Shared.Contracts;

namespace ICS.Application.Commands.Read
{
	// TODO: нужно переделать, так как мы не сейвим после выполнения операции
	/// <summary>
	/// Команда персистенса информации по приглашенному иностранцу
	/// </summary>
	public sealed class AlienWriteCommand
    {
        private readonly IContactRepository _contactRepository;
        private readonly IPassportRepository _passportRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IStateRegistrationRepository _stateRegistrationRepository;
        private readonly IAlienRepository _alienRepository;

        public AlienWriteCommand(
            IContactRepository contactRepository,
            IPassportRepository passportRepository,
            IOrganizationRepository organizationRepository,
            IStateRegistrationRepository stateRegistrationRepository,
            IAlienRepository alienRepository)
        {
            _contactRepository = contactRepository;
            _passportRepository = passportRepository;
            _organizationRepository = organizationRepository;
            _stateRegistrationRepository = stateRegistrationRepository;
            _alienRepository = alienRepository;
        }

        // TODO: переделать на запросы, которые будут принимать пути для выполнения Include
        /// <summary>
        /// Выполнить команду Добавить или Обновить паспортные данные приглашенного иностранца
        /// </summary>
        /// <param name="employeeId">Идентификатор иностранца</param>
        /// <param name="passportDto">Паспортные данные для создания паспорта</param>
        /// <returns>Идентификатор паспорта</returns>
        public async Task<Guid> AddOrUpdatePassportAsync(Guid alienId, PassportDto passportDto)
        {
            // TODO: проверить идентификатор, что не Guid.Empty

            var alien = await _alienRepository.GetAsync(alienId);
            if (alien.PassportId.HasValue)
            {
                await _passportRepository.UpdateAsync(alien.PassportId.Value, passportDto);
            }
            else
            {
                var passport = _passportRepository.Add(passportDto);
                alien.SetPassport(passport);
            }

            return alien.Passport!.Id;
        }

        // TODO: переделать на запросы, которые будут принимать пути для выполнения Include
        /// <summary>
        /// Выполнить команду Добавить или Обновить контактные данные приглашенного иностранца
        /// </summary>
        /// <param name="employeeId">Идентификатор иностранца</param>
        /// <param name="contactDto">Контактные данные для создания контакта</param>
        /// <returns>Идентификатор контакта</returns>
        public async Task<Guid> AddOrUpdateContactAsync(Guid alienId, ContactDto contactDto)
        {
            // TODO: проверить идентификатор, что не Guid.Empty

            var alien = await _alienRepository.GetAsync(alienId);
            if (alien.ContactId.HasValue)
            {
                await _contactRepository.UpdateAsync(alien.ContactId.Value, contactDto);
            }
            else
            {
                var contact = _contactRepository.Add(contactDto);
                alien.SetContact(contact);
            }

            return alien.Contact!.Id;
        }

        // TODO: переделать на запросы, которые будут принимать пути для выполнения Include
        /// <summary>
        /// Выполнить команду Добавить или Обновить данные по организации сотрудника
        /// </summary>
        /// <param name="employeeId">Идентификатор иностранца</param>
        /// <param name="organizationDto">Данные по организации для создания организации</param>
        /// <returns>Идентификатор контакта</returns>
        public async Task<Guid> AddOrUpdateOrganizationAsync(Guid alienId, OrganizationDto organizationDto)
        {
            // TODO: проверить идентификатор, что не Guid.Empty

            var alien = await _alienRepository.GetAsync(alienId);
            if (alien.OrganizationId.HasValue)
            {
                await _organizationRepository.UpdateAsync(alien.OrganizationId.Value, organizationDto);
            }
            else
            {
                // TODO: переделать государственную регистрацию так, чтобы для сотрудников из одного места работы была одна гос. регистрация организации
                var organization = _organizationRepository.Add(organizationDto);
                alien.SetOrganization(organization);
            }

            return alien.Organization!.Id;
        }

        // TODO: переделать на запросы, которые будут принимать пути для выполнения Include
        /// <summary>
        /// Выполнить команду Добавить или Обновить государственные регистрацонные данные приглашенного иностранца
        /// </summary>
        /// <param name="employeeId">Идентификатор иностранца</param>
        /// <param name="stateRegistrationDto">Государственные регистрацонные данные</param>
        /// <returns>Идентификатор государственных регистрационных данных</returns>
        public async Task<Guid> AddOrUpdateStateRegistrationAsync(Guid alienId, StateRegistrationDto stateRegistrationDto)
        {
            // TODO: проверить идентификатор, что не Guid.Empty

            var alien = await _alienRepository.GetAsync(alienId);
            if (alien.StateRegistrationId.HasValue)
            {
                await _stateRegistrationRepository.UpdateAsync(alien.StateRegistrationId.Value, stateRegistrationDto);
            }
            else
            {
                var newStateRegistration = _stateRegistrationRepository.Add(stateRegistrationDto);
                alien.SetStateRegistration(newStateRegistration);
            }

            return alien.StateRegistration!.Id;
        }

        /// <summary>
        /// Выполнить команду Обновить адрес пребывания иностранца
        /// </summary>
        /// <param name="invitationId">Идентификатор приглашения</param>
        /// <param name="stayAddress">Адрес пребывания иностранца</param>
        public async Task<Guid> UpdateAlienStayAddressAsync(Guid alienId, string stayAddress)
        {
            Contract.Argument.IsNotEmptyGuid(alienId, nameof(alienId));

            var alien = await _alienRepository.GetAsync(alienId);
            alien.SetStayAddress(stayAddress);

            return alien.Id;
        }

        /// <summary>
        /// Выполнить команду Обновить информацию по работе иностранца
        /// </summary>
        /// <param name="alienId">Идентификатор иностранца</param>
        /// <param name="jobDto">Данные по работе иностранца</param>
        /// <returns>Идентификатор иностранца</returns>
        public async Task<Guid> UpdateAlienJobAsync(Guid alienId, AlienJobDto jobDto)
        {
            Contract.Argument.IsNotEmptyGuid(alienId, nameof(alienId));

            var alien = await _alienRepository.GetAsync(alienId);

            alien.SetPosition(jobDto.Position);
            alien.SetWorkPlace(jobDto.WorkPlace);
            alien.SetWorkAddress(jobDto.WorkAddress);

            return alien.Id;
        }
    }




}
