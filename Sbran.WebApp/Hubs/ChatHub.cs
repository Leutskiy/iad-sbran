using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Sbran.CQS.Read.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.WebApp.Hubs
{
    public static class UserHandler
    {
        public static HashSet<string> ConnectedIds = new HashSet<string>();
    }
    /// <summary>
    /// Чат-хаб
    /// </summary>
    [Authorize]
    public sealed class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            UserHandler.ConnectedIds.Add(Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
        /* OLD
		public Task SendMessage(string user, string message)
		{
			return Clients.All.SendAsync("RecievedMessage", user, message);
		}
		*/

        public async Task Send(ChatMessageResult message, string to)
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

    }
}
