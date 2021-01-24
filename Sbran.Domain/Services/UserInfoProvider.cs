using Sbran.Domain.Services.Contracts;
using System;

namespace Sbran.Domain.Services
{
    /// <summary>
    /// Генератор идентификаторов
    /// </summary>
    public sealed class UserInfoProvider : IUserInfoProvider
    {
        public Guid GetUserId()
        {
            throw new NotImplementedException();
        }
    }
}