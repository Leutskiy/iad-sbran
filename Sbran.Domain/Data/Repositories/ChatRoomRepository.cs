using Microsoft.EntityFrameworkCore;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories
{
	/// <summary>
	/// Репозиторий комнаты
	/// </summary>
	public sealed class ChatRoomRepository : IChatRoomRepository
    {
        private readonly DomainContext _domainContext;
        /// <summary>
        /// Инициализация репозитория
        /// </summary>
        /// <param name="domainContext"></param>
        public ChatRoomRepository(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        /// <summary>
        /// Функция создания комнаты
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ChatRoom> CreateAsync(ChatRoom model)
        {
            await _domainContext.ChatRooms.AddAsync(model);
            await _domainContext.SaveChangesAsync();
            return model;
        }

        /// <summary>
        /// Функция удаления комнаты
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var model = await _domainContext.ChatRooms.FirstOrDefaultAsync(e => e.Id == id);
            if (model == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }
            _domainContext.ChatRooms.Remove(model);
        }

        /// <summary>
        /// Функция получения списка всех комнат
        /// </summary>
        /// <returns></returns>
        public Task<List<ChatRoom>> GetAllAsync() => _domainContext.ChatRooms.ToListAsync();

        /// <summary>
        /// Функция получения комнаты по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ChatRoom> GetAsync(Guid id)
        {
            var model = await _domainContext.ChatRooms.FirstOrDefaultAsync(e => e.Id == id);
            if (model == null)
            {
                return null;
            }
            return model;
        }
    }
}
