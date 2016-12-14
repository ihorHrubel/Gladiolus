using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccessLayer.Entities
{

    public class Message : IEntity
    {
        public Message()
        {
            this.MessageAttachments=new List<MessageAttachments>();
        }

        [Key]
        public string Id { get; set; }
        public DateTime SendDateTime { get; set; }
        public string Text { get; set; }
        [ForeignKey("Conversation")]
        public string ConversationId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Conversation Conversation { get; set; }
        public virtual ICollection<MessageAttachments> MessageAttachments { get; set; }
    }
}
