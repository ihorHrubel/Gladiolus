using System.Data.Entity.ModelConfiguration;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Mapping
{
    class MessageMapping : EntityTypeConfiguration<Message>
    {
        public MessageMapping()
        {
            this
             .HasRequired<Conversation>(m => m.Conversation)
             .WithMany(c => c.Messages)
             .HasForeignKey(m => m.ConversationId);

            this
           .HasRequired<User>(m => m.User)
           .WithMany(u => u.Messages)
           .HasForeignKey(m => m.UserId);

            this
                   .HasMany<MessageAttachments>(m => m.MessageAttachments) 
                    .WithOptional(ma => ma.Message)  
                    .HasForeignKey(ma => ma.MessageId); 
        }
    }
}
