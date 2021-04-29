using Sbran.Domain.Entities.System;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersFull();
        User Create(string account, string password, Profile profile);
        Task<User> GetAsync(string userName, string password);
        Task<User> GetWithId(Guid id);
        Task<Guid> GetEmployeeId(Guid userId);
        Task<Guid> GetProfileId(Guid userId);
        Task<User> GetProfileForUserName(string userName);
    }
}