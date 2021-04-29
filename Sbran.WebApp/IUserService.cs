using Sbran.Domain.Entities.System;
using System.Threading.Tasks;

namespace Sbran.WebApp
{
	public interface IUserService
    {
        bool IsAnExistingUser(string login);
        Task<bool> IsValidUserCredentialsAsync(string login, string password);
        Task<User> GetUserAsync(string login, string password);
        string DetectUserRole(string login);
    }
}
