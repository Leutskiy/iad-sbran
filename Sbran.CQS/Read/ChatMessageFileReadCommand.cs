using Microsoft.AspNetCore.Http;
using Sbran.CQS.Read.Contracts;
using Sbran.CQS.Read.Results;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities.Chat;
using Sbran.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.CQS.Read
{
    public sealed class ChatMessageFileReadCommand : IReadCommand<ChatMessageFileResult>
    {
        private readonly IChatMessageFileRepository _chatMessageFileRepository;

        public ChatMessageFileReadCommand(
            IChatMessageFileRepository chatMessageFileRepository)
        {
            _chatMessageFileRepository = chatMessageFileRepository;
        }

        public Task<ChatMessageFileResult> ExecuteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChatMessageFileResult>> ExecuteAsync()
        {
            throw new NotImplementedException();
        }

    }
}
