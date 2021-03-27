using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IScientificInterestsRepository
    {
        ScientificInterests Add(ScientificInterestsDto addedScientificInterests);

        Task DeleteAsync(Guid id);

        Task<List<ScientificInterests>> GetAllAsync();

        Task<ScientificInterests> GetAsync(Guid id);
        Task UpdateAsync(Guid id, ScientificInterestsDto scientificInterestsDto);
    }
}