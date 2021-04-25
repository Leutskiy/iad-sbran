using System;

namespace Sbran.Domain.Services.Contracts
{
    public interface IUserInfoProvider
    {
        Guid GetUserId();
    }
}