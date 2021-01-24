using System.Threading.Tasks;

namespace Sbran.WebApp
{
	public interface IUserService
    {
        bool IsAnExistingUser(string userName);
        Task<bool> IsValidUserCredentialsAsync(string userName, string password);
        string GetUserRole(string userName);
    }
}
