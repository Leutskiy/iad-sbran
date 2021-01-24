using Sbran.Domain.Services.Contracts;
using System;

namespace Sbran.Domain.Services
{
    /// <summary>
    /// Генератор идентификаторов
    /// </summary>
    public sealed class IdGenerator : IIdGenerator
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}