using Sbran.Domain.Entities.Chat;
using Sbran.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IChatRoomRepository
    {
        Task<List<ChatRoom>> GetAllAsync();

        Task<ChatRoom> GetAsync(Guid id);

        Task<ChatRoom> CreateAsync(ChatRoom model);

        Task DeleteAsync(Guid id);

    }
}
