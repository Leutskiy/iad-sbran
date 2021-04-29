using Microsoft.EntityFrameworkCore;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories
{
	public sealed class ScientificInterestsRepository : IScientificInterestsRepository
    {
        private readonly DomainContext _domainContext;

        public ScientificInterestsRepository(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        public ScientificInterests Add(ScientificInterestsDto addedScientificInterests)
        {
            var scientificInterests = new ScientificInterests()
            {
                NameOfScientificInterests = addedScientificInterests.NameOfScientificInterests,
                EmployeeId = addedScientificInterests.EmployeeId
            };
            _domainContext.ScientificInterests.Add(scientificInterests);
            return scientificInterests;
        }

        public async Task DeleteAsync(Guid id)
        {
            var scientificInterests = await _domainContext.ScientificInterests.FirstOrDefaultAsync(e => e.Id == id);
            if (scientificInterests != null)
            {
                _domainContext.Remove(scientificInterests);
            }
        }

        public async Task<List<ScientificInterests>> GetAllAsync() => await _domainContext.ScientificInterests.ToListAsync();

        public async Task<ScientificInterests> GetAsync(Guid id) => await _domainContext.ScientificInterests.FirstOrDefaultAsync(e => e.Id == id);

        public async Task UpdateAsync(Guid currentScientificInterestsId, ScientificInterestsDto newScientificInterests)
        {
            var scientificInterests = await _domainContext.ScientificInterests.FirstOrDefaultAsync(e => e.Id == currentScientificInterestsId);
            if (scientificInterests != null)
            {
                scientificInterests.NameOfScientificInterests = newScientificInterests.NameOfScientificInterests;
                scientificInterests.EmployeeId = newScientificInterests.EmployeeId;
            }
        }
    }
}
