using System.Data.Entity.ModelConfiguration;
using DataAccessLayer.Entities;
namespace DataAccessLayer.Mapping
{
        class BlackListMapping : EntityTypeConfiguration<BlackListUser>
        {
            public BlackListMapping()
            {
                this
                .HasRequired(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(true);

                this
                .HasRequired(p => p.BlockedUsers)
                .WithMany()
                .HasForeignKey(p => p.BlockedUserId)
                .WillCascadeOnDelete(false);
            }
        }
    }