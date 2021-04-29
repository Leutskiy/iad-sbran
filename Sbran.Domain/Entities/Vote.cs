using System;
using System.Collections.Generic;

namespace Sbran.Domain.Entities
{
	public sealed class Vote
    {
        public Vote()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<VoteList> voteLists { get; set; }
    }
}
