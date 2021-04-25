using Sbran.Domain.Entities;
using Sbran.Domain.Models;
using System;

namespace Sbran.Domain.Services.Contracts
{
    [Obsolete]
    public interface IInvitationService
    {
        Invitation Add(Employee employee, InvitationDto addedInvitation);

        void Edit(InvitationDto editedInvitation);
    }
}