using System.Data.Entity.ModelConfiguration;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Mapping
{
    class FriendMapping : EntityTypeConfiguration<Friend>
    {
        public FriendMapping()
        {
            this
            .HasRequired(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .WillCascadeOnDelete(true);

            this
            .HasRequired(p => p.Friends)
            .WithMany()
            .HasForeignKey(p => p.FriendId)
            .WillCascadeOnDelete(false);
        }
    }
}