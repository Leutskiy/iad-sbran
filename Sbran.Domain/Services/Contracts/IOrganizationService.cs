using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Services.Contracts
{
    public interface IOrganizationService
    {
        Organization Add(OrganizationDto addedOrganization);
    }
}