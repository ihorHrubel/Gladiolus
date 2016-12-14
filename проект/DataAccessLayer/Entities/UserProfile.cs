using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class UserProfile: IEntity
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set;}
        public bool IsOnline { get; set; }
        public string CurrentLocation { get; set; }
        public DateTime LastTimeOnline { get; set; }
        [Required, MinLength(2), MaxLength(15)]
        public string Name { get; set; }
        [Required, MinLength(2), MaxLength(21)]
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }
        public User User { get; set; }
    }
}
