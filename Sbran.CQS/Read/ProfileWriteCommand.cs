using System;
using System.Threading.Tasks;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Models;

namespace Sbran.CQS.Read
{
	/// <summary>
	/// Команда персистенса информации по профилю пользователя
	/// </summary>
	public sealed class ProfileWriteCommand
    {
        private readonly IProfileRepository _profileRepository;
        private readonly SystemContext _systemContext;

        public ProfileWriteCommand(
            IProfileRepository profileRepository,
            SystemContext systemContext)
        {
            _profileRepository = profileRepository;
            _systemContext = systemContext;
        }

        /// <summary>
        /// Выполнить команду Обновить профиль
        /// </summary>
        /// <param name="profileId">Идентификатор профиля</param>
        /// <param name="profileDto">Данные по профилю</param>
        public async Task UpdateAsync(Guid profileId, ProfileDto profileDto)
        {
            await _profileRepository.UpdateAsync(profileId, profileDto);

            await _systemContext.SaveChangesAsync();
        }
    }
}
