using Sbran.Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories.Contracts
{
	public interface IChatMessageRepository
    {
        Task<List<ChatMessage>> GetAllAsync();

        Task<ChatMessage> GetAsync(Guid id);

        Task<List<ChatMessage>> GetForChatRoomId(Guid id);

        Task<ChatMessage> CreateAsync(ChatMessage chatMessage);

        Task DeleteAsync(Guid id);
    }
}
