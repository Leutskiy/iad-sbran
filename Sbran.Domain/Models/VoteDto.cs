using System;
using System.Collections.Generic;

namespace Sbran.Domain.Models
{
	public sealed class VoteDto
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public List<VoteListDto>? voteLists { get; set; }
    }
}
