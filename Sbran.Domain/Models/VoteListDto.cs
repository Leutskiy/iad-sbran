using Sbran.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
