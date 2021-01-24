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
	/// Команда чтения государственных регистраций
	/// </summary>
	public sealed class StateRegistrationReadCommand : IReadCommand<StateRegistrationResult>
    {
        private readonly IStateRegistrationRepository _stateRegistrationRepository;

        public StateRegistrationReadCommand(IStateRegistrationRepository stateRegistrationRepository)
        {
            _stateRegistrationRepository = stateRegistrationRepository;
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="stateRegistrationId">Идентификатор государственной регистрации</param>
        /// <returns>Информация об госудраственной регистрации</returns>
        public async Task<StateRegistrationResult> ExecuteAsync(Guid stateRegistrationId)
        {
            var stateRegistration = await _stateRegistrationRepository.GetAsync(stateRegistrationId);

            return DomainEntityConverter.ConvertToResult(stateRegistration: stateRegistration);
        }

        public Task<IEnumerable<StateRegistrationResult>> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}