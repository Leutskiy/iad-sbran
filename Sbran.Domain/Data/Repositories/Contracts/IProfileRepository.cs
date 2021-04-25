using System;
using System.Threading.Tasks;
using Sbran.Domain.Entities.System;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IProfileRepository
    {
        Profile Create();

        Task<Profile> GetAsync(Guid id);

        Task<Profile[]> GetAllAsync();

        Task UpdateAsync(Guid profileId, ProfileDto newProfileData);
    }
}