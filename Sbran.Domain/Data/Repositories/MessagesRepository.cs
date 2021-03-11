using Microsoft.EntityFrameworkCore;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities.Chat;
using Sbran.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories
{
    /// <summary>
    /// Репозиторий сообщения
    /// </summary>
    public sealed class MessagesRepository : IMessagesRepository
    {
        private readonly DomainContext _domainContext;
        private readonly SystemContext _systemContext;
        /// <summary>
        /// Инициализация репозитория
        /// </summary>
        /// <param name="domainContext"></param>
        public MessagesRepository(DomainContext domainContext, SystemContext systemContext)
        {
            _domainContext = domainContext;
            _systemContext = systemContext;
        }

        /// <summary>
        /// Функция создания сообщения
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<MessagesChatDto> CreateAsync(Guid profileId, MessageDto model)
        {
            var user = await _systemContext.Users.FirstOrDefaultAsync(e => e.Account == model.userId.ToString());
            var userTo = await _systemContext.Users.FirstOrDefaultAsync(e => e.ProfileId == profileId);
            if (user == null)
            {
                return null;
            }
            var message = new Messages
            {
                ChatRoomId = Guid.Parse(model.chatRoomId),
                UserId = userTo.Id,
                Message = model.message,
                DateTime = DateTime.Now
            };
            await _domainContext.Messages.AddAsync(message);
            await _domainContext.SaveChangesAsync();
            MessagesChatDto dto = new MessagesChatDto
            {
                Message = message.Message,
                profileId = userTo.ProfileId.ToString(),
                profileTo = user.ProfileId.ToString(),
                DateTime = message.DateTime.ToString(),
            };
            return dto;
        }

        public async Task<MessagesChatDto> CreateForEmptyRoomAsync(Guid profileId, MessageDto model)
        {
            var user = await _systemContext.Users.FirstOrDefaultAsync(e => e.ProfileId == profileId);
            var userTemp = await _systemContext.Users.FirstOrDefaultAsync(e => e.Account == model.userId);

            var roomList = await _domainContext.ChatRoomLists.Where(e => e.UserId == user.Id).ToListAsync();
            var roomListTwo = await _domainContext.ChatRoomLists.Where(e => e.UserId == userTemp.Id).ToListAsync();

            //var uniqRoom = await _domainContext.ChatRooms.FirstOrDefaultAsync(e => e.Id == (roomList.Union));

            var dto = new MessagesChatDto();
            var message = new Messages();
            if (roomList.Count > 0 && roomListTwo.Count > 0)
            {
                var temp = roomList.FirstOrDefault(e => roomListTwo.FirstOrDefault(a => a.ChatRoomId == e.ChatRoomId) != null);
                //var temp = roomList.Intersect(roomListTwo).FirstOrDefault();
                if (temp != null)
                {
                    message = new Messages
                    {
                        ChatRoomId = temp.ChatRoomId,
                        UserId = user.Id,
                        Message = model.message,
                        DateTime = DateTime.Now
                    };
                    await _domainContext.Messages.AddAsync(message);
                    await _domainContext.SaveChangesAsync();

                    dto = new MessagesChatDto
                    {
                        Message = message.Message,
                        DateTime = message.DateTime.ToString(),
                        profileId = user.ProfileId.ToString(),
                        profileTo = userTemp.ProfileId.ToString(),
                    };
                    return dto;
                }
            }
            var room = new ChatRoom(Guid.Parse(model.chatRoomId ?? Guid.NewGuid().ToString()));
            await _domainContext.ChatRooms.AddAsync(room);
            var chatRoomListTo = new ChatRoomList();
            chatRoomListTo.ChatRoomId = room.Id;
            chatRoomListTo.UserId = user.Id;
            var chatRoomListFrom = new ChatRoomList();
            chatRoomListFrom.ChatRoomId = room.Id;
            chatRoomListFrom.UserId = userTemp.Id;
            await _domainContext.ChatRoomLists.AddAsync(chatRoomListFrom);
            await _domainContext.ChatRoomLists.AddAsync(chatRoomListTo);
            await _domainContext.SaveChangesAsync();

            message = new Messages
            {
                ChatRoomId = Guid.Parse(model.chatRoomId),
                UserId = user.Id,
                Message = model.message,
                DateTime = DateTime.Now
            };
            await _domainContext.Messages.AddAsync(message);
            await _domainContext.SaveChangesAsync();

            dto = new MessagesChatDto
            {
                Message = message.Message,
                profileTo = userTemp.ProfileId.ToString(),
                DateTime = message.DateTime.ToString(),
                profileId = user.ProfileId.ToString(),
            };
            return dto;//Тут необходимо создать еще список участников комнаты
        }

        /// <summary>
        /// Функция удаления сообщения
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var model = await _domainContext.Messages.FirstOrDefaultAsync(e => e.Id == id);
            if (model == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }
            _domainContext.Messages.Remove(model);
        }

        /// <summary>
        /// Функция получения списка всех сообщений
        /// </summary>
        /// <returns></returns>
        public async Task<List<Messages>> GetAllAsync() => await _domainContext.Messages.ToListAsync();

        /// <summary>
        /// Функция получения списка всех сообщений
        /// </summary>
        /// <returns></returns>
        public async Task<List<MessagesChatDto>> GetAllForRoomAsync(Guid roomId, string userName)
        {
            var user = await _systemContext.Users.FirstOrDefaultAsync(a => a.Account == userName);
            return await _domainContext.Messages.Where(e => e.ChatRoomId == roomId).Select(e => new MessagesChatDto
            {
                DateTime = e.DateTime.ToString(),
                IsValid = user.Id.CompareTo(e.UserId) == 0 ? true : false,
                Message = e.Message
            })
             .ToListAsync();
        }

        /// <summary>
        /// Функция получения сообщения по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Messages> GetAsync(Guid id)
        {
            var model = await _domainContext.Messages.FirstOrDefaultAsync(e => e.Id == id);
            if (model == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }
            return model;
        }
    }
}
