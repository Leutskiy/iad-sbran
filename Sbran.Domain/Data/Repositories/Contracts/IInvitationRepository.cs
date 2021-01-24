using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IInvitationRepository
    {
        Invitation Create(Alien alien, Employee employee);

        Invitation Add(Employee employee, InvitationDto addedInvitation);

        Task DeleteAsync(Guid id);

        Task<List<Invitation>> GetAllAsync();

        Task<Invitation> GetAsync(Guid id);
    }
}