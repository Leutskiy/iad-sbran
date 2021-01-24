using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.CQS.Read.Contracts
{
    public interface IReadCommand<TDto> where TDto : class
    {
        Task<TDto> ExecuteAsync(Guid id);

        Task<IEnumerable<TDto>> ExecuteAsync();
    }
}