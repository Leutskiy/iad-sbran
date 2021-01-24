using Sbran.Domain.Services.Contracts;
using System;

namespace Sbran.Domain.Services
{
    public sealed class SystemClock : IClock
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}