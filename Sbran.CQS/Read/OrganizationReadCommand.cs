using Sbran.CQS.Converters;
using Sbran.CQS.Read.Contracts;
using Sbran.CQS.Read.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Data.Repositories.Contracts;

namespace Sbran.CQS.Read
{
	/// <summary>
	/// Команда чтения организаций
	/// </summary>
	public sealed class OrganizationReadCommand : IReadCommand<OrganizationResult>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IReadCommand<StateRegistrationResult> _stateRegistrationReadCommand;

        public OrganizationReadCommand(
            IOrganizationRepository organizationRepository,
            IReadCommand<StateRegistrationResult> stateRegistrationReadCommand)
        {
            _organizationRepository = organizationRepository;
            _stateRegistrationReadCommand = stateRegistrationReadCommand;
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="organizationId">Идентификатор организации</param>
        /// <returns>Информация об организации</returns>
        public async Task<OrganizationResult> ExecuteAsync(Guid organizationId)
        {
            var organization = await _organizationRepository.GetAsync(organizationId);

            var stateRegistrationResult = organization.StateRegistrationId.HasValue
                ? await _stateRegistrationReadCommand.ExecuteAsync(organization.StateRegistrationId.Value)
                : default;

            return DomainEntityConverter.ConvertToResult(
                organization: organization,
                stateRegistrationResult: stateRegistrationResult);
        }

        public Task<IEnumerable<OrganizationResult>> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}