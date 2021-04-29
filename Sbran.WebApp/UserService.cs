using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities.System;
using System.Linq;

namespace Sbran.WebApp
{
    /// <summary>
    /// Сервис для работы с пользователями
    /// </summary>
    public sealed class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;

            AdminEmailDomain = "admin.ru";
            InstituteEmailDomains = new HashSet<string>
            {
                "ict.nsc.ru",
                "spsl.nsc.ru",
                "tomo.nsc.ru",
                "iae.nsk.su",
                "archaeology.nsc.ru",
                "sscc.ru",
                "igm.nsc.ru",
                "hydro.nsc.ru",
                "misd.ru",
                "history.nsc.ru",
                "laser.nsc.ru",
                "math.nsc.ru",
                "mcb.nsc.ru",
                "soramn.ru",
                "niic.nsc.ru",
                "ipgg.sbras.ru",
                "issa-siberia.ru",
                "eco.nsc.ru",
                "iis.nsk.su",
                "itam.nsc.ru",
                "itp.nsc.ru",
                "isp.nsc.ru",
                "philology.nsc.ru",
                "philosophy.nsc.ru",
                "solid.nsc.ru",
                "niboch.nsc.ru",
                "kinetics.nsc.ru",
                "ieie.nsc.ru",
                "inp.nsk.su",
                "tdisie.nsc.ru",
                "kti-git.nsc.ru",
                "ieie.nsc.ru",
                "soramn.ru",
                "niikel.ru",
                "soramn.ru",
                "phsysiol.ru",
                "soramn.ru",
                "nioch.nsc.ru",
                "ad-sbras.nsc.ru",
                "oesd.ru",
                "bk.ru",
                "catalysis.ru",
                "icg-bionet.nsc.ru",
                "ict.nsc.ru",
                "centercem.ru",
                "cnmt.ru"
            };
        }

        /// <summary>
        /// Почтовые домены институтов СО РАН
        /// </summary>
		public IReadOnlySet<string> InstituteEmailDomains { get; init; }

        /// <summary>
        /// Почтовый домен администратора
        /// </summary>
		public string AdminEmailDomain { get; init; }

        /// <summary>
        /// Аутентифицировать пользователя (без идентификации, сразу проверяем пользователя по паре логин/пароль)
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>Правильно ли указана пара логин/пароль</returns>
        public async Task<bool> IsValidUserCredentialsAsync(string login, string password)
        {
            _logger.LogInformation($"Start validating user credentials for [{login}]");

            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentException($"'{nameof(login)}' cannot be null or empty", nameof(login));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"'{nameof(password)}' cannot be null or empty", nameof(password));
            }

            var user = await GetUserAsync(login, password);

            return user != null;
        }

        public Task<User> GetUserAsync(string login, string password)
        {
            _logger.LogInformation($"Start getting user for {login}");

            //TODO: доработать для телефона/электронной почты
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentException($"'{nameof(login)}' cannot be null or empty", nameof(login));
            }

            //TODO: доработать проверку безопасности пароля (кол-во символов, повторяемость символов, сложность)
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"'{nameof(password)}' cannot be null or empty", nameof(password));
            }

            return _userRepository.GetAsync(login, password);
        }

        /// <summary>
        /// Проверить, что пользователь существует
        /// </summary>
        /// <param name="login">Логин пользователя. Cейчас требуем Email</param>
        /// <returns></returns>
        public bool IsAnExistingUser(string login)
        {
            throw new NotImplementedException("Implementation is on development state");
        }

        /// <summary>
        /// Определить пользовательскую роль
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Пользовательская роль</returns>
        public string DetectUserRole(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentException($"'{nameof(login)}' cannot be null or empty", nameof(login));
            }

            _logger.LogInformation($"Start detecting of the user role for [{login}]");

            var hasEmailDomainOfInstitute = InstituteEmailDomains.Any(emailDomain => login.Contains(emailDomain));
			if (hasEmailDomainOfInstitute)
			{
                _logger.LogInformation($"The [{login}] user has the Institute Director role");
                return UserRoles.InstituteDirector;
			}

            var hasEmailDomainOfAdmin = login.Contains(AdminEmailDomain);
            if (hasEmailDomainOfAdmin)
			{
                _logger.LogInformation($"The [{login}] user has the Admin role");
                return UserRoles.Admin;
			}

            _logger.LogInformation($"The [{login}] user has the Institute Employee role");
            return UserRoles.InstituteEmoloyee;
        }
    }
}
