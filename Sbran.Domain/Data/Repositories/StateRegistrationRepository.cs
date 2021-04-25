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
	/// Репозиторий регистраций государственных номеров
	/// </summary>
	public sealed class StateRegistrationRepository : IStateRegistrationRepository
    {
        private readonly DomainContext _domainContext;

        public StateRegistrationRepository(DomainContext databaseContext)
        {
            _domainContext = databaseContext;
        }

        /// <summary>
        /// Получить все государственные регистрации
        /// </summary>
        /// <returns>Государственные регистрации</returns>
        public Task<List<StateRegistration>> GetAllAsync()
        {
            return  _domainContext.Set<StateRegistration>().ToListAsync();
        }

        /// <summary>
        /// Получить государственные регистрации по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор государственной регистрации</param>
        /// <returns>Государственная регистрация</returns>
        public async Task<StateRegistration> GetAsync(Guid id)
        {
            Contract.Argument.IsNotEmptyGuid(id, nameof(id));

            var stateRegistration = await _domainContext.Set<StateRegistration>().FindAsync(id);

            if (stateRegistration == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }

            return stateRegistration;
        }

        /// <summary>
        /// Создать государственную регистрацию
        /// </summary>
        /// <returns>Идентификатор государственной регистрации</returns>
        public StateRegistration Create()
        {
            var createdStateRegistration = new StateRegistration();

            var newStateRegistration = _domainContext.Set<StateRegistration>().Add(createdStateRegistration);

            return newStateRegistration.Entity;
        }

        /// <summary>
        /// Добавить государственную регистрацию
        /// </summary>
        /// <param name="addedStateRegistration">Добавляемая государственная регистрация</param>
        /// <returns>Государственная регистрация</returns>
        public StateRegistration Add(StateRegistrationDto addedStateRegistration)
        {
            var createdStateRegistration = Create();

            createdStateRegistration.SetInn(addedStateRegistration.Inn);
            createdStateRegistration.SetOgrnip(addedStateRegistration.Ogrnip);

            return createdStateRegistration;
        }

        /// <summary>
        /// Обновить государственную регистрацию
        /// </summary>
        /// <param name="stateRegistrationId">Идентификатор обновляемой государственной регистрации</param>
        /// <param name="stateRegistrationDto">Данные для полного обновления государственной регистрации</param>
        public async Task UpdateAsync(
            Guid stateRegistrationId,
            StateRegistrationDto stateRegistrationDto)
        {
            var currentStateRegistration = await GetAsync(stateRegistrationId);
            currentStateRegistration.SetInn(stateRegistrationDto.Inn);
            currentStateRegistration.SetOgrnip(stateRegistrationDto.Ogrnip);
        }

        /// <summary>
        /// Удалить государственную регистрацию
        /// </summary>
        /// <param name="id">Идентификатор государственной регистрации</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            Contract.Argument.IsNotEmptyGuid(id, nameof(id));

            var deletedStateRegistration = await GetAsync(id);

            _domainContext.Set<StateRegistration>().Remove(deletedStateRegistration);
        }
    }
}