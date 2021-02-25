using System;

namespace Sbran.CQS.Read.Results
{
	public sealed class ParticipantChatResult
	{
		public Guid UserId { get; set; } = default!;

		public string UserFullName { get; set; } = default!;

		public byte[]? Avatar { get; set; } = default!;

		public string LastMessage { get; set; } = default!;

		public DateTimeOffset LastMesssageDate { get; set; } = default!;
	}
}