using System;

namespace Sbran.Domain.Services.Contracts
{
    public interface IClock
    {
        DateTimeOffset Now();
    }
}