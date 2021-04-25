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
	/// Команда чтения профиля
	/// </summary>
	public sealed class ProfileReadCommand : IReadCommand<ProfileResult>
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileReadCommand(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="profileId">Идентификатор профиля</param>
        /// <returns>Информация о профиле</returns>
        public async Task<ProfileResult> ExecuteAsync(Guid profileId)
        {
            var profile = await _profileRepository.GetAsync(profileId);

            return DomainEntityConverter.ConvertToResult(profile: profile);
        }

        public Task<IEnumerable<ProfileResult>> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}