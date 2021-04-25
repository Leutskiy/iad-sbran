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
    public sealed class MembershipRepository : IMembershipRepository
    {
        private readonly DomainContext _domainContext;

        public MembershipRepository(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        public Membership Add(MembershipDto addedMembership)
        {
            var membership = new Membership()
            {
                NameOfCompany = addedMembership.NameOfCompany,
                StatusInTheOrganization = addedMembership.StatusInTheOrganization,
                DateOfEntry = addedMembership.DateOfEntry,
                MembershipType = addedMembership.MembershipType,
                EmployeeId = addedMembership.EmployeeId,
                SiteOfTheOrganization = addedMembership.SiteOfTheOrganization
            };
            _domainContext.Memberships.Add(membership);
            return membership;
        }

        public async Task DeleteAsync(Guid id)
        {
            var membership = await _domainContext.Memberships.FirstOrDefaultAsync(e => e.Id == id);
            if (membership != null)
            {
                _domainContext.Remove(membership);
            }
        }

        public async Task<List<Membership>> GetAllAsync() => await _domainContext.Memberships.ToListAsync();

        public async Task<Membership> GetAsync(Guid id) => await _domainContext.Memberships.FirstOrDefaultAsync(e => e.Id == id);

        public async Task UpdateAsync(Guid currentMembershipId, MembershipDto newMembership)
        {
            var membership = await _domainContext.Memberships.FirstOrDefaultAsync(e => e.Id == currentMembershipId);
            if (membership != null)
            {

                membership.NameOfCompany = newMembership.NameOfCompany;
                membership.StatusInTheOrganization = newMembership.StatusInTheOrganization;
                membership.DateOfEntry = newMembership.DateOfEntry;
                membership.SiteOfTheOrganization = newMembership.SiteOfTheOrganization;
                membership.EmployeeId = newMembership.EmployeeId;
            }
        }
    }
}
