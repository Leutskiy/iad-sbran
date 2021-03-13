using Microsoft.EntityFrameworkCore;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories
{
    /// <summary>
    /// Репозиторий списка участников в комнате
    /// </summary>
    public sealed class ChatRoomListRepository : IChatRoomListRepository
    {
        private readonly DomainContext _domainContext;
        /// <summary>
        /// Инициализация репозитория
        /// </summary>
        /// <param name="domainContext"></param>
        public ChatRoomListRepository(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        /// <summary>
        /// Функция создания списка участников в комнате
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ChatRoomList> CreateAsync(ChatRoomList model)
        {
            await _domainContext.ChatRoomLists.AddAsync(model);
            await _domainContext.SaveChangesAsync();
            return model;
        }

        /// <summary>
        /// Функция удаления списка участников в комнате
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var model = await _domainContext.ChatRoomLists.FirstOrDefaultAsync(e => e.Id == id);
            if (model == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }
            _domainContext.ChatRoomLists.Remove(model);
        }

        /// <summary>
        /// Функция получения списка всех участников в комнате
        /// </summary>
        /// <returns></returns>
        public Task<List<ChatRoomList>> GetAllAsync() => _domainContext.ChatRoomLists.ToListAsync();

        /// <summary>
        /// Функция получения списка участников в комнате по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ChatRoomList> GetAsync(Guid id)
        {
            var model = await _domainContext.ChatRoomLists.FirstOrDefaultAsync(e => e.Id == id);
            if (model == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }
            return model;
        }
    }
}
