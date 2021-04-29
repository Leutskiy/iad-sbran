using Sbran.Domain.Entities;
using System;

namespace Sbran.Domain.Models
{
	public sealed class VoteListDto
    {
        public Guid Id { get; private set; }
        public string? Name { get; set; }
        public Vote? Vote { get; set; }
        public int? Count { get; set; }
        public Guid? VoteId { get; set; }

    }
}
