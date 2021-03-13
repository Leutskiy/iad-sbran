using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.Domain.Models
{
    public sealed class MessageDto
    {
        public string account { get; set; }
        public Guid chatRoomId { get; set; }
        public string? message { get; set; }
    }
}
