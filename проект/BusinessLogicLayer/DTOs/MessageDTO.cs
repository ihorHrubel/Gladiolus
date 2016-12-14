using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class MessageDTO
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime SendDateTime { get; set; }
        public string ConversationId { get; set; }
        public string UserId { get; set; }

    }
}
