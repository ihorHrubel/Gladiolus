using System.Data.Entity.ModelConfiguration;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Mapping
{
    class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            this
                .HasMany<Conversation>(u => u.Conversations)
                .WithMany(c => c.Users)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserRefId");
                    cs.MapRightKey("ConversationRefId");
                    cs.ToTable("ConversationUser");
                });
            this
               .HasOptional(u => u.userProfile) // Mark Address property optional in Student entity
               .WithRequired(up => up.User);
        }
    }
}
