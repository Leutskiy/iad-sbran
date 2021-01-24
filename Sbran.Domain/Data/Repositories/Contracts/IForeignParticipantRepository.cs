using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IForeignParticipantRepository
    {
        ForeignParticipant Create();

        ForeignParticipant Add(ForeignParticipantDto addedForeignParticipant);

        Task DeleteAsync(Guid id);

        Task<List<ForeignParticipant>> GetAllAsync();

        Task<ForeignParticipant> GetAsync(Guid id);
    }
}