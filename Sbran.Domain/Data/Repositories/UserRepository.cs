using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities;
using Sbran.Domain.Entities.System;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sbran.Domain.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SystemContext _systemContext;
        private readonly DomainContext _domainContext;

        public UserRepository(
            SystemContext systemContext,
            DomainContext domainContext)
        {
            _systemContext = systemContext;
            _domainContext = domainContext;
        }

        public User Create(string account, string password, Profile profile)
        {
            var createdUser = new User(profile);

            createdUser.SetAccount(account);
            createdUser.SetPassword(password);

            _systemContext.Users.Add(createdUser);

            return createdUser;
        }

        public async Task<User> Get(string userName, string password)
        {
            var user = await _systemContext.Users.FirstOrDefaultAsync(ctx => ctx.Account == userName && ctx.Password == password);

            /*проверка на NULL*/

            return user;
        }

        // TODO: этот метод убрать отсюда
        public async Task<Guid> GetEmployeeId(Guid userId)
        {
            var employeeId = await _domainContext.Set<Employee>()
                .Where(empl => empl.UserId == userId)
                .Select(empl => empl.Id)
                .FirstOrDefaultAsync();

            if (employeeId == Guid.Empty)
            {
                throw new Exception($"Сотрудник не найден для пользователя с id: {userId}");
            }

            return employeeId;
        }

        public async Task<Guid> GetProfileId(Guid userId)
        {
            var user = await _systemContext.Users.FirstOrDefaultAsync(user => user.Id == userId);

            if (user == null)
            {
                throw new Exception($"User is not found by id: {userId}");
            }

            return user.ProfileId;
        }

        public Task<List<User>> GetUsersFull()
        {
            return _systemContext.Users.Include<User, Profile>(user => user.Profile).ToListAsync();
        }
    }
}
