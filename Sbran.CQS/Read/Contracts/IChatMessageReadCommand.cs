using Sbran.CQS.Read.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.CQS.Read.Contracts
{
    public interface IChatMessageReadCommand
    {
        public Task<List<ChatMessageResult>> GetAllForRoomAsync(Guid roomId, string userName);
    }
}
