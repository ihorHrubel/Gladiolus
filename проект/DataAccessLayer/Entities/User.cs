using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.Entities
{
    public class User : IdentityUser,IEntity
    {
        public UserProfile userProfile { get; set; }
        public virtual ICollection<Conversation> Conversations { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public Conversation Conversation
        {
            get; set;
        }
        public Friend Friend { get; set; }
        public BlackListUser BlackListUser { get; set; }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public Message Message { get; set; }
        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            User p = obj as User;
            if ((System.Object)p == null)
            {
                return false;
            }
            return (UserName == p.UserName)&&(Id==p.Id);
        }
        public User()
        {
            this.Conversations = new HashSet<Conversation>();
            this.Messages = new List<Message>();
        }
    }
}