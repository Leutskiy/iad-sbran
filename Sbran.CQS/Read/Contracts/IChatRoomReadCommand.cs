using Sbran.CQS.Read.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.CQS.Read.Contracts
{
    public interface IChatRoomReadCommand
    {
        public Task<List<ChatRoomResult>> GetAllRooms(Guid profileId, string name);
        public Task<List<ChatRoomResult>> GetMyRooms(Guid profileId);
    }
}
