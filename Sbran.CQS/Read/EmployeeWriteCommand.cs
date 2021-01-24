using System;
using System.Threading.Tasks;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Models;

namespace Sbran.CQS.Read
{
	/// <summary>
	/// Команда персистенса информации по сотруднику
	/// </summary>
	public sealed class EmployeeWriteCommand
    {
        private readonly IContactRepository _contactRepository;
        private readonly IPassportRepository _passportRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IStateRegistrationRepository _stateRegistrationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly DomainContext _domainContext;

		public EmployeeWriteCommand(
			IContactRepository contactRepository,
			IPassportRepository passportRepository,
            IOrganizationRepository organizationRepository,
			IStateRegistrationRepository stateRegistrationRepository,
			IEmployeeRepository employeeRepository,
			DomainContext domainContext)
		{
			_contactRepository = contactRepository;
			_passportRepository = passportRepository;
			_organizationRepository = organizationRepository;
			_stateRegistrationRepository = stateRegistrationRepository;
			_employeeRepository = employeeRepository;
            _domainContext = domainContext;
		}

        // TODO: переделать на запросы, которые будут принимать пути для выполнения Include
        /// <summary>
        /// Выполнить команду Добавить или Обновить паспортные данные сотрудника
        /// </summary>
        /// <param name="employeeId">Идентификатор сотрудника</param>
        /// <param name="passportDto">Паспортные данные для создания паспорта</param>
        /// <returns>Идентификатор паспорта</returns>
        public async Task<Guid> AddOrUpdatePassportAsync(Guid employeeId, PassportDto passportDto)
        {
            // TODO: проверить идентификатор, что не Guid.Empty

            var employee = await _employeeRepository.GetAsync(employeeId);
            if (employee.PassportId.HasValue)
            {
                await _passportRepository.UpdateAsync(employee.PassportId.Value, passportDto);
            }
            else
            {
                var passport = _passportRepository.Add(passportDto);
                employee.SetPassport(passport);
            }

            await _domainContext.SaveChangesAsync();

            return employee.Passport!.Id;
        }

        // TODO: переделать на запросы, которые будут принимать пути для выполнения Include
        /// <summary>
        /// Выполнить команду Добавить или Обновить контактные данные сотрудника
        /// </summary>
        /// <param name="employeeId">Идентификатор сотрудника</param>
        /// <param name="contactDto">Контактные данные для создания контакта</param>
        /// <returns>Идентификатор контакта</returns>
        public async Task<Guid> AddOrUpdateContactAsync(Guid employeeId, ContactDto contactDto)
        {
            // TODO: проверить идентификатор, что не Guid.Empty

            var employee = await _employeeRepository.GetAsync(employeeId);
            if (employee.ContactId.HasValue)
            {
                await _contactRepository.UpdateAsync(employee.ContactId.Value, contactDto);
            }
            else
            {
                var contact = _contactRepository.Add(contactDto);
                employee.SetContact(contact);
            }

            await _domainContext.SaveChangesAsync();

            return employee.Contact!.Id;
        }

        // TODO: переделать на запросы, которые будут принимать пути для выполнения Include
        /// <summary>
        /// Выполнить команду Добавить или Обновить данные по организации сотрудника
        /// </summary>
        /// <param name="employeeId">Идентификатор сотрудника</param>
        /// <param name="organizationDto">Данные по организации для создания организации</param>
        /// <returns>Идентификатор контакта</returns>
        public async Task<Guid> AddOrUpdateOrganizationAsync(Guid employeeId, OrganizationDto organizationDto)
        {
            // TODO: проверить идентификатор, что не Guid.Empty

            var employee = await _employeeRepository.GetAsync(employeeId);
            if (employee.OrganizationId.HasValue)
            {               
                await _organizationRepository.UpdateAsync(employee.OrganizationId.Value, organizationDto);
            }
            else
            {
                // TODO: переделать государственную регистрацию так, чтобы для сотрудников из одного места работы была одна гос. регистрация организации
                var organization = _organizationRepository.Add(organizationDto);
                employee.SetOrganization(organization);
            }

            await _domainContext.SaveChangesAsync();

            return employee.Organization!.Id;
        }

        // TODO: переделать на запросы, которые будут принимать пути для выполнения Include
        /// <summary>
        /// Выполнить команду Добавить или Обновить государственные регистрацонные данные сотрудника
        /// </summary>
        /// <param name="employeeId">Идентификатор сотрудника</param>
        /// <param name="stateRegistrationDto">Государственные регистрацонные данные</param>
        /// <returns>Идентификатор государственных регистрационных данных</returns>
        public async Task<Guid> AddOrUpdateStateRegistrationAsync(Guid employeeId, StateRegistrationDto stateRegistrationDto)
        {
            // TODO: проверить идентификатор, что не Guid.Empty

            var employee = await _employeeRepository.GetAsync(employeeId);
            if (employee.StateRegistrationId.HasValue)
            {
                await _stateRegistrationRepository.UpdateAsync(employee.StateRegistrationId.Value, stateRegistrationDto);
            }
            else
            {
                var newStateRegistration = _stateRegistrationRepository.Add(stateRegistrationDto);
                employee.SetStateRegistration(newStateRegistration);
            }

            await _domainContext.SaveChangesAsync();

            return employee.StateRegistration!.Id;
        }

        /// <summary>
        /// Выполнить команду Обновить информацию о научной деятельности сотрудника
        /// </summary>
        /// <param name="employeeId">Идентификатор сотрудника</param>
        /// <param name="scientificInfoDto">Данные о научной деятельности сотрудника</param>
        public async Task UpdateEmployeeScientificInfoAsync(Guid employeeId, ScientificInfoDto scientificInfoDto)
        {
            // TODO: проверить идентификатор, что не Guid.Empty
            await _employeeRepository.UpdateScientificInfoAsync(employeeId, scientificInfoDto);

            await _domainContext.SaveChangesAsync();
        }

        /// <summary>
        /// Выполнить команду Обновить данные о работе сотрудника
        /// </summary>
        /// <param name="employeeId">Идентификатор сотрудника</param>
        /// <param name="jobDto">Данные о работе сотрудника</param>
        public async Task UpdateEmployeeJobAsync(Guid employeeId, JobDto jobDto)
        {
            // TODO: проверить идентификатор, что не Guid.Empty
            await _employeeRepository.UpdateJobAsync(employeeId, jobDto);

            await _domainContext.SaveChangesAsync();
        }
    }
}
