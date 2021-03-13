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
using Sbran.CQS.Read.Contracts;

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
        private readonly IChatRoomReadCommand _chatRoomReadCommand;
        private readonly ChatMessageWriteCommand _chatMessageWriteCommand;
        private readonly IChatMessageReadCommand _chatMessageReadCommand;
        private readonly IChatRoomRepository _chatRoomRepository;

        public ChatController(
            IChatRoomReadCommand chatRoomReadCommand,
            ChatMessageWriteCommand chatMessageWriteCommand,
            IChatMessageReadCommand chatMessageReadCommand,
            IChatRoomRepository chatRoomRepository
            )
        {
            Contract.Argument.IsNotNull(chatRoomReadCommand, nameof(chatRoomReadCommand));
            Contract.Argument.IsNotNull(chatMessageWriteCommand, nameof(chatMessageWriteCommand));
            Contract.Argument.IsNotNull(chatMessageReadCommand, nameof(chatMessageReadCommand));
            Contract.Argument.IsNotNull(chatRoomRepository, nameof(chatRoomRepository));

            _chatRoomReadCommand = chatRoomReadCommand;
            _chatMessageWriteCommand = chatMessageWriteCommand;
            _chatMessageReadCommand = chatMessageReadCommand;
            _chatRoomRepository = chatRoomRepository;
        }

        [HttpGet]
        [Route("{chatRoomId:guid}/{userName}")]
        public async Task<List<ChatMessageResult>> GetAsync(Guid chatRoomId, string userName)
        {
            Contract.Argument.IsNotEmptyGuid(chatRoomId, nameof(chatRoomId));
            return await _chatMessageReadCommand.GetAllForRoomAsync(chatRoomId, userName);
        }

        [HttpPost]
        [Route("{chatRoomId:guid}/send/{profileId:guid}")]
        public async Task<ChatMessageResult> CreateAsync(Guid chatRoomId, Guid profileId, [FromBody] MessageDto messageDto)
        {
            var chatRoom = await _chatRoomRepository.GetAsync(chatRoomId);
            if (chatRoom == null)
            {
                messageDto.chatRoomId = chatRoomId;
                return await _chatMessageWriteCommand.CreateForEmptyRoomAsync(profileId, messageDto);
            }
            return await _chatMessageWriteCommand.CreateAsync(profileId, messageDto);
        }

        [HttpGet]
        [Route("{profileId:guid}/myrooms")]
        public async Task<List<ChatRoomResult>> GetRoomsAsync(Guid profileId)
        {
            Contract.Argument.IsNotEmptyGuid(profileId, nameof(profileId));
            var temp = await _chatRoomReadCommand.GetMyRooms(profileId);
            return temp;
        }

        [HttpGet]
        [Route("{profileId:guid}/allUsers/{userName}")]
        public async Task<List<ChatRoomResult>> GetRoomsAllRooms(Guid profileId, string userName)
        {
            Contract.Argument.IsNotEmptyGuid(profileId, nameof(profileId));
            var temp = await _chatRoomReadCommand.GetAllRooms(profileId, userName);
            return temp;
        }

    }
}