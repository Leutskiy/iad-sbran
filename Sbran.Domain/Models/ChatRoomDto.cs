using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.Domain.Models
{
    public sealed class ChatRoomDto
    {
        public string? userid { get; set; }
        public string? image { get; set; }
        public string? chatRoomId { get; set; }
        public string? userfullname { get; set; }
        public string? lastmessagedate { get; set; }
        public string? lastmessage { get; set; }
    }
}
