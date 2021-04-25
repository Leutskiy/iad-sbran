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
	/// Команда чтения иностранцев
	/// </summary>
	public sealed class AlienReadCommand : IReadCommand<AlienResult>
    {
        private readonly IAlienRepository _alienRepository;
        private readonly IReadCommand<ContactResult> _contactReadCommand;
        private readonly IReadCommand<PassportResult> _passportReadCommand;
        private readonly IReadCommand<OrganizationResult> _organizationReadCommand;
        private readonly IReadCommand<StateRegistrationResult> _stateRegistrationReadCommand;

        public AlienReadCommand(
            IAlienRepository alienRepository,
            IReadCommand<ContactResult> contactReadCommand,
            IReadCommand<PassportResult> passportReadCommand,
            IReadCommand<OrganizationResult> organizationReadCommand,
            IReadCommand<StateRegistrationResult> stateRegistrationReadCommand)
        {
            _alienRepository = alienRepository;
            _contactReadCommand = contactReadCommand;
            _passportReadCommand = passportReadCommand;
            _organizationReadCommand = organizationReadCommand;
            _stateRegistrationReadCommand = stateRegistrationReadCommand;
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="alienId">Идентификатор иностранца</param>
        /// <returns>Информация об иностранце</returns>
        public async Task<AlienResult> ExecuteAsync(Guid alienId)
        {
            var alien = await _alienRepository.GetAsync(alienId);

            var contactResult = alien.ContactId.HasValue ? await _contactReadCommand.ExecuteAsync(alien.ContactId.Value) : default;
            var passportResult = alien.PassportId.HasValue ? await _passportReadCommand.ExecuteAsync(alien.PassportId.Value) : default;
            var organizationResult = alien.OrganizationId.HasValue ? await _organizationReadCommand.ExecuteAsync(alien.OrganizationId.Value) : default;
            var stateRegistrationResult = alien.StateRegistrationId.HasValue ? await _stateRegistrationReadCommand.ExecuteAsync(alien.StateRegistrationId.Value) : default;

            return DomainEntityConverter.ConvertToResult(
                alien: alien,
                contactResult: contactResult,
                passportResult: passportResult,
                organizationResult: organizationResult,
                stateRegistrationResult: stateRegistrationResult);
        }

        public Task<IEnumerable<AlienResult>> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}