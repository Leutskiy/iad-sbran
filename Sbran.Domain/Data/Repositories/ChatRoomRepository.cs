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
    /// Репозиторий комнаты
    /// </summary>
    public sealed class ChatRoomRepository : IChatRoomRepository
    {
        private readonly DomainContext _domainContext;
        private readonly SystemContext _systemContext;
        /// <summary>
        /// Инициализация репозитория
        /// </summary>
        /// <param name="domainContext"></param>
        public ChatRoomRepository(DomainContext domainContext, SystemContext systemContext)
        {
            _domainContext = domainContext;
            _systemContext = systemContext;
        }

        /// <summary>
        /// Функция создания комнаты
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ChatRoom> CreateAsync(ChatRoom model)
        {
            await _domainContext.ChatRooms.AddAsync(model);
            await _domainContext.SaveChangesAsync();
            return model;
        }

        /// <summary>
        /// Функция удаления комнаты
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var model = await _domainContext.ChatRooms.FirstOrDefaultAsync(e => e.Id == id);
            if (model == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }
            _domainContext.ChatRooms.Remove(model);
        }

        /// <summary>
        /// Функция получения списка всех комнат
        /// </summary>
        /// <returns></returns>
        public async Task<List<ChatRoom>> GetAllAsync() => await _domainContext.ChatRooms.ToListAsync();

        public async Task<List<ChatRoomDto>> GetAllRooms(Guid profileId, string name)
        {
            var listRooms = await _domainContext.ChatRoomLists
                .Include(e => e.ChatRoom)
                .ToListAsync();
            var me = await _systemContext.Users.FirstOrDefaultAsync(e => e.ProfileId == profileId);
            var users = await _systemContext.Users
                .Where(e => e.Account.ToLower().Contains(name.ToLower()) && e.ProfileId != profileId)
                .ToListAsync();
            var messages = await _domainContext.Messages.ToListAsync();
            List<ChatRoomDto> chats = new List<ChatRoomDto>();
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
                            var tempUser = await _systemContext.Users
                                .FirstOrDefaultAsync(e => e.Id.CompareTo(tempRoomForUser.UserId) == 0);
                            var tempRoomDto = new ChatRoomDto
                            {
                                image = "../../../assets/images/profilephotos/lora.jpg",
                                userid = tempUser?.Account ?? "",//tempRoom.UserId.ToString(),
                                chatRoomId = roomExist.ChatRoomId.ToString(),
                                lastmessage = lastMessage?.Message ?? "",
                                lastmessagedate = lastMessage?.DateTime.ToString("dd.MM.yyyy hh:mm") ?? "",
                                userfullname = tempUser?.Account ?? "",
                            };
                            chats.Add(tempRoomDto);
                        }
                    }
                    else
                    {
                        var tempRoomDto = new ChatRoomDto
                        {
                            image = "../../../assets/images/profilephotos/lora.jpg",
                            userid = temp?.Account ?? "",//tempRoom.UserId.ToString(),
                            chatRoomId = Guid.NewGuid().ToString(),
                            lastmessage = "",
                            lastmessagedate = "",
                            userfullname = temp?.Account ?? "",
                        };
                        chats.Add(tempRoomDto);
                    }
                }
                else
                {
                    var tempRoomDto = new ChatRoomDto
                    {
                        image = "../../../assets/images/profilephotos/lora.jpg",
                        userid = temp?.Account ?? "",//tempRoom.UserId.ToString(),
                        chatRoomId = Guid.NewGuid().ToString(),
                        lastmessage = "",
                        lastmessagedate = "",
                        userfullname = temp?.Account ?? "",
                    };
                    chats.Add(tempRoomDto);
                }
            }
            return chats;
        }

        /// <summary>
        /// Функция получения комнаты по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ChatRoom> GetAsync(Guid id)
        {
            var model = await _domainContext.ChatRooms.FirstOrDefaultAsync(e => e.Id == id);
            if (model == null)
            {
                return null;
            }
            return model;
        }

        public async Task<List<ChatRoomDto>> GetMyRooms(Guid profileId)
        {
            var listRooms = await _domainContext.ChatRoomLists
                .Include(e => e.ChatRoom)
                .ToListAsync();
            var user = await _systemContext.Users.FirstOrDefaultAsync(e => e.ProfileId == profileId);
            var messages = await _domainContext.Messages.ToListAsync();
            var chatRooms = listRooms
                .Where(e => e.UserId.CompareTo(user.Id) == 0);
            List<ChatRoomDto> chats = new List<ChatRoomDto>();
            foreach (var temp in chatRooms)
            {
                var tempRoom = listRooms.FirstOrDefault(e => e.ChatRoomId.CompareTo(temp.ChatRoomId) == 0 && e.UserId.CompareTo(user.Id) != 0);
                if (tempRoom != null)
                {
                    var lastMessage = messages
                        .OrderByDescending(e => e.DateTime)
                        .FirstOrDefault(e => e.ChatRoomId == tempRoom.ChatRoomId);
                    var tempUser = await _systemContext.Users
                        .FirstOrDefaultAsync(e => e.Id.CompareTo(tempRoom.UserId) == 0);
                    var tempRoomDto = new ChatRoomDto
                    {
                        image = "../../../assets/images/profilephotos/lora.jpg",
                        userid = tempUser?.Account ?? "",//tempRoom.UserId.ToString(),
                        chatRoomId = tempRoom.ChatRoomId.ToString(),
                        lastmessage = lastMessage?.Message ?? "",
                        lastmessagedate = lastMessage?.DateTime.ToString("dd.MM.yyyy hh:mm") ?? "",
                        userfullname = tempUser?.Account ?? "",
                    };
                    chats.Add(tempRoomDto);
                }
            }
            return chats.OrderByDescending(e => DateTime.Parse(e.lastmessagedate ?? DateTime.Now.ToString())).ToList();
        }
    }
}
