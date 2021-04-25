using Sbran.CQS.Converters;
using Sbran.CQS.Read.Contracts;
using Sbran.CQS.Read.Results;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.CQS.Read
{
    public sealed class ChatRoomReadCommand : IChatRoomReadCommand
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IChatRoomListRepository _chatRoomListRepository;
        private readonly IUserRepository _userRepository;

        public ChatRoomReadCommand(
            IChatMessageRepository chatMessageRepository,
            IChatRoomListRepository chatRoomListRepository,
            IUserRepository userRepository)
        {
            _chatMessageRepository = chatMessageRepository;
            _chatRoomListRepository = chatRoomListRepository;
            _userRepository = userRepository;
        }

        public async Task<List<ChatRoomResult>> GetAllRooms(Guid profileId, string name)
        {
            var listRooms = await _chatRoomListRepository.GetAllAsync();
            var usersFull = await _userRepository.GetUsersFull();
            var me = usersFull.FirstOrDefault(e => e.ProfileId == profileId);
            var users = usersFull
                .Where(e => e.Account.ToUpper().Contains(name.ToUpper()) && e.ProfileId != profileId)
                .ToList();
            var messages = await _chatMessageRepository.GetAllAsync();
            List<ChatRoomResult> chats = new List<ChatRoomResult>();
            foreach (var temp in users)
            {
                var tempRoom = listRooms.Where(e => e.UserId == temp.Id).ToList();
                if (tempRoom.Count > 0)
                {
                    var roomExist = listRooms
                        .FirstOrDefault(e => e.UserId == me.Id && tempRoom
                        .FirstOrDefault(a => a.ChatRoomId == e.ChatRoomId) != null);
                    if (roomExist != null)
                    {
                        var tempRoomForUser = tempRoom
                        .FirstOrDefault(a => a.ChatRoomId == roomExist.ChatRoomId);
                        if (tempRoomForUser != null)
                        {
                            var lastMessage = messages
                                .OrderByDescending(e => e.DateTime)
                                .FirstOrDefault(e => e.ChatRoomId == tempRoomForUser.ChatRoomId);
                            var tempUser = usersFull
                                .FirstOrDefault(e => e.Id.CompareTo(tempRoomForUser.UserId) == 0);

                            var tempRoomDto = DomainEntityConverter.ConvertToResult(
                                user: tempUser,
                                chatRoomList: tempRoomForUser,
                                chatMessage: lastMessage
                            );

                            chats.Add(tempRoomDto);
                        }
                    }
                    else
                    {
                        var tempRoomDto = DomainEntityConverter.ConvertToResult(
                            user: temp,
                            chatRoomList: null,
                            chatMessage: null
                        );
                        chats.Add(tempRoomDto);
                    }
                }
                else
                {
                    var tempRoomDto = DomainEntityConverter.ConvertToResult(
                        user: temp,
                        chatRoomList: null,
                        chatMessage: null
                    );
                    chats.Add(tempRoomDto);
                }
            }
            return chats;
        }

        public async Task<List<ChatRoomResult>> GetMyRooms(Guid profileId)
        {
            var listRooms = await _chatRoomListRepository.GetAllAsync();
            var usersFull = await _userRepository.GetUsersFull();
            var user = usersFull.FirstOrDefault(e => e.ProfileId == profileId);
            var messages = await _chatMessageRepository.GetAllAsync();
            var chatRooms = listRooms
                .Where(e => e.UserId.CompareTo(user.Id) == 0);
            List<ChatRoomResult> chats = new List<ChatRoomResult>();
            foreach (var temp in chatRooms)
            {
                var tempRoom = listRooms.FirstOrDefault(e => e.ChatRoomId.CompareTo(temp.ChatRoomId) == 0 && e.UserId.CompareTo(user.Id) != 0);
                if (tempRoom != null)
                {
                    var lastMessage = messages
                        .OrderByDescending(e => e.DateTime)
                        .FirstOrDefault(e => e.ChatRoomId == tempRoom.ChatRoomId);
                    var tempUser = usersFull
                        .FirstOrDefault(e => e.Id.CompareTo(tempRoom.UserId) == 0);
                    var tempRoomDto = DomainEntityConverter.ConvertToResult(
                               user: tempUser,
                               chatRoomList: tempRoom,
                               chatMessage: lastMessage
                           );
                    chats.Add(tempRoomDto);
                }
            }

            return chats
                .OrderByDescending(e => e.lastmessagedate)
                .ToList();
        }
    }
}