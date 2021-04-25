using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.Domain.Entities
{
    public sealed class VoteList
    {
        public VoteList()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public string Name { get; set; }

        public Vote? Vote { get; set; }

        public double Count { get; set; } 

        public Guid VoteId { get; set; }
    }
}
