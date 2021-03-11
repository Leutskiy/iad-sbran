using Sbran.Domain.Entities.Chat;
using Sbran.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IMessagesRepository
    {
        Task<List<Messages>> GetAllAsync();

        Task<Messages> GetAsync(Guid id);

        Task<List<MessagesChatDto>> GetAllForRoomAsync(Guid id, string userName);

        Task<MessagesChatDto> CreateAsync(Guid profileId, MessageDto model);

        Task<MessagesChatDto> CreateForEmptyRoomAsync(Guid profileId, MessageDto model);

        Task DeleteAsync(Guid id);


    }
}
