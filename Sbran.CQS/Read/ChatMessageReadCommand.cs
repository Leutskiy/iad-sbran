using Sbran.CQS.Read.Contracts;
using Sbran.CQS.Read.Results;
using Sbran.Domain.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.CQS.Read
{
    public sealed class ChatMessageReadCommand : IChatMessageReadCommand
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IChatRoomListRepository _chatRoomListRepository;
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IUserRepository _userRepository;

        public ChatMessageReadCommand(
            IChatMessageRepository chatMessageRepository,
            IChatRoomListRepository chatRoomListRepository,
            IChatRoomRepository chatRoomRepository,
            IUserRepository userRepository)
        {
            _chatMessageRepository = chatMessageRepository;
            _chatRoomListRepository = chatRoomListRepository;
            _chatRoomRepository = chatRoomRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Функция получения списка всех сообщений
        /// </summary>
        /// <returns></returns>
        public async Task<List<ChatMessageResult>> GetAllForRoomAsync(Guid roomId, string userName)
        {
            var user = await _userRepository.GetProfileForUserName(userName);
            var messages = await _chatMessageRepository.GetForChatRoomId(roomId);
            return messages.Select(e => new ChatMessageResult
            {
                DateTime = e.DateTime,
                IsValid = user.Id.CompareTo(e.UserId) == 0 ? true : false,
                Message = e.Message ?? ""
            })
             .ToList();
        }
    }
}
