using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.Domain.Models
{
    public sealed class MessageDto
    {
        public string? userId { get; set; }
        public string? chatRoomId { get; set; }
        public string? message { get; set; }
    }
}
