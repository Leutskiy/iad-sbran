using Sbran.Domain.Entities.System;
using System;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IUserRepository
    {
        User Create(string account, string password, Profile profile);
        Task<User> Get(string userName, string password);
        Task<Guid> GetEmployeeId(Guid userId);
        Task<Guid> GetProfileId(Guid userId);
    }
}