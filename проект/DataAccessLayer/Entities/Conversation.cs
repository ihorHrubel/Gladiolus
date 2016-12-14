using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Conversation : IEntity
    {
        [Key]
        public string Id { get; set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

        public Conversation()
        {
            this.Users = new HashSet<User>();
            this.Messages = new List<Message>();
        }
    }
}
