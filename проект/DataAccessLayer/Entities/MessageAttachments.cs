using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class MessageAttachments
    {
        [Key]
        public string Id { get; set; }

        public string AttachmentURL { get; set; }
        [ForeignKey("Message")]
        public string MessageId { get; set; }

        public virtual Message Message { get; set; }
    }
}
