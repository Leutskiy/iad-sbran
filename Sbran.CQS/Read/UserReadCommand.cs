using System;
using System.Threading.Tasks;
using Sbran.Domain.Data.Repositories.Contracts;

namespace Sbran.CQS.Read
{
	/// <summary>
	/// Команда для чтения пользовательских данных
	/// </summary>
	public sealed class UserReadCommand
    {
        private readonly IUserRepository _userRepository;

        public UserReadCommand(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Получить Идентификатор иностранца
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Идентификатор иностранца</returns>
        public Task<Guid> GetEmployeeIdAsync(Guid userId)
        {
            return _userRepository.GetEmployeeId(userId);
        }
    }
}
