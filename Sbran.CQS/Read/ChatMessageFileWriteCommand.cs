using Microsoft.AspNetCore.Http;
using Sbran.CQS.Converters;
using Sbran.CQS.Read.Results;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities.Chat;
using Sbran.Domain.Entities.System;
using Sbran.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.CQS.Read
{
    public sealed class ChatMessageFileWriteCommand
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IChatMessageFileRepository _chatMessageFileRepository;
        private readonly DomainContext _domainContext;
        private readonly IUserRepository _userRepository;

        public ChatMessageFileWriteCommand(
            IChatMessageRepository chatMessageRepository,
            IChatMessageFileRepository chatMessageFileRepository,
            IUserRepository userRepository,
            DomainContext domainContext
            )
        {
            _chatMessageRepository = chatMessageRepository;
            _chatMessageFileRepository = chatMessageFileRepository;
            _domainContext = domainContext;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Функция создания сообщения
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ChatMessageResult> CreateFile(ChatMessageFileDto file)
        {
            var user = await _userRepository.GetWithId(file.profileId);
            var userTo = await _userRepository.GetProfileForUserName(file.account);
            var message = new ChatMessage
            {
                ChatRoomId = file.chatRoomId,
                DateTime = DateTime.Now,
                Message = "",
                UserId = user.Id
            };
            var fileBase64String = Convert.FromBase64String(file.fileBinary);
            var chatMessageFile = new ChatMessageFile
            {
                ChatMessageId = message.Id,
                FileBinary = fileBase64String,
                fileName = file.fileName
            };
            await _chatMessageRepository.CreateAsync(message);
            await _chatMessageFileRepository.Create(chatMessageFile);
            await _domainContext.SaveChangesAsync();
            return new ChatMessageResult
            {
                DateTime = message.DateTime,
                fileId = chatMessageFile.Id,
                fileName = chatMessageFile.fileName,
                IsFile = true,
                Message = "",
                profileId = file.profileId,
                profileTo = userTo.Id,
            };
        }

        public async Task<byte[]> GetBytes(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
