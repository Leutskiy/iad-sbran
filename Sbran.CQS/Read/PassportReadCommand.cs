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
	/// Команда чтения паспортных данных
	/// </summary>
	public sealed class PassportReadCommand : IReadCommand<PassportResult>
    {
        private readonly IPassportRepository _passportRepository;

        public PassportReadCommand(IPassportRepository passportRepository)
        {
            _passportRepository = passportRepository;
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="passportId">Идентификатор паспортных данных</param>
        /// <returns>Информация о паспорте</returns>
        public async Task<PassportResult> ExecuteAsync(Guid passportId)
        {
            var passport = await _passportRepository.GetAsync(passportId);

            return DomainEntityConverter.ConvertToResult(passport: passport);
        }

        public Task<IEnumerable<PassportResult>> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}