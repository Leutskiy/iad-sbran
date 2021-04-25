using Sbran.CQS.Converters;
using Sbran.CQS.Read.Results;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities.Chat;
using Sbran.Domain.Entities.System;
using Sbran.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.CQS.Read
{
    public sealed class ChatMessageWriteCommand
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IChatRoomListRepository _chatRoomListRepository;
        private readonly DomainContext _domainContext;
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IUserRepository _userRepository;

        public ChatMessageWriteCommand(
            IChatMessageRepository chatMessageRepository,
            IChatRoomListRepository chatRoomListRepository,
            IChatRoomRepository chatRoomRepository,
            IUserRepository userRepository,
            DomainContext domainContext
            )
        {
            _chatMessageRepository = chatMessageRepository;
            _chatRoomListRepository = chatRoomListRepository;
            _chatRoomRepository = chatRoomRepository;
            _domainContext = domainContext;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Функция создания сообщения
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ChatMessageResult> CreateAsync(Guid profileId, MessageDto model)
        {
            var user = await _userRepository.GetWithId(profileId);
            var userTo = await _userRepository.GetProfileForUserName(model.account);
            if (user == null)
            {
                return null;
            }
            return await CreateMessage(
                model: model,
                userTo: userTo,
                user: user
                );
        }

        public async Task<ChatMessageResult> CreateForEmptyRoomAsync(Guid profileId, MessageDto model)
        {
            var user = await _userRepository.GetWithId(profileId);
            var userTemp = await _userRepository.GetProfileForUserName(model.account);

            var allRoomLists = await _chatRoomListRepository.GetAllAsync();

            var roomList = allRoomLists.Where(e => e.UserId == user.Id).ToList();
            var roomListTwo = allRoomLists.Where(e => e.UserId == userTemp.Id).ToList();

            //var uniqRoom = await _domainContext.ChatRooms.FirstOrDefaultAsync(e => e.Id == (roomList.Union));

            var dto = new ChatMessageResult();
            var message = new ChatMessage();
            if (roomList.Count > 0 && roomListTwo.Count > 0)
            {
                var temp = roomList.FirstOrDefault(e => roomListTwo.FirstOrDefault(a => a.ChatRoomId == e.ChatRoomId) != null);
                //var temp = roomList.Intersect(roomListTwo).FirstOrDefault();
                if (temp != null)
                {
                    return await CreateMessage(
                 model: model,
                 userTo: userTemp,
                 user: user
                 );
                }
            }
            var room = new ChatRoom(model.chatRoomId);
            var chatRoomListTo = new ChatRoomList();
            chatRoomListTo.ChatRoomId = room.Id;
            chatRoomListTo.UserId = user.Id;
            var chatRoomListFrom = new ChatRoomList();
            chatRoomListFrom.ChatRoomId = room.Id;
            chatRoomListFrom.UserId = userTemp.Id;

            await _chatRoomRepository.CreateAsync(room);
            await _chatRoomListRepository.CreateAsync(chatRoomListFrom);
            await _chatRoomListRepository.CreateAsync(chatRoomListTo);
            return await CreateMessage(
                model: model,
                userTo: userTemp,
                user: user
            );
        }

        public async Task<ChatMessageResult> CreateMessage(MessageDto model, User userTo, User user)
        {
            var message = new ChatMessage
            {
                ChatRoomId = model.chatRoomId,
                UserId = user.Id,
                Message = model.message,
                DateTime = DateTime.Now
            };
            await _chatMessageRepository.CreateAsync(message);
            await _domainContext.SaveChangesAsync();

            return DomainEntityConverter.ConvertToResult(
                        user: user,
                        userTo: userTo,
                        chatMessage: message
                    );
        }
    }
}
