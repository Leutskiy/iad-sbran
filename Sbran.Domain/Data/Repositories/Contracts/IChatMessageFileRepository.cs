using Sbran.Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories.Contracts
{
	public interface IChatMessageFileRepository
    {
        Task<List<ChatMessageFile>> GetMessageFilesByIds(Guid[] messageIds);
        ChatMessageFile GetById(Guid id);
        Task<Guid> Create(ChatMessageFile chatMessage);
        Task<List<ChatMessageFile>> GetForRoomId(Guid id);
    }
}
