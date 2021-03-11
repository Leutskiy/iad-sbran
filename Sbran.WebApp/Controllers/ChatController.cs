using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Sbran.CQS.Read;
using Sbran.CQS.Read.Results;
using Sbran.Domain.Models;
using Sbran.Shared.Contracts;
using System.Collections.Generic;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities.Chat;

namespace Sbran.WebApp.Controllers
{
    /// <summary>
    /// Контроллер информации по сотруднику
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/v1/chat")]
    public class ChatController : ControllerBase
    {
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IChatRoomListRepository _chatRoomListRepository;
        private readonly IMessagesRepository _messagesRepository;

        public ChatController(
            IChatRoomListRepository chatRoomListRepository,
            IChatRoomRepository chatRoomRepository,
            IMessagesRepository messagesRepository)
        {
            Contract.Argument.IsNotNull(chatRoomListRepository, nameof(chatRoomListRepository));
            Contract.Argument.IsNotNull(chatRoomRepository, nameof(chatRoomRepository));
            Contract.Argument.IsNotNull(messagesRepository, nameof(messagesRepository));

            _chatRoomRepository = chatRoomRepository;
            _chatRoomListRepository = chatRoomListRepository;
            _messagesRepository = messagesRepository;
        }

        [HttpGet]
        [Route("{chatRoomId:guid}/{userName}")]
        public async Task<List<MessagesChatDto>> GetAsync(Guid chatRoomId, string userName)
        {
            Contract.Argument.IsNotEmptyGuid(chatRoomId, nameof(chatRoomId));
            return await _messagesRepository.GetAllForRoomAsync(chatRoomId, userName);
        }

        [HttpPost]
        [Route("{chatRoomId:guid}/send/{profileId:guid}")]
        public async Task<MessagesChatDto> CreateAsync(Guid chatRoomId, Guid profileId, [FromBody] MessageDto messageDto)
        {
            Contract.Argument.IsNotNull(messageDto, nameof(messageDto));
            var chatRoom = await _chatRoomRepository.GetAsync(chatRoomId);
            if (chatRoom == null)
            {
                messageDto.chatRoomId = chatRoomId.ToString();
                return await _messagesRepository.CreateForEmptyRoomAsync(profileId, messageDto);
            }
            return await _messagesRepository.CreateAsync(profileId, messageDto);
        }

        [HttpGet]
        [Route("{profileId:guid}/myrooms")]
        public async Task<List<ChatRoomDto>> GetRoomsAsync(Guid profileId)
        {
            Contract.Argument.IsNotEmptyGuid(profileId, nameof(profileId));
            var temp = await _chatRoomRepository.GetMyRooms(profileId);
            return temp;
        }

        [HttpGet]
        [Route("{profileId:guid}/allUsers/{userName}")]
        public async Task<List<ChatRoomDto>> GetRoomsAllRooms(Guid profileId, string? userName)
        {
            if (userName == null)
            {
                List<ChatRoomDto> dto = new List<ChatRoomDto>();
                return dto;
            }
            Contract.Argument.IsNotEmptyGuid(profileId, nameof(profileId));
            var temp = await _chatRoomRepository.GetAllRooms(profileId, userName);
            return temp;
        }

    }
}