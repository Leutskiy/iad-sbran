using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IAlienRepository
    {
        Alien Create();

        Alien Add(InviteeDto addedAlien);

        Task DeleteAsync(Guid id);

        Task<List<Alien>> GetAllAsync();

        Task<Alien> GetAsync(Guid id);
    }
}