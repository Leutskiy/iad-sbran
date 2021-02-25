using System;

namespace Sbran.WebApp.Models
{
	public sealed class ParticipantChat
	{
		public Guid UserId { get; set; } = default!;

		public string UserFullName { get; set; } = default!;

		public byte[]? Avatar { get; set; } = default!;

		public string LastMessage { get; set; } = default!;

		public DateTimeOffset LastMesssageDate { get; set; } = default!;

	}
}