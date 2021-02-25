

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Sbran.CQS.Read;
using Sbran.CQS.Read.Results;
using Sbran.WebApp.Hubs;
using Sbran.WebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.WebApp.Controllers
{
	[Route("api/chat")]
	[ApiController]
	//[Authorize]
	public sealed class ChatController : ControllerBase
	{
		private readonly IHubContext<ChatHub> _hubContext;
		private readonly ParticipantChatReadCommand _participantChatReadCommand;

		public ChatController(IHubContext<ChatHub> hubContext, ParticipantChatReadCommand participantChatReadCommand)
		{
			_hubContext = hubContext;
			_participantChatReadCommand = participantChatReadCommand;
		}

		[Route("send")]
		[HttpPost]
		public IActionResult SendReqeust([FromBody] MessageDto messageDto)
		{
			_hubContext.Clients.All.SendAsync("RecievedMessage", messageDto.user, messageDto.messageText);

			return Ok();
		}

		[Route("participants")]
		[HttpGet]
		public Task<IEnumerable<ParticipantChatResult>> GetParticipants()
		{
			return _participantChatReadCommand.GetParticipants();
		}
	}
}
