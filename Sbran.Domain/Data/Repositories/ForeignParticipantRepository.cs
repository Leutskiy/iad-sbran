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
	/// Репозиторий иностранных участников
	/// </summary>
	public sealed class ForeignParticipantRepository : IForeignParticipantRepository
    {
        private readonly IPassportRepository _passportRepository;
        private readonly DomainContext _domainContext;

		public ForeignParticipantRepository(IPassportRepository passportRepository, DomainContext domainContext)
		{
			_passportRepository = passportRepository;
			_domainContext = domainContext;
		}

		/// <summary>
		/// Получить всех иностранных участников
		/// </summary>
		/// <returns>Иностранные участники</returns>
		public Task<List<ForeignParticipant>> GetAllAsync()
        {
            return _domainContext.ForeignParticipants.ToListAsync();
        }

        /// <summary>
        /// Получить иностранного участника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор иностранного участника</param>
        /// <returns>Иностранный участник</returns>
        public async Task<ForeignParticipant> GetAsync(Guid id)
        {
            Contract.Argument.IsNotEmptyGuid(id, nameof(id));

            var foreignParticipant = await _domainContext.ForeignParticipants.FindAsync(id);
            if (foreignParticipant == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }

            return foreignParticipant;
        }

        /// <summary>
        /// Создать иностранного участника
        /// </summary>
        /// <returns>Идентификатор инстранного участника</returns>
        public ForeignParticipant Create()
        {
            var createdForeignParticipant = new ForeignParticipant();

            _domainContext.ForeignParticipants.Add(createdForeignParticipant);

            return createdForeignParticipant;
        }

        /// <summary>
        /// Удалить иностранного участника
        /// </summary>
        /// <param name="id">Идентификатор иностранного участника</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            Contract.Argument.IsNotEmptyGuid(id, nameof(id));

            var deletedForeignParticipant = await _domainContext.ForeignParticipants.FirstOrDefaultAsync(foreignParticipantItem => foreignParticipantItem.Id == id);
            if (deletedForeignParticipant == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }

            _domainContext.ForeignParticipants.Remove(deletedForeignParticipant);
        }

        /// <summary>
        /// Добавить иностранного участника
        /// </summary>
        /// <param name="addedForeignParticipant">DTO добавляемого иностранного участника</param>
        /// <returns>Иностранный участник</returns>
        public ForeignParticipant Add(ForeignParticipantDto addedForeignParticipant)
        {
            var foreignParticipant = Create();

            if (addedForeignParticipant.Passport != null)
			{
                var foreignParticipantPassport = _passportRepository.Add(addedPassport: addedForeignParticipant.Passport);
                foreignParticipant.SetPassport(foreignParticipantPassport);
            }

            return foreignParticipant;
        }
    }
}