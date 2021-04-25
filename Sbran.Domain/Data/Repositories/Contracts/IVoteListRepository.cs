using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IVoteListRepository
    {
        Task DeleteAsync(Guid id);

        Task<List<VoteList>> GetAllAsync();

        Task<VoteList> GetAsync(Guid id);
        Task AddCount(Guid id);

        VoteList Add(VoteListDto addedVoteList);

        Task UpdateAsync(Guid currentVoteListId, VoteListDto newVoteList);
    }
}