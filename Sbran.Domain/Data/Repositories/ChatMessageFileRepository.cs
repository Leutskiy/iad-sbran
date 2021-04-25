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
    public sealed class ChatMessageFileRepository : IChatMessageFileRepository
    {
        private readonly DomainContext _domainContext;

        public ChatMessageFileRepository(DomainContext databaseContext)
        {
            _domainContext = databaseContext;
        }

        public async Task<Guid> Create(ChatMessageFile chatMessage)
        {
            await _domainContext.ChatMessageFiles.AddAsync(chatMessage);
            return chatMessage.Id;
        }

        public ChatMessageFile GetById(Guid id) => _domainContext.ChatMessageFiles.FirstOrDefault(e => e.Id == id);

        public async Task<List<ChatMessageFile>> GetForRoomId(Guid id)
        {
            return await _domainContext.ChatMessageFiles.Include(e => e.ChatMessage).Where(e => e.ChatMessage.ChatRoomId == id).ToListAsync();
        }
    }
}
