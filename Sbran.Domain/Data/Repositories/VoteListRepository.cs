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
    public sealed class VoteListRepository : IVoteListRepository
    {
        private readonly DomainContext _domainContext;

        public VoteListRepository(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        public VoteList Add(VoteListDto addedVoteList)
        {
            var voteList = new VoteList()
            {
                Name = addedVoteList.Name,
                Count = addedVoteList.Count ?? 0,
                VoteId = addedVoteList.VoteId.Value,
            };
            _domainContext.VoteLists.Add(voteList);
            return voteList;
        }

        public async Task AddCount(Guid id)
        {
            var voteList = await _domainContext.VoteLists
             .FirstOrDefaultAsync(e => e.Id == id);
            voteList.Count += 1;
            _domainContext.VoteLists.Update(voteList);
           await _domainContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var voteList = await _domainContext.VoteLists.FirstOrDefaultAsync(e => e.Id == id);
            if (voteList != null)
            {
                _domainContext.Remove(voteList);
            }
        }

        public async Task<List<VoteList>> GetAllAsync() => await _domainContext
            .VoteLists
            .ToListAsync();

        public async Task<VoteList> GetAsync(Guid id) => await _domainContext.VoteLists
            .FirstOrDefaultAsync(e => e.Id == id);

        public async Task UpdateAsync(Guid currentVoteListId, VoteListDto newVoteList)
        {
            var voteList = await _domainContext.VoteLists.FirstOrDefaultAsync(e => e.Id == currentVoteListId);
            if (voteList != null)
            {
                voteList.Name = newVoteList.Name;
                voteList.Count = newVoteList.Count ?? 0;
                voteList.VoteId = newVoteList.VoteId.Value;
                _domainContext.VoteLists.Update(voteList);
            }
        }
    }
}
