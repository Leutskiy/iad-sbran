using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IMembershipRepository
    {
        Membership Add(MembershipDto addedMembership);

        Task DeleteAsync(Guid id);

        Task<List<Membership>> GetAllAsync();

        Task<Membership> GetAsync(Guid id);

        Task UpdateAsync(Guid id, MembershipDto membershipDto);
    }
}