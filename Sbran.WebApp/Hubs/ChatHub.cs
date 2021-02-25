using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
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

		public async Task Send(string message, string to)
		{
			var userName = Context?.User?.Identity?.Name;

			if (Context?.UserIdentifier != to) // если получатель и текущий пользователь не совпадают
			{
				await Clients.User(Context?.UserIdentifier).SendAsync("Receive", message, userName);
			}
				
			await Clients.User(to).SendAsync("Receive", message, userName);
		}

		public override async Task OnConnectedAsync()
		{
			await Clients.All.SendAsync("Notify", $"Приветствуем {Context.UserIdentifier}", Context.UserIdentifier);
			await base.OnConnectedAsync();
		}
	}
}
