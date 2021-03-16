using Microsoft.EntityFrameworkCore;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities.Chat;
using Sbran.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories
{
    /// <summary>
    /// Репозиторий сообщения
    /// </summary>
    public sealed class СhatMessageRepository : IChatMessageRepository
    {
        private readonly DomainContext _domainContext;
        /// <summary>
        /// Инициализация репозитория
        /// </summary>
        /// <param name="domainContext"></param>
        public СhatMessageRepository(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        /// <summary>
        /// Функция создания сообщения
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ChatMessage> CreateAsync(ChatMessage chatMessage)
        {
            await _domainContext.AddAsync(chatMessage);
            return chatMessage;
        }

        /// <summary>
        /// Функция удаления сообщения
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var model = await _domainContext.ChatMessages.FirstOrDefaultAsync(e => e.Id == id);
            if (model == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }
            _domainContext.ChatMessages.Remove(model);
        }

        /// <summary>
        /// Функция получения списка всех сообщений
        /// </summary>
        /// <returns></returns>
        public Task<List<ChatMessage>> GetAllAsync() => _domainContext.ChatMessages.ToListAsync();

        /// <summary>
        /// Функция получения сообщения по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ChatMessage> GetAsync(Guid id)
        {
            var model = await _domainContext.ChatMessages.FirstOrDefaultAsync(e => e.Id == id);
            if (model == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }
            return model;
        }

        public async Task<List<ChatMessage>> GetForChatRoomId(Guid id)
        {
            return await _domainContext.ChatMessages
                .Where(e => e.ChatRoomId == id)
                .ToListAsync();
        }

    }
}
