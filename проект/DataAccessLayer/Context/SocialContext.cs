using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DataAccessLayer.Entities;
using DataAccessLayer.Mapping;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.Context
{
    public class SocialContext : IdentityDbContext<User>
    {
        public SocialContext() : base("DBConnection")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<BlackListUser> BlackListUsers { get; set; }
        public DbSet<MessageAttachments> MessageAttachments { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Friend> Friends { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
            modelBuilder.Configurations.Add(new UserMapping());
            modelBuilder.Configurations.Add(new MessageMapping());
            modelBuilder.Configurations.Add(new FriendMapping());
        }
    }
}
        
