using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities.System;

namespace Sbran.WebApp
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;

        private readonly IDictionary<string, string> _users = new Dictionary<string, string>
        {
            { "test1", "password1" },
            { "test2", "password2" },
            { "admin", "securePassword" }
        };

        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            _logger = logger;
            this._userRepository = userRepository;
        }

        public async Task<bool> IsValidUserCredentialsAsync(string userName, string password)
        {
            _logger.LogInformation($"Validating user [{userName}]");
            if (string.IsNullOrWhiteSpace(userName))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            var user = await _userRepository.Get(userName, password);

            return user != null;
        }

        public async Task<User> GetUserAsync(string userName, string password)
        {
            _logger.LogInformation($"Getting user [{userName}]");
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new Exception();
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception();
            }

            return await _userRepository.Get(userName, password);
        }

        public bool IsAnExistingUser(string userName)
        {
            return _users.ContainsKey(userName);
        }

        public string GetUserRole(string userName)
        {
            return UserRoles.Admin;

            /*
            if (!IsAnExistingUser(userName))
            {
                return string.Empty;
            }

            if (userName == "admin")
            {
                return UserRoles.Admin;
            }

            return UserRoles.BasicUser;
            */
        }
    }
}
