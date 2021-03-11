using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Sbran.Domain.Models;
using System.Threading.Tasks;

namespace Sbran.WebApp.Hubs
{
    /// <summary>
    /// Чат-хаб
    /// </summary>
    [Authorize]
    public sealed class ChatHub : Hub
    {
        /* OLD
		public Task SendMessage(string user, string message)
		{
			return Clients.All.SendAsync("RecievedMessage", user, message);
		}
		*/

        public async Task Send(MessagesChatDto message, string to)
        {
            var userName = Context?.User?.Identity?.Name ?? "";
            if (userName != "")
            {
                var fromUser = message;
                fromUser.IsValid = false;
                await Clients.User(userName).SendAsync("Receive", fromUser, to);
            }
            var toUser = message;
            toUser.IsValid = true;
            toUser.profileId = toUser.profileTo;
            await Clients.User(to).SendAsync("Receive", toUser, userName);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify", $"Приветствуем {Context.UserIdentifier}", Context.UserIdentifier);
            await base.OnConnectedAsync();
        }
    }
}
