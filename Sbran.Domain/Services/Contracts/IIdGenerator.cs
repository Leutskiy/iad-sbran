using System;

namespace Sbran.Domain.Services.Contracts
{
    public interface IIdGenerator
    {
        Guid Generate();
    }
}