using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("BlackListUsers")]
   public class BlackListUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RelationId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string BlockedUserId { get; set; }
        [ForeignKey("BlockedUserId")]
        public User BlockedUsers { get; set; }
    }
}