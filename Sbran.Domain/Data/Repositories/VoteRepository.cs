using Microsoft.EntityFrameworkCore;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories
{
    public sealed class VoteRepository : IVoteRepository
    {
        private readonly DomainContext _domainContext;

        public VoteRepository(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        public Vote Add(VoteDto addedVote)
        {
            var vote = new Vote()
            {
                Name = addedVote.Name??"",
            };
            _domainContext.Votes.Add(vote);
            return vote;
        }

        public async Task DeleteAsync(Guid id)
        {
            var vote = await _domainContext.Votes.FirstOrDefaultAsync(e => e.Id == id);
            if (vote != null)
            {
                _domainContext.Remove(vote);
            }
        }

        public async Task<List<Vote>> GetAllAsync() => await _domainContext
            .Votes
            .ToListAsync();

        public async Task<Vote> GetAsync(Guid id) => await _domainContext.Votes
            .FirstOrDefaultAsync(e => e.Id == id);

        public async Task UpdateAsync(Guid currentVoteId, VoteDto newVote)
        {
            var vote = await _domainContext.Votes.FirstOrDefaultAsync(e => e.Id == currentVoteId);
            if (vote != null)
            {
                vote.Name = newVote.Name??"";
                _domainContext.Votes.Update(vote);
            }
        }
    }
}
