using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IVoteRepository
    {
        Task DeleteAsync(Guid id);

        Task<List<Vote>> GetAllAsync();

        Task<Vote> GetAsync(Guid id);

        Vote Add(VoteDto addedVote);

        Task UpdateAsync(Guid currentVoteId, VoteDto newVote);
    }
}