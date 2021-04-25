using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;
using Sbran.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories
{
    /// <summary>
    /// Репозиторий организаций
    /// </summary>
    public sealed class OrganizationRepository : IOrganizationRepository
    {
        private readonly IStateRegistrationRepository _stateRegistrationRepository;
        private readonly DomainContext _domainContext;

		public OrganizationRepository(IStateRegistrationRepository stateRegistrationRepository, DomainContext databaseContext)
		{
			_stateRegistrationRepository = stateRegistrationRepository;
			_domainContext = databaseContext;
		}

		/// <summary>
		/// Получить все организации
		/// </summary>
		/// <returns>Организации</returns>
		public Task<List<Organization>> GetAllAsync()
        {
            return _domainContext.Set<Organization>().ToListAsync();
        }

        /// <summary>
        /// Получить организацию по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор организации</param>
        /// <returns>Организация</returns>
        public async Task<Organization> GetAsync(Guid id)
        {
            var organization = await _domainContext.Set<Organization>().FindAsync(id);

            if (organization == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }

            return organization;
        }

        /// <summary>
        /// Создать организацию
        /// </summary>
        /// <param name="stateRegistration">Государственная регистрация</param>
        /// <param name="shortName">Короткое наименование</param>
        /// <param name="legalAddress">Юридический адрес</param>
        /// <param name="scientificActivity">Научная деятельность</param>
        /// <returns>Идентификатор организации</returns>
        public Organization Create()
        {
            var createdOrganization = new Organization();

            _domainContext.Set<Organization>().Add(createdOrganization);

            return createdOrganization;
        }

        /// <summary>
        /// Добавить организацию
        /// </summary>
        /// <param name="addedOrganization">DTO добавляемой организации</param>
        public Organization Add(OrganizationDto addedOrganization)
        {
            var createdOrganization = Create();

            if (addedOrganization.StateRegistration != null)
			{
                var stateRegistration = _stateRegistrationRepository.Add(addedOrganization.StateRegistration);
                createdOrganization.SetStateRegistration(stateRegistration);
            }
            
            createdOrganization.SetName(addedOrganization.Name);
            createdOrganization.SetShortName(addedOrganization.ShortName);
            createdOrganization.SetLegalAddress(addedOrganization.LegalAddress);
            createdOrganization.SetScientificActivity(addedOrganization.ScientificActivity);

            return createdOrganization;
        }

        /// <summary>
        /// Обновить организацию
        /// </summary>
        /// <param name="currentOrganizationId">Идентификатор обновляемой организации</param>
        /// <param name="organizationDto">Данные для полного обновления организации</param>
        public async Task UpdateAsync(
            Guid organizationId,
            OrganizationDto organizationDto)
        {
            var currentOrganization = await GetAsync(organizationId);

            if (currentOrganization.StateRegistrationId.HasValue && organizationDto.StateRegistration != null)
            {
                await _stateRegistrationRepository.UpdateAsync(currentOrganization.StateRegistrationId.Value, organizationDto.StateRegistration);
            }
            else if (organizationDto.StateRegistration != null)
            {
                var stateRegistration = _stateRegistrationRepository.Add(organizationDto.StateRegistration);
                currentOrganization.SetStateRegistration(stateRegistration);
            }

            currentOrganization.Update(
                name: organizationDto.Name,
                shortName: organizationDto.ShortName,
                legalAddress: organizationDto.LegalAddress,
                scientificActivity: organizationDto.ScientificActivity);
        }

        /// <summary>
        /// Удалить организацию
        /// </summary>
        /// <param name="id">Идентификатор организации</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            Contract.Argument.IsNotEmptyGuid(id, nameof(id));

            var deletedOrganization = await GetAsync(id);

            _domainContext.Set<Organization>().Remove(deletedOrganization);
        }
    }
}