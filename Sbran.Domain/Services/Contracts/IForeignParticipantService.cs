using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Services.Contracts
{
    public interface IForeignParticipantService
    {
        ForeignParticipant Add(ForeignParticipantDto addedForeignParticipant);
    }
}