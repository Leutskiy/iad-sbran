using Sbran.Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IChatRoomListRepository
    {

        Task<List<ChatRoomList>> GetAllAsync();

        Task<ChatRoomList> GetAsync(Guid id);

        Task<ChatRoomList> CreateAsync(ChatRoomList model);

        Task DeleteAsync(Guid id);

    }
}
